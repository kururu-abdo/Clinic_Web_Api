using clinic.Models;
using clinic.Models.DTO;

namespace clinic.Repository
{
    public interface IHospitalRepository
    {
        Task<HospitalDTO> AddAsync(HospitalDTO dto);

        Task<List<Clinic>> GetAll();



        Task<HospitalDTO> Update(HospitalDTO dto);

        Task<string?> Delete(string hosptialId
        );


    }
}