using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Helpers;
using WebsiteProjectMobile.Models;

namespace WebsiteProjectMobile.Services
{
  
    public class SizeDataStore : ADataStore<SizeForView>
    {
        public SizeDataStore()
            : base()
        {
            items = websideServiceReference.SizeAllAsync().GetAwaiter().GetResult().ToList();

        }

        public override async Task<SizeForView> AddItemToService(SizeForView item)
        {
            try
            {
                return await websideServiceReference.SizePOSTAsync(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override async Task<bool> DeleteItemFromService(SizeForView item)
        {
            return await websideServiceReference.SizeDELETEAsync(item.SizesId).HandleRequest();
        }

        public override SizeForView Find(SizeForView item)
        {
            return items.Where((SizeForView arg) => arg.SizesId == item.SizesId).FirstOrDefault();
        }

        public override SizeForView Find(int id)
        {
            return items.Where((SizeForView arg) => arg.SizesId == id).FirstOrDefault();
        }

        public override async Task Refresh()
        {
            items = (await websideServiceReference.SizeAllAsync()).ToList();
        }

        public override async Task<bool> UpdateItemInService(SizeForView item)
        {
            return await websideServiceReference.SizePUTAsync(item.SizesId, item).HandleRequest();
        }
    }

}


