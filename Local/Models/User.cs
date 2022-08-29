using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserProfile { get; set; } = "";
        public string UserPhone { get; set; }

        public string UserIsuse { get; set; } = "1";
        public DateTime UserCreate { get; set; } = DateTime.Now;


        public string RoleId { get; set; }
        //[ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
