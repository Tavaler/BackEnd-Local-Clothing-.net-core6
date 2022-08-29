using System.ComponentModel.DataAnnotations;

namespace Local.Models
{
    public class ProductImg
    {
        [Key]
        public string ProductImgId { get; set; }
        public string ProductImgName { get; set;}
        public DateTime? CreateDate { get; set; }

        public string ProductId { get; set; }
    }
}
