using Newtonsoft.Json;
using NOBLE_SALE.Enums;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Model.Company;
using NOBLE_SALE.Model.SetupSteps;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NOBLE_SALE.Services
{
    class SetupService
    {
        private string url;
        private HttpClient client;

        public async Task<bool> AcceptTermsAndConditions()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Company/TermsAndConditionAgreed?companyId="+ UserData.Current.CompanyId + "&termsCondition=" + true;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client = new WebAPI().client;
            try
            {
                var response = await client.GetStringAsync(url);

                
                    return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", ex.Message, "ok");
                return false;
            }
        }

        public async Task<bool> SetFinancialYear()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Company/AddFinancialYear?year=" + "2022-2022" + "&month=" + 0;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);


                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", ex.Message, "ok");
                return false;
            }
        }

        public async Task<bool> CreateCurrency(CurrencyVm currency)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/SaveCurrency";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(currency));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            try
            {
                var response = await client.PostAsync(url, contents);
                if (response.ReasonPhrase == "OK")
                    return true;

                else
                    return false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Message", ex.Message, "ok");
                return false;
            }
        }

        public async Task<bool> CreateVat(TaxRateVm tax)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/SaveTaxRate";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(tax));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            try
            {
                var response = await client.PostAsync(url, contents);
                if (response.ReasonPhrase == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Message", ex.Message, "ok");
                return false;
            }
        }

        public async Task<bool> UpdateCompany(NOBLE_SALE.Model.SetupSteps.BusinessVm business)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Company/SaveLocation";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(business));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            try
            {
                var response = await client.PostAsync(url, contents);
                if (response.ReasonPhrase == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Message", ex.Message, "ok");
                return false;
            }
        }


        public async Task<bool> UpdateSteps(NOBLE_SALE.Model.SetupSteps.StepsVm steps)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "account/SetupUpdateInCompany";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(steps));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            try
            {
                var response = await client.PostAsync(url, contents);
                if (response.ReasonPhrase == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Message", ex.Message, "ok");
                return false;
            }
        }

        public async Task<CompanyDto> GetCompanyDetail(Guid id)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Company/EditCompany?Id=" + id;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var Company = JsonConvert.DeserializeObject<CompanyDto>(response);
                return Company;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }


        public async Task<bool> CreateChartofAccount(TemplateType template)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Accounting/TemplateAccountSetup?template=" + template;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);


                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", ex.Message, "ok");
                return false;
            }
        }

    }
}
