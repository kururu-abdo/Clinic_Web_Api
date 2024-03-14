namespace clinic.Dto
{
    public class UserRegisterDto
    {
        
           public Guid Id {get; set;}

        public string UserName {get; set;}

        public string Mobile {get; set;}

        public string Email {get; set;}

        public string Address {get; set;}

        public Guid  RoleId {get; set;} 
    }
}