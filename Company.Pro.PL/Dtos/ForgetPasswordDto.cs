using System.ComponentModel.DataAnnotations;

namespace Company.Pro.PL.Dtos
{
    public class ForgetPasswordDto
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email Is Required !!")]
        public string Email { get; set; }
    }
}
