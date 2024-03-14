using clinic.Models;

namespace clinic.Core
{


    public interface IJwtProvider
    {
        string GetToken(string Username  , string Role);

          Task<Role> GetRole(Guid RoleId);

          Task<User?> GetUserByName(string userName);
    }
    
}