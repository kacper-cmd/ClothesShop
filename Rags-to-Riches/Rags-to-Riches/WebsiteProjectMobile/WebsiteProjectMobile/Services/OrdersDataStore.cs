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

    public class OrderDataStore : ADataStore<OrderForView>
    {
        public OrderDataStore()
            : base()
        {
            items = websideServiceReference.OrdersAllAsync().GetAwaiter().GetResult().ToList();

        }

        public override async Task<OrderForView> AddItemToService(OrderForView item)
        {
            try
            {
                return await websideServiceReference.OrdersPOSTAsync(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override async Task<bool> DeleteItemFromService(OrderForView item)
        {
            return await websideServiceReference.OrdersDELETEAsync(item.OrderId).HandleRequest();
        }

        public override OrderForView Find(OrderForView item)
        {
            return items.Where((OrderForView arg) => arg.OrderId == item.OrderId).FirstOrDefault();
        }

        public override OrderForView Find(int id)
        {
            return items.Where((OrderForView arg) => arg.OrderId == id).FirstOrDefault();
        }

        public override async Task Refresh()
        {
            items = (await websideServiceReference.OrdersAllAsync()).ToList();
        }

        public override async Task<bool> UpdateItemInService(OrderForView item)
        {
            return await websideServiceReference.OrdersPUTAsync(item.OrderId, item).HandleRequest();
        }
    }

}


