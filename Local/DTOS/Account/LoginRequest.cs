using System.ComponentModel.DataAnnotations;

namespace Local.DTOS.Account
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [MinLength(5)]
        public string UserPassword { get; set; }

    }
}
