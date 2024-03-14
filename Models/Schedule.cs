namespace clinic.Models
{
    public class Schedule
    {
        public Guid Id {get; set;}

        public DateTime StartTime {get; set;}

        public DateTime EndTime {get; set;}

public int ScheduleStatus {get; set;} = 0;

        public Guid ScheduleTypeId {get; set;}

        public Guid DoctorId {get; set;}

public Guid DayId {get; set;}
        public Booking Booking {get; set;}

        public Doctor Doctor {get; set;}


        public ICollection<Fees> Fees {get; set;}

        public ScheduleType ScheduleType {get; set;}

        public Day Day {get; set;}
    }
}