using Newtonsoft.Json;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Model.Sale;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NOBLE_SALE.Services
{
    class ProductService
    {
        public string url;
        public HttpClient client;

        public async Task<PagedResult<ProductListModel>> GetProducts(Guid? CategoryId, string SearchTerm, Guid? WarehouseId)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url+= "Product/GetProductInformationForTouch?searchTerm=" + SearchTerm + "&wareHouseId=" + WarehouseId + "&categoryId=" + CategoryId  ;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var Products = JsonConvert.DeserializeObject<PagedResult<ProductListModel>>(response);
                return Products;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<string> GetHTML()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Account/SendPDF";

            try
            {
                var response = await client.GetStringAsync(url);
                return response;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<SaleDetailLookupModel> GetSaleDetail(Guid id)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Sale/SaleDetail?id=" + id ;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var SaleDetail = JsonConvert.DeserializeObject<SaleDetailLookupModel>(response);
                return SaleDetail;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }


        public async Task<CashCustomerLookupModel> GetCustomerSearch(string term)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Sale/SearchCashCustomer?search=" + term;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var CashCustomer = JsonConvert.DeserializeObject<CashCustomerLookupModel>(response);
                return CashCustomer;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<PagedResult<CategoryListModel>> GetCategories()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/GetCategoryInformation?isActive=" + true;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var Categories = JsonConvert.DeserializeObject<PagedResult<CategoryListModel>>(response);
                return Categories;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<PagedResult<List<ContactLookUpModel>>> GetCustomers()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;

           // "Product/GetCategoryInformation?isActive=" + isActive + "&pageNumber=" + PageNumber + "&searchTerm=" + searchTerm;


            url += "Contact/ContactList?isCustomer=" + true +  "&searchTerm=" + null + "&pageNumber=" + null + "&IsDropDown=" +true + "&IsActive=" + true + "&status=" + false + "&paymentTerms=" + null;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var Customers = JsonConvert.DeserializeObject<PagedResult<List<ContactLookUpModel>>>(response);
                return Customers;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }



        public async Task<TaxRateListModel> GetTax()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/TaxRateList" ;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var TaxList = JsonConvert.DeserializeObject<TaxRateListModel>(response);
                return TaxList;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<string> GetProductCode()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/ProductAutoGenerateCode";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                return response;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<PaymentOptionsListModel> GetPaymentOptions()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/PaymentOptionsList?isActive=" + true;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var paymentoptions = JsonConvert.DeserializeObject<PaymentOptionsListModel>(response);
                return paymentoptions;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<RegistrationNoLookUp> GetRegistrationNoDetail()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Sale/SaleAutoGenerateNo";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var AutoNoDetail = JsonConvert.DeserializeObject<RegistrationNoLookUp>(response);
                return AutoNoDetail;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }

        public async Task<PagedResult<CategoryListModel>> GetCategories(bool isActive, int PageNumber, string searchTerm)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/GetCategoryInformation?isActive=" + isActive + "&pageNumber=" + PageNumber + "&searchTerm=" + searchTerm ;
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.GetStringAsync(url);
                var Categories = JsonConvert.DeserializeObject<PagedResult<CategoryListModel>>(response);
                return Categories;
            }
            catch (Exception E)
            {
                await Application.Current.MainPage.DisplayAlert("errorss", E.Message, "ok");
                return null;
            }
        }


        public async Task<bool> SaveProduct(ProductVm model)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Product/SaveProductInformation";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(model));
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




        public async Task<string> SaveSale(SaleLookupModel model)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Sale/SaveSaleInformation";
            var token = UserData.Current.Token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(model));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            try
            {
               
                var response = await client.PostAsync(url, contents);

                string content =  await response.Content.ReadAsStringAsync();
                var result = await Task.Run(() => JsonConvert.DeserializeObject<Root>(content));
                if (response.ReasonPhrase == "OK")
                    
                return result.message.id;

                else
                    return null;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Message", ex.Message, "ok");
                return null;
            }
        }
    }
}
