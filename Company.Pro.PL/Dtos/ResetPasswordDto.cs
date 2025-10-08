using System.ComponentModel.DataAnnotations;

namespace Company.Pro.PL.Dtos
{
    public class ResetPasswordDto
    {

        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "ConfirmPassword Is Dose't Match Original Password !!")]
        public string ConfirmPassword { get; set; }
    }
}
