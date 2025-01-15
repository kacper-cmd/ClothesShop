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
    public partial class OrderDetailPage : ContentPage
    {
        OrderSummaryViewModel _viewModel;
        public OrderDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new OrderSummaryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }

    }
}