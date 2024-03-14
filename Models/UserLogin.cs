namespace clinic.Models
{
    public class UserLogin
    {
        public Guid Id {get; set;}

        public Guid RoleId {get; set;}

        public Guid UserId {get; set;}

        public string UserName {get; set;}

        public string Password {get; set;}

                public string Token {get; set;}

    public User User {get; set;}

    public Role Role {get; set;}
    }
}