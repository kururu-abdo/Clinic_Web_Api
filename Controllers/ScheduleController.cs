using clinic.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{


    [ApiController]
      [Route("[controller]")]
    public class ScheduleController:ControllerBase
    {



              [Authorize(Roles ="Doctor")]

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddScheduleDto dto){

          return Ok();
        }
    }
}