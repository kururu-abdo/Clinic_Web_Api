namespace clinic.Dto 
{
    public class UserDto
    {
           public Guid Id {get; set;}

        public string UserName {get; set;}

        public string Mobile {get; set;}

        public string Email {get; set;}

        public string Address {get; set;}

        public string  RoleName {get; set;}


                public string? Token {get; set;} =null;

    }
}