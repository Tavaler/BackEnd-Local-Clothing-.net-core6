using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local.Models
{
    public class AddStock
    {
        [Key]
        public string Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
