using WebsiteProjectMobile.Services;
using Xamarin.Forms;

namespace WebsiteProjectMobile.ViewModels
{
    public abstract class AEditItemViewModel<T> : BaseViewModel
    {
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        public AEditItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        public abstract bool ValidateSave();
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public abstract T SetItem(T existingItem);// public abstract T SetItem();
        private async void OnSave()
        {
            // Retrieve the existing item from the DataStore
            T existingItem = await DataStore.GetItemAsync(SetItemId());

            // Call the SetItem method to update the existing item
            T updatedItem = SetItem(existingItem);

            // Update the item in the DataStore
            await DataStore.UpdateItemAsync(updatedItem);
           // await DataStore.UpdateItemAsync(SetItem());
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        protected abstract int SetItemId();

    }
}
