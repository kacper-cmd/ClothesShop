using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteProjectMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebsiteProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : ContentPage
    {
        NewProductViewModel _viewModel;

        public NewProductPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewProductViewModel();

        }
       
    }
}