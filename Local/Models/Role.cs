using System.ComponentModel.DataAnnotations;

namespace Local.Models
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreateData { get; set; }
        public string RoleIsused { get; set; }
    }
}
