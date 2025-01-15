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
    public partial class CategoryEditPage : ContentPage
    {
        CategoryEditViewModel _viewModel;
        public CategoryEditPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CategoryEditViewModel();  

        }
    }
}