//using Local.Migrations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local.Models
{
    public class Product
    {
        public Product()
        {
            ProductImg = new HashSet<ProductImg>();
        }
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductDetail { get; set; }
        public int ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public DateTime ProcuctDate { get; set; } = DateTime.Now;
        public string Isused { get; set; }

        //public string? ProductImage { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public virtual ICollection<ProductImg> ProductImg { get; set; }

        //public string LocalId { get; set; }
        //[ForeignKey("LocalId")]
        //[ValidateNever]
        //internal LocalProduct LocalProducrt { get; set; }

        //public Categories Category { get; set; }
        //public int? TypePId { get; set; }
        //public int? TypeCId { get; set; }
    }
}
