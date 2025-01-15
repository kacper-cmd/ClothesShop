using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Models;
using WebsiteProjectMobile.Views;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    public class CategoryViewModel :AListViewModel<CategoryForView>
    {
        public Command EditItemCommand { get; }

        public CategoryViewModel() : base("Category")
        {
            Title = "Category";
            EditItemCommand = new Command<CategoryForView>(GoToEditPage);
        }

        public override async void GoToAddPage()
        {
            await Shell.Current.GoToAsync(nameof(NewCategoryPage));
        }

        public override async void GoToEditPage(CategoryForView item)
        {
            await Shell.Current.GoToAsync($"{nameof(CategoryEditPage)}?{nameof(CategoryEditViewModel.CategoriesId)}={item.CategoriesId}");
        }
      
    }
}
