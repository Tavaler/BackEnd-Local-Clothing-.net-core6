using System.ComponentModel.DataAnnotations;

namespace Local.Models
{
    public class Transportation
    {
        [Key]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string Detail { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}