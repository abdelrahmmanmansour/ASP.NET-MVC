using System.ComponentModel.DataAnnotations;

namespace Company.Pro.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName Is Required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName Is Required !!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Is Required !!")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email Is Required !!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword Is Dose't Match Original Password !!")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
