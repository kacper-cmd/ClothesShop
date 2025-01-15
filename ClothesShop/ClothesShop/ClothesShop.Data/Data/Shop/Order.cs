using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.Data.Data.Shop
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Order Status")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(50)]
        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }
        //public int AddressId { get; set; }
        //[ForeignKey("AddressId")]
        //public Address Address { get; set; }
        [Required(ErrorMessage = "The Street field is required.")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "The City field is required.")]
        public string? City { get; set; }
        [Required(ErrorMessage = "The State field is required.")]
        public string? State { get; set; }
        [Required(ErrorMessage = "The ZipCode field is required.")]
       // [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "The ZipCode field must be a valid US zip code.")]
        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<OrderProduct>? OrderProducts { get; set; }


        public int? OrderStatusId { get; set; } = 1;
        [ForeignKey("OrderStatusId")]
        [Display(Name = "Order Status")]
        public OrderStatus? OrderStatus { get; set; }
    }
}
