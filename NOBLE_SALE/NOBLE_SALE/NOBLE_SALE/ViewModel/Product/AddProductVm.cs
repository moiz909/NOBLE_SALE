using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Product
{
    class AddProductVm : BaseViewModel
    {

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }

        }
        private bool _IsValidName;

        public bool IsValidName
        {
            get { return _IsValidName; }
            set 
            {
                _IsValidName = value;
                OnPropertyChanged();
            }
        }

        private bool _IsValidCategory;

        public bool IsValidCategory
        {
            get { return _IsValidCategory; }
            set 
            { 
                _IsValidCategory = value;
                OnPropertyChanged();
            }
        }


        private string _Code;

        public string Code
        {
            get { return _Code; }
            set 
            { 
                _Code = value;
                OnPropertyChanged();
            }
        }

        private ProductVm _Product;

        public ProductVm Product
        {
            get { return _Product; }
            set 
            { 
                _Product = value;
                OnPropertyChanged();
            }
        }
        private PagedResult<CategoryListModel> _Categories;

        public PagedResult<CategoryListModel> Categories
        {
            get { return _Categories; }
            set 
            { 
                _Categories = value;
                OnPropertyChanged();
            }
        }

        private TaxRateListModel _TaxRateList;
        public TaxRateListModel TaxRateList
        {
            get { return _TaxRateList; }
            set { _TaxRateList = value; OnPropertyChanged(); }
        }

        public CategoryLookUpModel SelectedCategory { get; set; }
        public Command SaveProductHandler { get; set; }


        public AddProductVm()
        {
            IsBusy = false;
            Product = new ProductVm();
            IsValidName = true;
            IsValidCategory = true;
            Categories = new PagedResult<CategoryListModel>();
            TaxRateList = new TaxRateListModel();
            SaveProductHandler = new Command(SaveProductCommand);
            GetData();
        }

        private async void SaveProductCommand(object obj)
        {

            if (Product.EnglishName == null || Product.EnglishName == string.Empty || Product.EnglishName == " ")
            {
                IsValidName = false;
            }
            else if(SelectedCategory == null)
            {
                IsValidCategory = false;
            }

            else
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                IsValidCategory = true;
                Product.Assortment = string.Empty;
                Product.BarCode = string.Empty;
                Product.BasicUnit = string.Empty;
                Product.CategoryId = SelectedCategory.Id;
                Product.Id = Guid.Empty;
                Product.Image = string.Empty;
                Product.IsActive = true;
                Product.Length = "1";
                Product.SalePriceUnit = string.Empty;
                Product.SaleReturnDays = "0";
                Product.StyleNumber = string.Empty;
                Product.TaxMethod = TaxRateList.TaxMethod;
                Product.TaxRateId = TaxRateList.TaxRates[0].Id;
                Product.Width = "1";
                Product.ArabicName = Product.EnglishName;

                var service = new ProductService();

                var response = await service.SaveProduct(Product);

                if (response)
                {
                    await App.Current.MainPage.DisplayAlert("Message", "Saved Successfull", "ok");
                    IsBusy = false;
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Contact Support", "ok");
                    IsBusy = false;
                }
            }
        }

        private async void GetData()
        {

            if (IsBusy)
                return;

            IsBusy = true;
            var service = new ProductService();
            Code = await service.GetProductCode();
            Categories = await service.GetCategories();
            TaxRateList = await service.GetTax();

            IsBusy = false;
        }
    }
}
