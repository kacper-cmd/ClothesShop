using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Services;
using WebsiteProjectMobile.Views;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    public class CartViewModel : AListViewModel<CartItemForView>
    {
      
        public Command OrderCommand { get; set; }
        public ProductDataStore productDataStore;
        public CartItemDataStore cartItemDataStore;
        public CartViewModel() : base("Cart")
        {
            Title = "Category";
            OrderCommand = new Command(OnOrder);
            productDataStore = DependencyService.Get<ProductDataStore>();
            cartItemDataStore = DependencyService.Get<CartItemDataStore>();
            LoadProducts();
        }
        
        private ObservableCollection<ProductForView> _Products;

        public ObservableCollection<ProductForView> Products
        {
            get => _Products;
            set => SetProperty(ref _Products, value);
        }
       
        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }
        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public async void LoadProducts()
        {
            try
            {

                var products = await cartItemDataStore.GetAllCurrentCartItems();
                Products = new ObservableCollection<ProductForView>(products);
               

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public async void OnOrder()
        {
            await ((CartItemDataStore)DataStore).MakeOrderFromCart();
        }
        public override void GoToAddPage()
        {
            throw new NotImplementedException();
        }

        
        public override void GoToEditPage(CartItemForView item)
        {
            throw new NotImplementedException();
        }
    }
}
