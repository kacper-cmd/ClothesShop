using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;

namespace WebsiteProjectMobile.Services
{
    public abstract class ADataStore<T> : IDataStore<T> where T : class
    {
        public List<T> items;
        protected readonly WebsideProjectMobileService websideServiceReference;
        public ADataStore()
        {
            var handler = new HttpClientHandler();
#if DEBUG
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
#endif
            var client = new HttpClient(handler);
            websideServiceReference = new WebsideProjectMobileService("https://localhost:7252/", client);

        }
        public async Task<bool> AddItemAsync(T item)
        {
            await AddItemToService(item);
            await Refresh();
            return await Task.FromResult(true);
        }
        public abstract T Find(T item);
        public abstract T Find(int id);
        public abstract Task Refresh();
        public abstract Task<bool> DeleteItemFromService(T item);
        public abstract Task<bool> UpdateItemInService(T item);
        public abstract Task<T> AddItemToService(T item);

        public async Task<bool> UpdateItemAsync(T item)
        {
            var oldItem = Find(item);
            items.Remove(oldItem);
            items.Add(item);

            await UpdateItemInService(item);//call the api 
            return await Task.FromResult(true);
        }

        
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = Find(id);
            await DeleteItemFromService(oldItem);
            await Refresh();

            return await Task.FromResult(true);
        }

        public async Task<T> GetItemAsync(int id)
        {
            return await Task.FromResult(Find(id));
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            await Refresh();
            return await Task.FromResult(items);
        }

     
    }
}
