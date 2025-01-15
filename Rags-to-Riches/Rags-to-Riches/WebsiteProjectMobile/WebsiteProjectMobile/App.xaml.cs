using System;
using WebsiteProjectMobile.Services;
using WebsiteProjectMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebsiteProjectMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<CategoryDataStore>();
            DependencyService.Register<ProductDataStore>();
            DependencyService.Register<OrderDataStore>();
            DependencyService.Register<SizeDataStore>();
            DependencyService.Register<CartItemDataStore>();
            DependencyService.Register<AddressDataStore>();
            
            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
