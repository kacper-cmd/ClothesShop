using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WebsideProjectMobile.Service.Reference;

namespace WebsiteProjectMobile.ViewModels
{
    public class NewCategoryViewModel : ANewItemViewModel<CategoryForView>, IDataErrorInfo
    {
        private string name;
        private bool isActive;
        //private int idCategory;

        //public int IdCategory
        //{
        //    get => idCategory;
        //    set => SetProperty(ref idCategory, value);
        //}
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {

                    SetProperty(ref isActive, value);
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    // name = value;
                    SetProperty(ref name, value);
                    ValidateProperty(nameof(Name));

                }
            }
        }
        private void ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(Name))
            {
                NameError = ValidateName();
            }
        }
       
        public override bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name);

        }
        private string ValidateName()
        {

            if (string.IsNullOrWhiteSpace(Name))
            {
                return "Name is required.";
            }

            return null;
        }
        private string nameError;

        public string NameError
        {
            get { return nameError; }
            set
            {
                if (nameError != value)
                {
                    // nameError = value;
                    SetProperty(ref nameError, value);

                    OnPropertyChanged();
                }
            }
        }
        //https://learn.microsoft.com/pl-pl/dotnet/desktop/wpf/data/how-to-implement-validation-logic-on-custom-objects?view=netframeworkdesktop-4.8
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Name))
                {
                    return ValidateName();
                }

                return null;
            }
        }

        public override CategoryForView SetItem()
        {
            return new CategoryForView
            {
                Name = this.Name,
                IsActive = this.IsActive
            };
        }

       
    }
}
