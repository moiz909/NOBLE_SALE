using NOBLE_SALE.Enums;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Model.SetupSteps;
using NOBLE_SALE.Services;
using NOBLE_SALE.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.SetupSteps
{
    class SetupCurrencyVm : BaseViewModel
    {
        private CurrencyVm _Currency;

        public CurrencyVm Currency
        {
            get { return _Currency; }
            set { _Currency = value; OnPropertyChanged(); }
        }
        private TaxRateVm _Tax;

        public TaxRateVm Tax
        {
            get { return _Tax; }
            set { _Tax = value; OnPropertyChanged(); }
        }

        public StepsVm Steps { get; set; }



        public Command SaveCommand { get; set; }


        public SetupCurrencyVm()
        {
            Tax = new TaxRateVm();
            Currency = new CurrencyVm();
            Steps = new StepsVm();
            SaveCommand = new Command(SaveHandler);
        }

        private async void SaveHandler(object obj)
        {
            var service = new SetupService();
            Tax.NameArabic = string.Empty;
            Tax.Id = Guid.Empty;
            Tax.isActive = true;
            Tax.Setup = true;
            var response = await service.CreateVat(Tax);
            if (response)
            {
                Currency.Id = Guid.Empty;
                Currency.NameArabic = string.Empty;
                Currency.IsActive = true;
                Currency.Image = string.Empty;
                Currency.ArabicSign = string.Empty;
                Currency.Setup = true;
                response = await service.CreateCurrency(Currency);
                if (response)
                {
                    UserData.CurrencyandVat = false;
                    Steps.CompanyId = UserData.Current.CompanyId;
                    Steps.Step3 = true;
                    Steps.Step4 = true;
                    response = await service.UpdateSteps(Steps);
                    if (response)
                    {
                        response = await service.CreateChartofAccount(TemplateType.Business);
                        if (response)
                        {
                            Steps.CompanyId = UserData.Current.CompanyId;
                            Steps.Step1 = true;
                            response = await service.UpdateSteps(Steps);
                            if (response)
                            {
                                await Application.Current.MainPage.Navigation.PopAsync();
                                await Application.Current.MainPage.Navigation.PopAsync();
                                await Application.Current.MainPage.Navigation.PushAsync(new SetupPage());
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                            }
                        }
                        
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                    }

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
            }

        }
    }
}
