using System.ComponentModel.DataAnnotations;

namespace WaxWelio.Web.Models
{
    public class LoginViewModels
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage ="Email cannot be blank")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password cannot be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}