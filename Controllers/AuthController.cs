using clinic.Repository;
using Microsoft.AspNetCore.Mvc;


namespace clinic.Controllers
{
    


    [ApiController]
[Route("[controller]")]
    public class AuthController :ControllerBase
    {
        private readonly IAppRepository _appRepo;

        public AuthController(IAppRepository appRepo)
        {
            _appRepo = appRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken(string userName , string role){
            string token =   _appRepo.GetToken(userName,role);

            return Ok(token);
        } 
    }
}