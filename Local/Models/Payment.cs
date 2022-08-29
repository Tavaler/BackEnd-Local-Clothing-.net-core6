using System.ComponentModel.DataAnnotations;

namespace Local.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }
        public string ImgPay { get; set; }
        public string? Status { get; set; }
        public string? Detail { get; set; }
        public DateTime? Createdate { get; set; }

        public string OrderId { get; set; }
    }
}
