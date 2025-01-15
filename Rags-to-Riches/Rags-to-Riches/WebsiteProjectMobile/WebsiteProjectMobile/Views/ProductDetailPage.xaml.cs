using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebsiteProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private ProductDetailsViewModel _viewModel;
        public ProductDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel  = new ProductDetailsViewModel();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}