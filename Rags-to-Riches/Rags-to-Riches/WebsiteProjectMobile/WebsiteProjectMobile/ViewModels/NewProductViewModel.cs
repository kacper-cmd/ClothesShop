using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Services;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    public class NewProductViewModel : ANewItemViewModel<ProductForView>, IDataErrorInfo
    {
        private CategoryDataStore categoryDataStore;
        private SizeDataStore sizeDataStore;
        public NewProductViewModel()
        {
            categoryDataStore = DependencyService.Get<CategoryDataStore>();
            sizeDataStore = DependencyService.Get<SizeDataStore>();
            Categories = new ObservableCollection<CategoryForView>();
            Sizes = new ObservableCollection<SizeForView>();
            LoadCategories();
            LoadSizes();

        }

        private async void LoadSizes()
        {
            var retrievedSizes =  await sizeDataStore.GetItemsAsync();
            foreach (var size in retrievedSizes)
            {
                Sizes.Add(size);
            }
        }

        private async void LoadCategories()
        {
            var retrievedCategories = await categoryDataStore.GetItemsAsync();
            foreach (var category in retrievedCategories)
            {
                Categories.Add(category);
            }
        }
        //private bool isActive;
        //private int idProduct;

        //public int IdProduct
        //{
        //    get => idProduct;
        //    set => SetProperty(ref idProduct, value);
        //}
        private string description;
        private string imageUrl;


        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    // name = value;
                    SetProperty(ref description, value);
                    ValidateProperty(nameof(Description));

                }
            }
        }
        public ObservableCollection<CategoryForView> Categories { get; }

        private CategoryForView selectedCategory;
        public CategoryForView SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (selectedCategory != value)
                {
                    SetProperty(ref selectedCategory, value);
                    ValidateProperty(nameof(SelectedCategory));
                    OnPropertyChanged(nameof(SelectedCategory));

                }
            }
        }

        public ObservableCollection<SizeForView> Sizes { get; }
     
        private SizeForView selectedSize;
        public SizeForView SelectedSize
        {
            get { return selectedSize; }
            set
            {
                if (selectedSize != value)
                {
                    SetProperty(ref selectedSize, value);
                    OnPropertyChanged(nameof(SelectedSize));
                }
            }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                if (imageUrl != value)
                {
                    SetProperty(ref imageUrl, value);
                    ValidateProperty(nameof(ImageUrl));
                    OnPropertyChanged(nameof(ImageUrl));
                }
            }
        }
        private double cost;
        public double Cost
        {
            get { return cost; }
            set
            {
                if (cost != value)
                {
                    // name = value;
                    SetProperty(ref cost, value);
                    ValidateProperty(nameof(Cost));
                    OnPropertyChanged(nameof(Cost));
                }
            }
        }

        //https://learn.microsoft.com/pl-pl/dotnet/desktop/wpf/data/how-to-implement-validation-logic-on-custom-objects?view=netframeworkdesktop-4.8

        public override ProductForView SetItem()
        {
            var image = new ImageForView
            {
                Image1 = this.ImageUrl
            };

            var product = new ProductForView
            {
                Description = this.Description,
                Cost = this.Cost,
                CategoriesId = this.SelectedCategory.CategoriesId,
                CategoryName = this.SelectedCategory.Name,
                Images = new List<ImageForView> { image },
                IsActive = true,
                SizeName = this.SelectedSize.Size1,        
                SizesId = this.SelectedSize.SizesId,
            };

            return product;

        }
        
        public override bool ValidateSave()
        {

            return
             !string.IsNullOrWhiteSpace(Description)
             && Cost > 0
             && !string.IsNullOrWhiteSpace(ImageUrl);
             //&& SelectedCategory != null
             //&& SelectedSize != null;
        }

        #region IDataErrorInfo Implementation

        public string Error { get { return null; } }

        public string this[string columnName]
        {

            get
            {
                switch (columnName)
                {

                    case nameof(Description):
                        return DescriptionError;
                    case nameof(Cost):
                        return CostError;
                    case nameof(SelectedCategory):
                        return SelectedCategoryError;
                    case nameof(ImageUrl):
                        return ImageUrlError;
                    case nameof(SelectedSize):
                        return SelectedSizeError;
                    default:
                        return null;
                }
            }
        }

        private string ValidateDescription()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                return "Description is required.";
            }
            return null;
        }

        private string ValidateCost()
        {
            if (Cost <= 0)
            {
                return "Cost must be greater than zero.";
            }
            return null;
        }
        private string ValidateSelectedCategory()
        {
            if (SelectedCategory == null)
            {
                return "Category is required.";
            }
            return null;
        }
        private string ValidateImageUrl()
        {
            if (string.IsNullOrWhiteSpace(ImageUrl))
            {
                return "Image URL is required.";
            }
            return null;
        }
        private string ValidateSelectedSize()
        {
            if (SelectedSize == null)
            {
                return "Size is required.";
            }
            return null;
        }

        private void ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Description):
                    DescriptionError = ValidateDescription();
                    break;
                case nameof(Cost):
                    CostError = ValidateCost();
                    break;
                case nameof(ImageUrl):
                    ImageUrlError = ValidateImageUrl();
                    break;
                //case nameof(SelectedCategory):
                //    SelectedCategoryError = ValidateSelectedCategory();
                //    break;
                //case nameof(SelectedSize):
                //    SelectedSizeError = ValidateSelectedSize();
                //    break;
            }
        }
        #endregion
        #region Error properties
        private string descriptionError;

        public string DescriptionError
        {
            get { return descriptionError; }
            set
            {
                if (descriptionError != value)
                {
                    // nameError = value;
                    SetProperty(ref descriptionError, value);
                    OnPropertyChanged();
                }
            }
        }

        private string imageUrlError;
        public string ImageUrlError
        {
            get { return imageUrlError; }
            set { SetProperty(ref imageUrlError, value); }
        }
        private string costError;
        public string CostError
        {
            get { return costError; }
            set
            {
                SetProperty(ref costError, value); OnPropertyChanged();
            }
        }



        private string selectedCategoryError;
        public string SelectedCategoryError
        {
            get { return selectedCategoryError; }
            set
            {
                SetProperty(ref selectedCategoryError, value); OnPropertyChanged();
            }
        }

        private string selectedSizeError;
        public string SelectedSizeError
        {
            get { return selectedSizeError; }
            set
            {
                SetProperty(ref selectedSizeError, value); OnPropertyChanged();
            }
        }

        #endregion
    }
}

