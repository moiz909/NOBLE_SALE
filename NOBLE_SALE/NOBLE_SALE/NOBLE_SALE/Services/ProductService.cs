using Newtonsoft.Json;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Product;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NOBLE_SALE.Services
{
    class ProductService
    {
        public string url;
        public HttpClient client;

        public async Task<PagedResult<ProductListModel>> GetProducts(Guid? CategoryId, string SearchTerm, Guid WarehouseId, int PageNumber)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url+= "Product/GetProductInformation?categoryId=" + CategoryId + "&searchTerm=" + SearchTerm + "&wareHouseId=" + WarehouseId + "&pageNumber=" + PageNumber + "&pageSize=" + 20 + "&isDropdown=" + true;
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
    }
}
