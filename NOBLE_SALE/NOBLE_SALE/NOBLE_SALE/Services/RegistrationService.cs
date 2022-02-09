using Newtonsoft.Json;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Model.Company;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NOBLE_SALE.Services
{
    class RegistrationService
    {
        public string url;
        public HttpClient client;

        public async Task<string> RegisterLocation(BusinessVm business)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiNjczZmUxODItMTE3ZS00MjE5LTg0NGUtYzUxYWUwOGU2YmI4IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjQ2OTE3NTM2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.GvXya2vcWViVRbquoNMhaTjobJu48zA9JyW1dlWIWu0";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            url += "Company/SaveBusiness";

            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(business));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");


            try
            {
                var response = await client.PostAsync(url, contents);
                //var details = JsonConvert.DeserializeObject<PagedResult<ObservableCollection<InventoryLookUpModel>>>(response);
                return "HELLO";
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("errorss", e.Message, "ok");
                Console.WriteLine(e.Message);
                return null;
            }




        }
    }
}
