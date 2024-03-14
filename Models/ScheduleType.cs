namespace clinic.Models
{
    
    public class ScheduleType
    {
        public Guid Id {get; set;}

        public string Name { get; set;}

        public string Description {get; set;}
public Schedule Schedule {get; set;}

    }
}