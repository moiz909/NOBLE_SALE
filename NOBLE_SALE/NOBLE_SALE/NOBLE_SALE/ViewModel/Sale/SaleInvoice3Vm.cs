using NOBLE_SALE.Model.Sale;
using NOBLE_SALE.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Sale
{
    class SaleInvoice3Vm : BaseViewModel
    {

        private SaleLookupModel _Sale;

        public SaleLookupModel Sale
        {
            get { return _Sale; }
            set 
            {
                _Sale = value;
                OnPropertyChanged();
            }
        }

        private SalePaymentLookupModel _SalePayment;

        public SalePaymentLookupModel SalePayment
        {
            get { return _SalePayment; }
            set 
            {
                _SalePayment = value;
                OnPropertyChanged();
            }
        }
        public Command SaveSaleHandler { get; set; }

        private decimal _due;

        public decimal due
        {
            get { return _due; }
            set 
            { 
                _due = value;
                OnPropertyChanged();
            }
        }

        private decimal _balance;

        public decimal balance
        {
            get { return due-recieved; }
            //set 
            //{ 
            //    _balance = value;
            //    OnPropertyChanged();
            //}
        }

        private decimal _recieved;

        public decimal recieved
        {
            get { return _recieved; }
            set
            {
                _recieved = value;
                OnPropertyChanged("recieved");
                OnPropertyChanged("balance");
            }
        }

        private decimal _change;

        public decimal change
        {
            get { return _change; }
            set
            {
                _change = value;
                OnPropertyChanged();
            }
        }
        public PaymentTypeLookupModel Payment { get; set; }






        public SaleInvoice3Vm(SaleLookupModel sale)
        {
            Sale = new SaleLookupModel();
            Payment = new PaymentTypeLookupModel();
            Sale = sale;
            due = new decimal();
            due = sale.SalePayment.DueAmount;
            SaveSaleHandler = new Command(SaveSaleCommand);
        }

        private async void SaveSaleCommand(object obj)
        {
            Payment.Amount = recieved;
            Payment.Id = Guid.Empty;
            Payment.PaymentType = SalePaymentType.Cash;

            if (balance < 0)
            {
                Sale.Change = balance * -1;
            }
            else
            {
                Sale.Change = balance;
            }
            Sale.SalePayment.PaymentTypes = new List<PaymentTypeLookupModel>();
            Sale.SalePayment.PaymentTypes.Add(Payment);

            var service = new ProductService();

            var response = await service.SaveSale(Sale);

            if (response)
            {
                await App.Current.MainPage.DisplayAlert("Message", "Sale Successfull", "ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Contact Support", "ok");
            }
        }
    }
}
