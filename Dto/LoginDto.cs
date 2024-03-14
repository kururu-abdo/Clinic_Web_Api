using System.ComponentModel.DataAnnotations;

namespace clinic.Dto
{
    public class LoginDto
    {


        [Required]
        public string Username {get; set;}



[Required ,MinLength(6 ,ErrorMessage ="password must be at least 6 characters")]
        public string Password {get; set;}
    }
}