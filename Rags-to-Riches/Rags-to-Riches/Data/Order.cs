
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;


public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal? TotalCost { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime? DeliveryDate { get; set; } = DateTime.Now.AddDays(7);

    public int? OrderStatusId { get; set; }

    public virtual ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();

    [ForeignKey("OrderStatusId")]
    public virtual OrderStatus? OrderStatus { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; } = null!;
}
