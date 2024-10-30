using System.ComponentModel.DataAnnotations;

namespace UserPortal.Models.Dto.Account
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
