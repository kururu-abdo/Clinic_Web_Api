namespace clinic.Models
{
    public class User
    {
        public Guid Id {get; set;}

        public string UserName {get; set;}

        public string Mobile {get; set;}

        public string Email {get; set;}

        public string Address {get; set;}

public string Password {get; set;}
        // public Guid RoleId {get; set;}


                public string? Token {get; set;} =null;


        public ICollection<Booking> Bookings {get; set;}
        public ICollection<UserRole> Roles {get; set;}

    }
    
}