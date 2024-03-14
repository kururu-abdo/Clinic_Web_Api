namespace clinic.Models
{
    public class Role
    {
        public Guid Id {get; set;}

        public string RoleName {get; set;}

        public string Description {get; set;}


        public ICollection<UserRole> Roles {get; set;}
    }
}