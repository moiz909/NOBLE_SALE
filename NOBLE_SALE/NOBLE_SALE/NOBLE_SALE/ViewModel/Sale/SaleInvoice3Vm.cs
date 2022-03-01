using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Model.Sale;
using NOBLE_SALE.PDFReports;
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

        private decimal _cash;

        public decimal cash
        {
            get { return _cash; }
            set 
            { 
                _cash = value;
                OnPropertyChanged();
            }
        }

        private decimal _bank;

        public decimal bank
        {
            get { return _bank; }
            set 
            {
                _bank = value;
                OnPropertyChanged();
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


        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private PaymentOptionsListModel _PaymentOption;

        public PaymentOptionsListModel PaymentOption
        {
            get { return _PaymentOption; }
            set 
            { 
                _PaymentOption = value;
                OnPropertyChanged();
            }
        }

        private bool _IsCash;

        public bool IsCash
        {
            get { return _IsCash; }
            set 
            { 
                _IsCash = value;
                OnPropertyChanged();
            }
        }

        private bool _IsBank;

        public bool IsBank
        {
            get { return _IsBank; }
            set 
            { 
                _IsBank = value;
                OnPropertyChanged();
            }
        }

        private bool _IsMasterCard;

        public bool IsMasterCard
        {
            get { return _IsMasterCard; }
            set 
            { 
                _IsMasterCard = value;
                OnPropertyChanged();
            }
        }

        private bool _IsVisa;

        public bool IsVisa
        {
            get { return _IsVisa; }
            set 
            { 
                _IsVisa = value;
                OnPropertyChanged();
            }
        }

        private bool _IsMada;

        public bool IsMada
        {
            get { return _IsMada; }
            set 
            { 
                _IsMada = value;
                OnPropertyChanged();
            }
        }
        private bool _IsStcPay;

        public bool IsStcPay
        {
            get { return _IsStcPay; }
            set 
            { 
                _IsStcPay = value;
                OnPropertyChanged();
            }
        }




        public Command CashHandler { get; set; }
        public Command BankHandler { get; set; }
        public Command MasterCardHandler { get; set; }
        public Command VisaHandler { get; set; }
        public Command MadaHandler { get; set; }
        public Command StcHandler { get; set; }








        public SaleInvoice3Vm(SaleLookupModel sale)
        {
            PaymentOption = new PaymentOptionsListModel();
            GetData();
            IsMasterCard = false;
            IsVisa = false;
            IsMada = false;
            IsStcPay = false;
            IsBank = false;
            IsCash = true;
            IsBusy = false;
            Sale = new SaleLookupModel();
            Payment = new PaymentTypeLookupModel();
            Sale = sale;
            due = new decimal();
            due = sale.SalePayment.DueAmount;
            SaveSaleHandler = new Command(SaveSaleCommand);
            CashHandler = new Command(CashCommand);
            BankHandler = new Command(BankCommand);
            MasterCardHandler = new Command(MasterCardCommand);
            VisaHandler = new Command(VisaCommand);
            MadaHandler = new Command(MadaCommand);
            StcHandler = new Command(StcCommand);
        }

        private void StcCommand(object obj)
        {
            IsStcPay = true;
            IsMada = false;
            IsMasterCard = false;
            IsVisa = false;
        }

        private void MadaCommand(object obj)
        {
            IsStcPay = false;
            IsMada = true;
            IsMasterCard = false;
            IsVisa = false;
        }

        private void VisaCommand(object obj)
        {
            IsStcPay = false;
            IsMada = false;
            IsMasterCard = false;
            IsVisa = true;
        }

        private void MasterCardCommand(object obj)
        {
            IsStcPay = false;
            IsMada = false;
            IsMasterCard = true;
            IsVisa = false;
        }

        private void BankCommand(object obj)
        {
            cash = 0;
            IsBank = true;
        }

        private void CashCommand(object obj)
        {
            bank = 0;
            IsBank = false;
        }

        private async void GetData()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var services = new ProductService();
            PaymentOption = await services.GetPaymentOptions();
            isBusy = false;
        }

        private async void SaveSaleCommand(object obj)
        {

            if (IsBusy)
                return;

            IsBusy = true;

            if (!IsBank)
            {
                Payment.Amount = recieved;
                Payment.Id = Guid.Empty;
                Payment.PaymentType = SalePaymentType.Cash;
                Sale.SalePayment.PaymentType = SalePaymentType.Cash;
            }
            else
            {
                Sale.SalePayment.PaymentType = SalePaymentType.Bank;
                if (IsMasterCard)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[3].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "MasterCard";
                }
                if (IsVisa)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[5].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "Visa";
                }
                if (IsMada)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[1].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "Mada";
                }
                if (IsStcPay)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[4].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "STC Pay";
                }
            }

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
            var pdfReport = new SalesReport(Sale.SaleItems);

            await pdfReport.CreateReport();
            //var response = await service.SaveSale(Sale);

            //if (response)
            //{
            //    await App.Current.MainPage.DisplayAlert("Message", "Sale Successfull", "ok");
            //    await Application.Current.MainPage.Navigation.PopAsync();
            //    await Application.Current.MainPage.Navigation.PopAsync();
            //    isBusy = false;
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "Contact Support", "ok");
            //    isBusy = false;
            //}
        }
    }
}
