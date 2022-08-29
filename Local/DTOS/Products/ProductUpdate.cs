using System.ComponentModel.DataAnnotations;

namespace Local.DTOS.Products
{
    public class ProductUpdate
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int ProductPrice { get; set; }

        public string? ProductDetail { get; set; }
        public int ProductStock { get; set; }

        //public string? Seed { get; set; }
        //public string? level { get; set; }
        [Required]
        public string CategoryId { get; set; }

        //public IFormFileCollection? upfile { get; set; }

        /// profile product  ProductImage
        public IFormFileCollection? upfile { get; set; }
        //public DateTime ProcuctDate { get; set; } = DateTime.Now;

        
    }
}
