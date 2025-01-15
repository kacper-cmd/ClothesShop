using System;
using System.Collections.Generic;
using System.Text;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Services;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    public class OrderViewModel : AListViewModel<OrderForView> 
    {
       // public OrderDataStore orderDataStore;
        public OrderViewModel() : base("Order")
        {
            //orderDataStore = DependencyService.Get<OrderDataStore>();

        }
        public override void GoToAddPage()
        {
            throw new NotImplementedException();
        }

        public override void GoToEditPage(OrderForView item)
        {
            throw new NotImplementedException();
        }
    }
}
