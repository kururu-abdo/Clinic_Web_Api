namespace clinic.Models
{
    public class Doctor
    {
        public Guid Id {get; set;}

        public Guid ClinicId {get; set;}
 public string UserName {get; set;}

 public string FullName {get; set;}
public string Mobile {get; set;}

public string Email {get; set;}
public string Password {get; set;}
public string? Token {get; set;} = null;

public DateTime DateAdded {get; set;}

public string Avatar { get; set;}
        public Clinic Clinic {get; set;}

        
    }
}