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
    public partial class CategoryPage : ContentPage
    {
        CategoryViewModel _viewModel;
        public CategoryPage()
        {
            InitializeComponent();
           BindingContext = _viewModel = new CategoryViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

      
    }
}