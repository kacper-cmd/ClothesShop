using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Helpers;
using Xamarin.Forms;

namespace WebsiteProjectMobile.Services
{
    public class CategoryDataStore : ADataStore<CategoryForView>
    {
        public CategoryDataStore()
            : base()
        {
            items = websideServiceReference.CategoryAllAsync().GetAwaiter().GetResult().ToList();

        }

        public override async Task<CategoryForView> AddItemToService(CategoryForView item)
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Saved!", "Saved!", "OK");
                return await websideServiceReference.CategoryPOSTAsync(item);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error, no saving!", ex.Message, "OK");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override async Task<bool> DeleteItemFromService(CategoryForView item)
        {
            return await websideServiceReference.CategoryDELETEAsync(item.CategoriesId).HandleRequest();
        }

        public override CategoryForView Find(CategoryForView item)
        {
            return items.Where((CategoryForView arg) => arg.CategoriesId == item.CategoriesId).FirstOrDefault();
        }

        public override CategoryForView Find(int id)
        {
            return items.Where((CategoryForView arg) => arg.CategoriesId == id).FirstOrDefault();
        }

        public override async Task Refresh()
        {
            items = (await websideServiceReference.CategoryAllAsync()).ToList();
        }

        public override async Task<bool> UpdateItemInService(CategoryForView item)
        {
            return await websideServiceReference.CategoryPUTAsync(item.CategoriesId, item).HandleRequest();
        }
    }

}
