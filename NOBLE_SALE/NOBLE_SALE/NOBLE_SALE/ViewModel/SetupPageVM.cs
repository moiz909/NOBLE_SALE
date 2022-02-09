using NOBLE_SALE.Helper;
using NOBLE_SALE.View;
using NOBLE_SALE.View.SetupSteps;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel
{
    class SetupPageVM : BaseViewModel
    {
        public Command ChartofAccountCommand { get; set; }
        public Command SetupCompanyCommand { get; set; }
        public Command SetupCurrencyCommand { get; set; }
        public Command SetupVatCommand { get; set; }
        public Command SetupYearCommand { get; set; }
        public Command SaveCommand { get; set; }

        public SetupPageVM()
        {
            ChartofAccountCommand = new Command(ChartofAccountHandler);
            SetupCurrencyCommand = new Command(SetupCurrencyHandler);
            SetupCompanyCommand = new Command(SetupCompanyHandler);
            SaveCommand = new Command(SaveHandler);
        }

        private async void SaveHandler(object obj)
        {
            if(!UserData.CurrencyandVat && !UserData.CompanySetup)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Dashboard());
            }
        }

        private async void SetupCompanyHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SetupCompanyView());
        }

        private async void SetupCurrencyHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SetupCurrencyView());
        }


        private async void ChartofAccountHandler(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CoaTemplate());
        }
    }
}
