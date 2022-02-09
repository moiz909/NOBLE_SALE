using NOBLE_SALE.Enums;
using NOBLE_SALE.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.SetupSteps
{
    class CoaTemplateVm : BaseViewModel
    {
        public string SelectedCoa { get; set; }
        public Command SaveCommand { get; set; }
        public CoaTemplateVm()
        {
            SaveCommand = new Command(SaveHandler);
        }

        private async void SaveHandler(object obj)
        {
            if(SelectedCoa!= string.Empty)
            {
                var service = new SetupService();
                var response = await service.CreateChartofAccount(TemplateType.Business);
                if (response)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                }
            }
        }
    }
}
