using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Models;
using WebsiteProjectMobile.Services;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    public class OrderSummaryViewModel : BaseViewModel
    {
        private OrderForView order;
        private OrderDataStore DataStore => DependencyService.Get<OrderDataStore>();
        public Command ConfirmOrderCommand { get; }
        public OrderSummaryViewModel()
        {
          //  LoadOrderId();
            Order = new OrderForView
            {
                OrderId = GenerateOrderId(),
                //User = new UserFor(),
                // OrderStatus = new OrderStatus(),
                //OrderProducts = new List<OrderProduct>()
            };

            ConfirmOrderCommand = new Command(ConfirmOrder);
        }
        private int GenerateOrderId()
        {
            Random random = new Random();
            return random.Next(1000, 10000);
        }
        private void ConfirmOrder()
        {
            //Order.UserId.Username = Username;
            //Order.User.Addresses = new Address
            //{
            //    Street = Address,
            //    City = City,
            //    ZipCode = ZipCode
            //};
            //   Application.Current.MainPage.DisplayAlert("Order Confirmation", message, "OK");
        }

        private async void LoadOrderId()
        {
            try
            {
                var item = await DataStore.GetItemsAsync();
                
                
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public OrderForView Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }


        private int orderId;
        public int OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                orderId = value;
                OnPropertyChanged();

            }
        }
        private decimal _totalCost;
        public decimal TotalCost
        {
            get { return _totalCost; }
            set { _totalCost = value; OnPropertyChanged(); }
        }
        private DateTime _orderDate;
        private DateTime _deliveryDate;
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; OnPropertyChanged(); }
        }

        public DateTime DeliveryDate
        {
            get { return _deliveryDate; }
            set { _deliveryDate = value; OnPropertyChanged(); }
        }
        private string _orderStatus;
        public string OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; OnPropertyChanged(); }
        }

        private string username;
        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

     

  

        
    }
}
