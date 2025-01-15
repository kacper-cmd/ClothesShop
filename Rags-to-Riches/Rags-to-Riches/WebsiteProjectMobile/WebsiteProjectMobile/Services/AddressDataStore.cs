using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Helpers;
using WebsiteProjectMobile.Models;
using Xamarin.Forms;

namespace WebsiteProjectMobile.Services
{
    public class AddressDataStore : ADataStore<AddressForView>
    {
        public AddressDataStore()
            : base()
        {
            items = websideServiceReference.AddressesAllAsync().GetAwaiter().GetResult().ToList();

        }

        public override async Task<AddressForView> AddItemToService(AddressForView item)
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Saved!", "Saved!", "OK");
                return await websideServiceReference.AddressesPOSTAsync(item);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error, no saving!", ex.Message, "OK");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override async Task<bool> DeleteItemFromService(AddressForView item)
        {
            return await websideServiceReference.AddressesDELETEAsync(item.AddressesId).HandleRequest();
        }

        public override AddressForView Find(AddressForView item)
        {
            return items.Where((AddressForView arg) => arg.AddressesId == item.AddressesId).FirstOrDefault();
        }

        public override AddressForView Find(int id)
        {
            return items.Where((AddressForView arg) => arg.AddressesId == id).FirstOrDefault();
        }

        public override async Task Refresh()
        {
            items = (await websideServiceReference.AddressesAllAsync()).ToList();
        }

        public override async Task<bool> UpdateItemInService(AddressForView item)
        {
            return await websideServiceReference.AddressesPUTAsync(item.AddressesId, item).HandleRequest();
        }
    }

}
