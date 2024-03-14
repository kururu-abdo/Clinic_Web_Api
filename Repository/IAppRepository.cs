using clinic.Models;

namespace clinic.Repository
{
    public interface IAppRepository
    {
        String GetToken(string userName , string Role);

        Task<Role> GetRole(Guid RoleId);


        Task<User?> GetUserByName(string userName);
    }
}