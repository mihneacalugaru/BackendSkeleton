using System.ComponentModel.DataAnnotations;

namespace BackendSkeleton.Controllers.Account
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
