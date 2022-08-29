using System.ComponentModel.DataAnnotations;

namespace Local.DTOS.Products
{
    public class ProductRequest
    {
        //public string ProductId { get; set; }

        [Required]
        //[MaxLength(100, ErrorMessage = "no more than {1} chars")]
        public string ProductName { get; set; }

        [Required]
        //[Range(0, 1000, ErrorMessage = "between {1}-{2}")]
        public int ProductStock { get; set; }

        [Required]
        //[Range(0, 1_000_000, ErrorMessage = "between {1}-{2}")]
        public decimal ProductPrice { get; set; }

        [Required]
        //[MaxLength(100, ErrorMessage = "no more than {1} chars")]
        public string ProductDetail { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public IFormFileCollection? upfile { get; set; }

    }
}
