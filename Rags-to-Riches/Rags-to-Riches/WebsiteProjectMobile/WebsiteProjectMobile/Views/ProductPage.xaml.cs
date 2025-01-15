using System.Linq;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebsiteProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
  
        ProductViewModel _viewModel;
        public ProductPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ProductViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

       
    }
}