using NOBLE_SALE.Model.Sale;
using System;
using System.Collections.Generic;
using System.Text;

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




        public SaleInvoice3Vm()
        {
            due = 500;
        }

        //public SaleInvoice3Vm(SaleLookupModel sale)
        //{
        //    due = new decimal();
        //    due = sale.SalePayment.DueAmount;
        //}
    }
}
