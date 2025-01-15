using System;
using System.Collections.Generic;
using System.ComponentModel;
using WebsiteProjectMobile.Models;
using WebsiteProjectMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebsiteProjectMobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}