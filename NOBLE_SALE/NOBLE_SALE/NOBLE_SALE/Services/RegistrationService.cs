
using Newtonsoft.Json;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Model.Company;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        
        public async Task<RegisterLocationResultModel> RegisterLocation(BusinessVm business)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiNjczZmUxODItMTE3ZS00MjE5LTg0NGUtYzUxYWUwOGU2YmI4IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjQ2OTE3NTM2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.GvXya2vcWViVRbquoNMhaTjobJu48zA9JyW1dlWIWu0";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            business.BusinessParentId = Guid.Parse("5103CFD5-CD6E-4B84-0245-08D9BADEA086");
            business.ClientParentId = Guid.Parse("FC022686-B15E-4AE7-0244-08D9BADEA086");

            UserData.BusinessParentId = business.BusinessParentId;
            UserData.ClientParentId = business.ClientParentId;

            business.AddressInArabic = "";
            business.CityInArabic = "";
            business.CityInEnglish = "";
            business.CountryInEnglish = "";
            business.CountryInArabic = "";
            business.Id = "";
            business.LandLine = "";
            business.NameInArabic = "";
            business.Website = "";

            url += "Company/SaveLocation";

            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(business));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");


            try
            {
                var response = await client.PostAsync(url, contents);
                //var details = JsonConvert.DeserializeObject<PagedResult<ObservableCollection<InventoryLookUpModel>>>(response);
                var content = await response.Content.ReadAsStringAsync();
                var Items = await Task.Run(() => JsonConvert.DeserializeObject<RegisterLocationResultModel>(content));
                return Items;
            }
            catch (Exception e)
            {
                //await Application.Current.MainPage.DisplayAlert("errorss", e.Message, "ok");
                Console.WriteLine(e.Message);
                return null;
            }




        }

        public async Task<ObservableCollection<CompanyDto>> GetList(Guid id)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiNjczZmUxODItMTE3ZS00MjE5LTg0NGUtYzUxYWUwOGU2YmI4IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjQ2OTE3NTM2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.GvXya2vcWViVRbquoNMhaTjobJu48zA9JyW1dlWIWu0";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            url += "Company/List?id=" + id;

            try
            {
                var response = await client.GetStringAsync(url);
                var Items = await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<CompanyDto>>(response));
                return Items;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<NobleRoleDetailsLookUpModel> GetRoles(Guid companyId)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "company/NobleRolesDetail?companyId=" + companyId;

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiNjczZmUxODItMTE3ZS00MjE5LTg0NGUtYzUxYWUwOGU2YmI4IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjQ2OTE3NTM2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.GvXya2vcWViVRbquoNMhaTjobJu48zA9JyW1dlWIWu0";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            try
            {
                var response = await client.GetStringAsync(url);
                var list = JsonConvert.DeserializeObject<NobleRoleDetailsLookUpModel>(response);
                return list;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return null;
            }
        }

        public async Task<bool> AddPermissions(ObservableCollection<NobleRolesPermissionsLookUpModel> rolePermissionsVm)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "company/SaveNobleRolePermissions";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiNjczZmUxODItMTE3ZS00MjE5LTg0NGUtYzUxYWUwOGU2YmI4IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjQ2OTE3NTM2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.GvXya2vcWViVRbquoNMhaTjobJu48zA9JyW1dlWIWu0";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(rolePermissionsVm));
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
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return false;
            }
        }

        public async Task<bool> AddMultiUnit(CompanyDto company)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Company/SaveMultiUnit";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiNjczZmUxODItMTE3ZS00MjE5LTg0NGUtYzUxYWUwOGU2YmI4IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjQ2OTE3NTM2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.GvXya2vcWViVRbquoNMhaTjobJu48zA9JyW1dlWIWu0";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(company));
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
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return false;
            }
        }

        public async Task<bool> AddLicense(CompanyLicenceVm companyLicenceVm)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            url += "Company/AddLicence";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiNjczZmUxODItMTE3ZS00MjE5LTg0NGUtYzUxYWUwOGU2YmI4IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjQ2OTE3NTM2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.GvXya2vcWViVRbquoNMhaTjobJu48zA9JyW1dlWIWu0";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(companyLicenceVm));
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
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return false;
            }
        }
    }
}
