using clinic.Dto;
using clinic.Repository;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class UserController :ControllerBase
    {
        private readonly IUserRepository _userRepo;
                private readonly IAppRepository _appRepo;

        public UserController(IUserRepository userRepo ,IAppRepository appRepository){
            _userRepo=userRepo;
            _appRepo =appRepository;
        }

[HttpPost]
public async Task<IActionResult> AddAsync( string userName , string email ,
             string password , string mobile,
             string roleId , string address ){


                var role  = await _appRepo.GetRole(Guid.Parse(roleId));
                var token =  _appRepo.GetToken(userName ,role.RoleName);
var userLogin  = await _userRepo.Register(userName,email,
password,mobile ,
roleId,address, token);


return Ok(userLogin);

             }


[HttpGet]
[Route("roles")]
public async Task<IActionResult> GetRolesAsync(){
var roles = await _userRepo.GetRoles();
return Ok(roles);
}


[HttpPost]
[Route("AddRole")]
public async Task<IActionResult> AddRoleAsync( string role, string desc){
try
{
    var roleData = await _userRepo.AddRole(role , desc);
    if (roleData is not null)
    {
        return Ok(role);
    }else {
           return BadRequest("operation failed");

    }
}
catch (System.Exception)
{
    
   return BadRequest("operation failed");
}
}




[HttpPost]
[Route("login")]
public async Task<IActionResult> LoginAsync(LoginDto  loginDto){
    var result = await _userRepo.LoginAsync(loginDto);
    if (result==null)
    {
        return Forbid();
    }

    return Ok(result);
}


    }



}