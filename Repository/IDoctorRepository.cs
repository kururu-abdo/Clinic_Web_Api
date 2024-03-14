using clinic.Dto;
using clinic.Models;

namespace clinic.Repository
{
    public interface IDoctorRepository
    {
        Task<DoctorDto> AddAsync(DcotorRegisterDto request);

          Task<ICollection<Doctor>> List(string ClinicId);

                Task<Doctor?> LoginAsync(DoctorLoginDto request);

    }
}