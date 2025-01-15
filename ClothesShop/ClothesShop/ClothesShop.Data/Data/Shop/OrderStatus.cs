using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.Data.Data.Shop
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string? StatusName { get; set; }//  In Progress, Shipped, Delivered, Cancelled, Returned, Refunded
        public bool IsActive { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
