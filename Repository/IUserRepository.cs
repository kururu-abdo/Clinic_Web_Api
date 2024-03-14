using clinic.Dto;
using clinic.Models;

namespace clinic.Repository
{
    
    public interface IUserRepository
    {
        Task<UserLogin?> Register(
            string userName , string email ,
             string password , string mobile,
             string roleId , string address , string token
        );


       Task<string> AddRole(string name , string des);

        Task<List<Role>> GetRoles();
Task<User?> GetUserByUserName(string userName);
Task<User?> LoginAsync(LoginDto dto);
Task<Boolean> userExists(string Username);
    }
}