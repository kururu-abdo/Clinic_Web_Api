using clinic.Models;

namespace clinic.Repository
{
    public class ClinicRepository : IClinicRepository
    {
        public IEnumerable<Clinic> GetAll()
        {
       return Enumerable.Range(1, 5).Select(index => new
        Clinic
        {
        })
        .ToArray();
        }
    }
}