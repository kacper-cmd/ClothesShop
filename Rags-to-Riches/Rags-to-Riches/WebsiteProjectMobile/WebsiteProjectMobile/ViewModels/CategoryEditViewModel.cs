using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using WebsideProjectMobile.Service.Reference;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels 
{
    [QueryProperty(nameof(CategoriesId), nameof(CategoriesId))]
    public class CategoryEditViewModel : AEditItemViewModel<CategoryForView>
    {
        private CategoryForView _category;
        public CategoryForView Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        private int _categoriesId;
        public int CategoriesId
        {
            get
            {
                return _categoriesId;
            }
            set
            {
                //_categoriesId = value;
                //LoadItemId(value);
                if (_categoriesId != value)
                {
                    _categoriesId = value;
                    LoadItemId(value);
                }
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                // var item = DataStore.UpdateItemAsync();
                CategoryForView item2 = await DataStore.GetItemAsync(itemId);
                CategoriesId = item2.CategoriesId;
                Name = item2.Name;
                IsActive = item2.IsActive;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    // name = value;
                    SetProperty(ref name, value);
                   

                }
            }
        }
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { SetProperty(ref isActive, value); }
        }

        public override bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name);
        }

        public override CategoryForView SetItem(CategoryForView existingItem)
        {


            CategoryForView updatedItem = new CategoryForView
            {
                CategoriesId = existingItem.CategoriesId,
                Name = Name,
                IsActive = IsActive
            };
            OnPropertyChanged(nameof(Category));  // Notify property change for Category object

            return updatedItem;
        }

        protected override int SetItemId()
        {
            return CategoriesId;
        }

        //public ICommand SaveCommand { get; }
        //public override Category SetItem()
        //{
        //    Category item = new Category
        //    {
        //        CategoriesId = this.CategoriesId,
        //        Name = this.Name,
        //        IsActive = this.IsActive
        //    };
        //    return item;

        //}




    }
}
