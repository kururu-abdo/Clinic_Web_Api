using clinic.Core;
using clinic.Data;
using clinic.Dto;
using clinic.Models;
using clinic.Utils;
using Microsoft.EntityFrameworkCore;

namespace clinic.Repository
{

    public class DoctorRepository : IDoctorRepository
    {

        private readonly ClinicDb _clinicDb;
        private readonly IPasswordHasher _passwordHasher;
private readonly IJwtProvider _jwtProvider;
        public DoctorRepository(ClinicDb clinicDb , IPasswordHasher  passwordHasher , IJwtProvider jwtProvider)
        {
            _clinicDb = clinicDb;
            _passwordHasher =passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<DoctorDto> AddAsync(DcotorRegisterDto request)
        {

//hashing password

             var hash =   _passwordHasher.HashPassword  (
                request.Password);
//add ph

          var doctor = new Doctor{ 

            FullName =request.FullName ,
            Avatar = request.Avatar ,
            Password =hash,
            Email =request.Email ,
            Mobile = request.Mobile ,
            UserName =request.UserName,
DateAdded =request.DateAdded ,
Token = request.Token ,
ClinicId =request.ClinicId
          };
          await
_clinicDb.AddAsync(doctor);
await _clinicDb.SaveChangesAsync();
var result = new DoctorDto{
Token =request.Token ,
FullName =request.FullName ,
Email = request.Email,

};

return result;


        }

       

        public async Task<Doctor?> LoginAsync(DoctorLoginDto request)
        {
            if(! await DoctorExists(request.Email)){
                return null;
            }

            //use inclue
var doctor = await _clinicDb.Doctors.Where(x=> 
            x.Email==request.Email 
            
              )
            .Include(c=> c.Clinic)
            .FirstOrDefaultAsync();
             if(doctor is null){
                return null;
            }
if(!_passwordHasher.VerifyPassword(doctor.Password ,request.Password)){
    return null;
}

           
var token = _jwtProvider.GetToken(doctor.UserName ,"Doctor");

doctor.Token = token;
await _clinicDb.SaveChangesAsync();

return doctor!; 
       }

       

     public async   Task<ICollection<Doctor>> List(string ClinicId)
        {
            var doctors = await _clinicDb.Doctors.Where(x=> x.ClinicId==Guid.Parse(ClinicId)).ToListAsync();

           return doctors;
        }




         public async Task<Boolean> DoctorExists(string Username){
var doctor = await _clinicDb.Doctors.Where(x=> x.Email==Username).FirstOrDefaultAsync();


return doctor !=null;
        }
    }
}