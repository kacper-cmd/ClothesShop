using Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteProjectAPI.ViewModel
{
    public class ImageForView
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }

        public string? Image1 { get; set; }

    }
}
