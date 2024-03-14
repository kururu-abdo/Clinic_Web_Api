namespace clinic.Core
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);


        bool VerifyPassword (string hashed , string password);
        
    }
}