using NOBLE_SALE.Services;
using NOBLE_SALE.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel
{
     class TermsAndConditionVM : BaseViewModel
    {
        public Command AcceptTermsCommand { get; set; }
        public TermsAndConditionVM()
        {
            AcceptTermsCommand = new Command(AcceptTermsHandler);
        }

        private async void AcceptTermsHandler(object obj)
        {
            var service = new SetupService();
            var response = await service.AcceptTermsAndConditions();
            if (response)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new SetupPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
            }
        }
    }
}
