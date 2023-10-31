using System.ComponentModel.DataAnnotations;

namespace Techblog.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Password must be atleast 6 characters")]
        public string Password { get; set; }
    }
}
