namespace clinic.Models
{
    public class Fees
    {
    public Guid Id {get; set;}

    public string Description {get; set;}

    public Guid ScheduleId {get; set;}

    public double amount {get; set;}



public Schedule Schedule {get; set;}
    
    }
}