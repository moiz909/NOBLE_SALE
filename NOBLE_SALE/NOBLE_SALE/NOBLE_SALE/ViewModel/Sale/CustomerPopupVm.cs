using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Sale;
using NOBLE_SALE.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Sale
{
    class CustomerPopupVm : BaseViewModel
    {
        private SaleLookupModel _sale;

        public SaleLookupModel sale
        {
            get { return _sale; }
            set 
            { 
                _sale = value;
                OnPropertyChanged();
            }
        }

        private string _SearchString;

        public string SearchString
        {
            get { return _SearchString; }
            set 
            {
                _SearchString = value;
                OnPropertyChanged();
            }
        }

        private CashCustomerLookupModel _CashCustomer;

        public CashCustomerLookupModel CashCustomer
        {
            get { return _CashCustomer; }
            set 
            { 
                _CashCustomer = value;
                OnPropertyChanged();
            }
        }

        private string _Mobile;

        public string Mobile
        {
            get { return _Mobile; }
            set 
            { 
                _Mobile = value;
                OnPropertyChanged();
            }
        }


        private string _CustomerType;

        public string CustomerType
        {
            get { return _CustomerType; }
            set 
            { 
                _CustomerType = value;
                OnPropertyChanged();
            }
        }

        private string _CustomerId;

        public string CustomerId
        {
            get { return _CustomerId; }
            set 
            { 
                _CustomerId = value;
                OnPropertyChanged();
            }
        }


        private string _Email;

        public string Email
        {
            get { return _Email; }
            set 
            { 
                _Email = value;
                OnPropertyChanged();
            }
        }


        private Command<object> _FieldChange;
        public Command<object> FieldChange
        {
            get
            {
                return _FieldChange ?? (_FieldChange = new Command<object>(FieldChangedCommand));
            }
        }

        private PagedResult<List<ContactLookUpModel>> _CustomerList;

        public PagedResult<List<ContactLookUpModel>> CustomerList
        {
            get { return _CustomerList; }
            set 
            { 
                _CustomerList = value;
                OnPropertyChanged();
            }
        }

        private bool _IsEnable;

        public bool IsEnable
        {
            get { return _IsEnable; }
            set 
            {
                _IsEnable = value;
                OnPropertyChanged();
            }
        }

        private List<ContactLookUpModel> _List;

        public List<ContactLookUpModel> List
        {
            get { return _List; }
            set { _List = value; OnPropertyChanged(); }
        }



        public ContactLookUpModel SelectedCustomer { get; set; }



        public Command SearchHandler { get; set; }

        public Command SaveHandler { get; set; }


        public CustomerPopupVm()
        {
            UserData.Sale = new Model.Sale.SaleLookupModel();
            sale = new SaleLookupModel();
            SearchHandler = new Command(SearchCommand);
            CashCustomer = new CashCustomerLookupModel();
            CustomerList = new PagedResult<List<ContactLookUpModel>>();
            GetCustomer();
            SaveHandler = new Command(SaveCommand);
            IsEnable = true;
            List = new List<ContactLookUpModel>();
        }

        private async void SaveCommand(object obj)
        {
            UserData.Sale.CashCustomer = sale.CashCustomer;
            UserData.Sale.CustomerId = sale.CustomerId;
            UserData.Sale.CashCustomerId = CustomerId;
            UserData.Sale.Mobile = Mobile;
            UserData.Sale.Email = Email;
            sale.CustomerAddressWalkIn = string.Empty;
            await PopupNavigation.Instance.PopAsync();
        }

        public void FieldChangedCommand(object obj)
        {
            IsEnable = false;
            sale.CustomerId = SelectedCustomer.Id;
            sale.CashCustomer = SelectedCustomer.EnglishName;
            sale.CashCustomerId = CustomerId;
            sale.CustomerAddressWalkIn = string.Empty;
            if (SelectedCustomer != null)
            {
                IsEnable = false;
            }
        }

        private async void GetCustomer()
        {
            var service = new ProductService();
            CustomerList = await service.GetCustomers();
            
            foreach (var item in CustomerList.Results)
            {
                if(item.EnglishName == null || item.EnglishName == string.Empty)
                {
                    item.DisplayName = item.ArabicName;
                }
                else if(item.ArabicName != null || item.ArabicName != string.Empty)
                {
                    item.DisplayName = item.EnglishName;
                }
                else
                {
                    item.DisplayName = "Customer Name";
                }
            }

            List = CustomerList.Results;
        }

        private async void SearchCommand(object obj)
        {
            var service = new ProductService();
            CashCustomer = await service.GetCustomerSearch(SearchString);
            if (CashCustomer != null)
            {
                UserData.Sale.CashCustomer = CashCustomer.Name;
                UserData.Sale.Mobile = CashCustomer.Mobile;
                UserData.Sale.Email = CashCustomer.Email;
                UserData.Sale.CashCustomerId = CashCustomer.CashCustomerId;
                sale.CashCustomer = CashCustomer.Name;
                sale.Mobile = CashCustomer.Mobile;
                sale.Email = CashCustomer.Email;
                sale.CashCustomerId = CashCustomer.CustomerId;
                CustomerType = CashCustomer.Name;
                Mobile = CashCustomer.Mobile;
                Email = CashCustomer.Email;
                CustomerId = CashCustomer.CustomerId;
            }
            else
            {

                UserData.Sale.CashCustomer = "Walk-In";
                UserData.Sale.Mobile = string.Empty;
                UserData.Sale.Email = string.Empty;
                UserData.Sale.CashCustomerId = string.Empty;


                sale.CashCustomer = "Walk-In";
                sale.Mobile = string.Empty;
                sale.Email = string.Empty;
                sale.CashCustomerId = string.Empty;

                CustomerType = "Walk-In";
                Mobile = string.Empty;
                Email = string.Empty;
                CustomerId = string.Empty;
            }
        }
    }
}
