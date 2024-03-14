using clinic.Models.DTO;
using clinic.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicController : ControllerBase
{

private readonly IClinicRepository _clinicRepo;
private readonly IHospitalRepository _hospitalRepo;
private readonly IImageRepository _imageRepo;

public ClinicController(IClinicRepository clinicRepository ,
 IHospitalRepository hospital, IImageRepository imageRepo){
    _clinicRepo =clinicRepository;
_hospitalRepo=hospital;
_imageRepo =imageRepo;
}

    [HttpGet(Name = "clinincs"), Authorize]
    public async Task<IActionResult> Get()
    {
        var clinics =  _clinicRepo.GetAll();
        return Ok(clinics); 
    }

   [HttpPost]
   public async Task<IActionResult> AddAsync(

   string Name ,

       IFormFile? Logo ,

         string Address,
         DateTime Close ,

         DateTime Open 
        

   ){
string? logoUrl;
var hospitalDTO= new HospitalDTO();
if (Logo is not null)
{
    string? url =  _imageRepo.UploadImage(Logo);
    if (url is not null)
    {
        logoUrl =url;
        hospitalDTO.Logo = logoUrl;
    }
    
}

hospitalDTO.Address =Address;
hospitalDTO.Name=Name;
hospitalDTO.Open=Open;
hospitalDTO.Close=Close;

    var result = await _hospitalRepo.AddAsync(hospitalDTO);

    return Ok(result);
   }



   [HttpGet]
   [Route("all")]
   public async Task<IActionResult> GetAll(){
    var hospitals = await _hospitalRepo.GetAll();
    return Ok(hospitals);
   }
}