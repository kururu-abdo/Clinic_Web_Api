using System.Security.Cryptography;
using clinic.Core;
using clinic.Data;
using clinic.Dto;
using clinic.Models;
using clinic.Utils;
using Microsoft.EntityFrameworkCore;

namespace clinic.Repository
{
    public class UserRepository
    : IUserRepository
    {
        private readonly ClinicDb _clinicDb;
private readonly IPasswordHasher _passwordHasher;
        public UserRepository(ClinicDb clinicDb , IPasswordHasher passwordHasher){
            _clinicDb=clinicDb;
            _passwordHasher =passwordHasher;
        }

        public async Task<string> AddRole(string name , string des)
        {
           await _clinicDb.Roles.AddAsync(new Role{RoleName=name , Description=des});
           await _clinicDb.SaveChangesAsync();
return name;
        }

        public async Task<List<Role>> GetRoles()
        {
            var roles = await  _clinicDb.Roles.ToListAsync();
            return roles;
        }

        public async Task<UserLogin?> Register(string userName,
         string email, string password, string mobile, 
         string roleId, string address , string token)
        {


            var user = new  User{
                UserName =userName ,
                Email=email,
                Mobile=mobile,
                
                Address=address,
        Password= _passwordHasher.HashPassword(password),
Token=token 
            };
if (await IsUserFound(userName))
{
    return null;
}else {

    var insertedUser= await _clinicDb.Users.AddAsync(user);
 await _clinicDb.SaveChangesAsync();
    var userData = await GetUserByUserName(userName);

    await _clinicDb.UserRoles.AddAsync(new UserRole{ 
        RoleId=Guid.Parse(roleId) , UserId=userData!.Id
    });
    await _clinicDb.SaveChangesAsync();

    return new UserLogin{
        UserName =userName ,
        Token=token
        
    };

}
        }








        public async Task<bool> IsRoleFound(string id){
    var role = await _clinicDb.Roles.FindAsync(id);
    return role !=null;
}
  public async Task<bool> IsUserFound(string userName){
    var user = await _clinicDb.Users.FirstOrDefaultAsync(x => x.UserName==userName);
    return user !=null;
}

        public async Task<User?> GetUserByUserName(string userName)
        {
           var user = await _clinicDb.Users.FirstOrDefaultAsync(x=> x.UserName==userName );

           return user;
        }

        public async Task<User?> LoginAsync(LoginDto dto)
        {
if (!await userExists(dto.Username))
{
return null;
}
 var user = await _clinicDb.Users.Where(x=> 
            x.UserName==dto.Username 
            
          
            
            
            )
            .Include(r=> r.Roles)
            .FirstOrDefaultAsync();
if(!_passwordHasher.VerifyPassword(user.Password ,dto.Password)){
    return null;
}

           





return user!;
           
        }


        public async Task<Boolean> userExists(string Username){
var user = await _clinicDb.Users.Where(x=> x.UserName==Username).FirstOrDefaultAsync();


return user!=null;
        }
    }
}