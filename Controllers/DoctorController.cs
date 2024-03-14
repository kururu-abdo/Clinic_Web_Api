using clinic.Dto;
using clinic.Models;
using clinic.Repository;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    

   [ApiController]
[Route("[controller]")]
    public class DoctorController :ControllerBase
    {
        private readonly IImageRepository _imageRepo;
        private readonly IDoctorRepository _doctorRepo;
                private readonly IAppRepository _appRepo;

        public DoctorController(IDoctorRepository 
        doctorRepo , IImageRepository imageRepository , 
        
        IAppRepository appRepository
        )
        {
            _doctorRepo = doctorRepo;
            _imageRepo =imageRepository;
            _appRepo =appRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(

string ClinicId ,
string FullName, 
string Mobile, 

string Email, 
string UserName, 

string Password, 
IFormFile Avatar
        ){

 string? url =  _imageRepo.UploadImage(Avatar);
            string token =
            
               _appRepo.GetToken(UserName,"Doctor");

 var request = new DcotorRegisterDto{
DateAdded = DateTime.Now ,
ClinicId = Guid.Parse(ClinicId), 

Password =Password ,
Email =Email ,
Mobile =Mobile ,
UserName =UserName,
FullName =FullName,
Avatar = url, 
Token = token
 };
var  doctor = await _doctorRepo.AddAsync(request);

return Ok(doctor);

        }
   
   
   
   
   [HttpGet]
   [Route("all/{clinicId}")]
   public async Task<IActionResult> GetAll(string clinicId){
    var doctors = await _doctorRepo.List(clinicId);
    return Ok(doctors);

   }



   [HttpPost]
   [Route("login")]
   public async Task<IActionResult> Login(DoctorLoginDto dto){
    var doctor = await _doctorRepo.LoginAsync(dto);


    if (doctor is null)
    {
        return Forbid();
    }
    return Ok(doctor);

   }
    }
}