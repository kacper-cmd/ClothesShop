using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Models;
using WebsiteProjectMobile.Services;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;
using static Xamarin.Essentials.Permissions;

namespace WebsiteProjectMobile.ViewModels
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class ProductDetailsViewModel : AListViewModel<ProductForView>
    {
        public ProductDataStore productDataStore;
        public CartItemDataStore cartItemDataStore;
        public Command AddToCartCommand { get; }
        public int Id { get; set; }

        public ProductForView product { get; set; }

        public ProductDetailsViewModel() : base("Product")
        {
            productDataStore = DependencyService.Get<ProductDataStore>();
            cartItemDataStore = DependencyService.Get<CartItemDataStore>();
            //AddToCartCommand = new Command(AddToCart);
            AddToCartCommand = new Command(AddToCart);
        }
        public async void LoadItemId(int productId)
        {
            try
            {
                product = await DataStore.GetItemAsync(productId);
                Id = product.ProductId;
                Description = product.Description;
                CategoryName = product.CategoryName;
                Cost = product.Cost ?? 0;
                Images = product.Images;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void AddToCart()
        {

            // await Shell.Current.DisplayAlert("Added to cart", $"{item.Description}", "Mcaffe");
            var cartDataStore = DependencyService.Get<CartItemDataStore>();
            await cartDataStore.AddToCart(Id, Cost);

        }

        private ICollection<ImageForView> _Images;

        public ICollection<ImageForView> Images
        {
            get => _Images;
            set => SetProperty(ref _Images, value);
        }
        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        private string _CategoryName;
        public string CategoryName
        {
            get => _CategoryName;
            set => SetProperty(ref _CategoryName, value);
        }
        private double _Cost;
        public double Cost
        {
            get => _Cost;
            set => SetProperty(ref _Cost, value);
        }

        private int productId;
        public int ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                LoadItemId(value);
            }
        }




        public override void GoToAddPage()
        {
            throw new NotImplementedException();
        }

        public override void GoToEditPage(ProductForView item)
        {
            throw new NotImplementedException();
        }





    }
}