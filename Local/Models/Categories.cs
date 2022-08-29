using System.ComponentModel.DataAnnotations;

namespace Local.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Isused { get; set; }
        
    }
}
