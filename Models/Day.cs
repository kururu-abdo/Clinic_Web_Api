namespace clinic.Models
{
    public class Day
    {
        public Guid Id {get; set;}
public string DayEnName {get; set;}
        public string DayArName {get; set;}




        public ICollection<Schedule> Schedules {get; set;}
    }
}