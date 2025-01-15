
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WebsiteProjectMobile.Services;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    public abstract class AListViewModel<T> : BaseViewModel
    {
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>(); private T _selectedItem;
        public ObservableCollection<T> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<T> ItemTapped { get; }
        public Command<T> EditItemCommand { get; }


        public AListViewModel(string title)
        {
            Title = title;
            Items = new ObservableCollection<T>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<T>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            EditItemCommand = new Command<T>(OnEditItem);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = default(T);
        }

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public abstract void GoToAddPage();
        public async void OnAddItem(object obj)
        {
            GoToAddPage();
        }
        public async void OnEditItem(T item)
        {
            if (item == null)
                return;
            GoToEditPage(item);
            // Implement your edit logic here
        }

        public abstract void GoToEditPage(T item);
       

        async void OnItemSelected(T item)
        {
            if (item == null)
                return;
            //await Shell.Current.GoToAsync($"{nameof(KlientDetailPage)}?{nameof(KlientDetailViewModel.ItemId)}={item.IdKlienta}");
        }
    }
}
