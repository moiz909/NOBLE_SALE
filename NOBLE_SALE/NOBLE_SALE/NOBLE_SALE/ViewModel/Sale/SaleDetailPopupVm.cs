using NOBLE_SALE.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Sale
{
     class SaleDetailPopupVm : BaseViewModel
    {
        private decimal _Due;

        public decimal Due
        {
            get { return _Due; }
            set 
            { 
                _Due = value;
                OnPropertyChanged();
            }
        }

        private decimal _Recieved;

        public decimal Recieved
        {
            get { return _Recieved; }
            set 
            { 
                _Recieved = value;
                OnPropertyChanged();
            }
        }

        private decimal _Balance;

        public decimal Balance
        {
            get { return _Balance; }
            set 
            { 
                _Balance = value;
                OnPropertyChanged();
            }
        }

        public string HtmlString { get; set; }

        private PDFToHtml PDFToHtml { get; set; }

        public Command OkBtn { get; set; }


        public SaleDetailPopupVm(decimal due, decimal recieved, decimal balance, string html)
        {
            OkBtn = new Command(OkBtnCOmmand);
            Due = due;
            Recieved = recieved;
            Balance = balance;
            HtmlString = html;
        }
        
        private async void OkBtnCOmmand(object obj)
        {

            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PopAsync();
            PDFToHtml = new PDFToHtml();
            PDFToHtml.HTMLString = HtmlString;

            PDFToHtml.GeneratePDF();
            
        }
    }
}
