using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; }


        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}