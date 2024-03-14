using clinic.Data;
using clinic.Dto;
using clinic.Models;

namespace clinic.Repository
{
    public class SheduleRepository : ISheduleRepository
    {
        private readonly ClinicDb _clinicDb;

        public SheduleRepository(ClinicDb clinicDb)
        {
            _clinicDb = clinicDb;
        }

        public Task<Schedule> AddAsync(AddScheduleDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Day> AddDayAsync(string arName, string enName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Day>> GetDaysAsync()
        {
            throw new NotImplementedException();
        }
    }
}