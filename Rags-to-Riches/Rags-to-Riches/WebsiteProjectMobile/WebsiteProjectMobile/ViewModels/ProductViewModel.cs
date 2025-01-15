using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Models;
using WebsiteProjectMobile.Services;
using WebsiteProjectMobile.Views;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    public class ProductViewModel : AListViewModel<ProductForView>
    {
       public ICommand ItemTapped { get; private set; }

        public ProductViewModel() : base("Product")
        {
            Title = "Product";
            ItemTapped = new Command<ProductForView>(OnItemTapped);

        }
        private async void OnItemTapped(ProductForView product)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailsViewModel.ProductId)}={product.ProductId}");

        }
        private ProductForView selectedProduct;
        public ProductForView SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (selectedProduct != value)
                {
                    selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                    OnItemSelected(value);
                   
                }
            }
        }
        async void OnItemSelected(ProductForView item)
        {
            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailsViewModel.ProductId)}={item.ProductId}");

        }
        public override async void GoToAddPage()
        {
            await Shell.Current.GoToAsync(nameof(NewProductPage));
        }

        public override void GoToEditPage(ProductForView item)
        {
            throw new NotImplementedException();
        }
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;

                OnPropertyChanged(nameof(SearchText));
            }
        }
      
    }
}

