namespace clinic.Models
{
   public class Clinic
    {
        public Guid Id {get; set;}
 
        public string Name {get; set;}

        public string Logo {get; set;}

        public string Address {get; set;}
        public DateTime Close {get; set;}

        public DateTime Open {get; set;}


public ICollection<Doctor> Doctors {get; set;}

    }
}