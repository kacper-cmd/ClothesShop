using System.ComponentModel;
using WebsiteProjectMobile.ViewModels;
using Xamarin.Forms;

namespace WebsiteProjectMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}