using Data;

namespace WebsiteProjectAPI.ViewModel
{
    public class OrderForView
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public decimal? TotalCost { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }


        //public virtual ICollection<OrderProductForView> OrderProducts { get; set; } = new List<OrderProductForView>();

        public int? OrderStatusId { get; set; }
        public string? OrderStatusName { get;  set; }

        //public virtual OrderStatus? OrderStatus { get; set; }

        //public virtual UserForView User { get; set; } = null!;
    }
}
