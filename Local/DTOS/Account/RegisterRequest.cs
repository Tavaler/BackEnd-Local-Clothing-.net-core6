using System.ComponentModel.DataAnnotations;

namespace Local.DTOS.Account
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [MinLength(5)]
        public string UserPassword { get; set; }
        public int RoleId { get; set; }

        [Required]
        public string UserFirstname { get; set; }

        [Required]
        public string UserLastname { get; set; }

        //[Required]
        //public string UserProfile { get; set; }

        [Required]
        public string UserPhone { get; set; }

        //public string UserIsuse { get; set; }
    }
}
