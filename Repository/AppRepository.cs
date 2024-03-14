using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using clinic.Data;
using clinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace clinic.Repository
{

    public class AppRepository : IAppRepository
    {

        private readonly IConfiguration _config;
private readonly ClinicDb _clinicDb;
        public AppRepository(IConfiguration config , ClinicDb db)
        {
            _config = config;
            _clinicDb =db;
        }

        public async Task<Role> GetRole(Guid RoleId)
        {
           var role = await _clinicDb.Roles.FindAsync(RoleId);

           return role!;
        }

        public string GetToken(string userName , string Role)
        {
 List<Claim> Claims = new List<Claim> {
new Claim(ClaimTypes.Name , userName), 
new Claim(ClaimTypes.Role ,Role)
           };
            
          

var key = new 
SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));

var creds = new SigningCredentials(key , 
SecurityAlgorithms.HmacSha512Signature);


var tokenDescriptor = new SecurityTokenDescriptor{
    Subject = new ClaimsIdentity(Claims) ,
    Issuer=_config["JwtSettings:Issuer"], 
    Audience=_config["JwtSettings:Audience"], 
    SigningCredentials =creds  
};
var  token =  new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
var jwt = new JwtSecurityTokenHandler().WriteToken(token);
return jwt;


        }

        public async Task<User?> GetUserByName(string userName)
        {
            var user = await _clinicDb.Users.FirstOrDefaultAsync(x=> x.UserName==userName);
        
        return user;
        }
    
    
    
    }
}