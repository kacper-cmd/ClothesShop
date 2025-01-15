using Data;
using System.ComponentModel.DataAnnotations;

namespace WebsiteProjectAPI.ViewModel
{
    public class SizeForView
    {
        public int SizesId { get; set; }

        public string Size1 { get; set; } = null!;
      //  public virtual ICollection<ProductForView> Products { get; set; } = new List<ProductForView>();

    }
}
