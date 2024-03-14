using clinic.Models;

namespace clinic.Repository
{
    public interface IClinicRepository{
        IEnumerable<Clinic>  GetAll();
    }
}