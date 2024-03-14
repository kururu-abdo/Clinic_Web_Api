namespace clinic.Models
{
    public class Booking
    {
        public Guid Id {get; set;}
        public Guid UserId {get; set;}
        public Guid ScheduleId {get; set;}

        public DateTime DateAdded { get; set; } =  DateTime.Now ;
        


        public User User {get; set;}
        public Schedule Schedule {get; set;}
    }
}