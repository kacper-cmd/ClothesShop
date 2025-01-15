using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data;


public partial class OrderProduct
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    [Key]
    public int OrderProductsId { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order? Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; } = null!;
}
