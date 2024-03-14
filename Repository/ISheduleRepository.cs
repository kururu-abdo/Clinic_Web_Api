using clinic.Dto;
using clinic.Models;

namespace clinic.Repository
{
    public interface ISheduleRepository
    {
        Task<Schedule> AddAsync(AddScheduleDto dto);

        Task<Day> AddDayAsync(string arName , string enName);


        Task<IEnumerable<Day>> GetDaysAsync();


        
    }
}