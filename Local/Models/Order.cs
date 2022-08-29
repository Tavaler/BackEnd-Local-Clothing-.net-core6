using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Local.Models
{
    public class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
            Payment = new HashSet<Payment>();
            Transportation = new HashSet<Transportation>();
        }

        [Key]
        public string Id { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public int Total { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? TransportationCode { get; set; }
        public string Isused { get; set; }

        //public int AddressId { get; set; }
        //[ForeignKey("AddressId")]
        //[ValidateNever]

        [ValidateNever]
        public virtual ICollection<OrderItem>? OrderItem { get; set; }

        [ValidateNever]
        public virtual ICollection<Payment>? Payment { get; set; }

        [ValidateNever]
        public virtual ICollection<Transportation>? Transportation { get; set; }
    }
}
