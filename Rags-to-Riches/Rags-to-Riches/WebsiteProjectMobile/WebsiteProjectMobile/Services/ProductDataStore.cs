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

    public class ProductDataStore : ADataStore<ProductForView>
    {
        public ProductDataStore()
            : base()
        {
            items = websideServiceReference.ProductAllAsync().GetAwaiter().GetResult().ToList();
        }

        public override async Task<ProductForView> AddItemToService(ProductForView item)
        {
            try
            {
                return await websideServiceReference.ProductPOSTAsync(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override async Task<bool> DeleteItemFromService(ProductForView item)
        {
            return await websideServiceReference.ProductDELETEAsync(item.ProductId).HandleRequest();
        }

        public override ProductForView Find(ProductForView item)
        {
            return items.Where((ProductForView arg) => arg.ProductId == item.ProductId).FirstOrDefault();
        }

        public override ProductForView Find(int id)
        {
            return items.Where((ProductForView arg) => arg.ProductId == id).FirstOrDefault();
        }

        public override async Task Refresh()
        {
            items = (await websideServiceReference.ProductAllAsync()).ToList();
        }

        public override async Task<bool> UpdateItemInService(ProductForView item)
        {
            return await websideServiceReference.ProductPUTAsync(item.ProductId, item).HandleRequest();
        }
    }

}


