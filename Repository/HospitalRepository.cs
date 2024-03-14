using clinic.Data;
using clinic.Models;
using clinic.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace clinic.Repository
{

    public class HospitalRepository : IHospitalRepository
    {

        private readonly ClinicDb _clinicDb;

        public HospitalRepository(ClinicDb clinicDb)
        {
            _clinicDb = clinicDb;
        }

        public async Task<HospitalDTO> AddAsync(HospitalDTO dto)
        {
            var hospital = new Clinic{
Name=dto.Name , 
Address =dto.Address , Close=dto.Close ,Open=dto.Open ,
Logo =dto.Logo
            };
        await   _clinicDb.Clinics.AddAsync(hospital);
        await _clinicDb.SaveChangesAsync();

        return dto;
        }

        public async Task<List<Clinic>> GetAll()
        {
            return  
            await _clinicDb.Clinics
            
            .ToListAsync();
        }
    }

}