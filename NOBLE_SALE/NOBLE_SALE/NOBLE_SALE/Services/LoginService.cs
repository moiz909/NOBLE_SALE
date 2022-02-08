using Newtonsoft.Json;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NOBLE_SALE.Services
{
    class LoginService
    {
        public string url;
        public HttpClient client;

        public async Task<LoginModel> AuthenticatorCode(string email, string password, string code, string recoveryCode)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;

            url += "account/AuthenticatorCode?email=" + email +
                "&password=" + password + "&code=" + code + "&recoveryCode=" + recoveryCode;

            try
            {
                var response = await client.GetStringAsync(url);

                if(response==null)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Error", "Please check your code", "Ok");
                    return null;
                }
                else if(response.ToString()=="false")
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Unsuccessful", "Unable To Load User. Please try again.", "Ok");
                    return null;
                }
                else
                {
                    var Items = await Task.Run(() => JsonConvert.DeserializeObject<LoginModel>(response));
                    return Items;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<LoginModel> LoginUser(InputModel inputModel)
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            string serializedModel = await Task.Run(() => JsonConvert.SerializeObject(inputModel));
            var contents = new StringContent(serializedModel);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            url += "account/login";

            try
            {
                var response = await client.PostAsync(url, contents);

                LoginModel Items = new LoginModel();

                if (response.IsSuccessStatusCode && response.ReasonPhrase == "OK")
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if(content.ToString()=="true")
                    {
                        UserData.TwoFactor = true;
                        return null;
                    }   
                    
                    Items = await Task.Run(() => JsonConvert.DeserializeObject<LoginModel>(content));
                    UserData.Current = Items;
                    return Items;
                }

                else
                    return null;
            }
            
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return null;
            }
        }
        public async Task<bool> LogOutUser()
        {
            url = new WebAPI().URL;
            url += "company/logout";
            client = new WebAPI().client;

            try
            {
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<ModuleRightLookUpModel>> GetCompanyPermissions()
        {
            url = new WebAPI().URL;
            client = new WebAPI().client;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + UserData.Current.Token);
            url += "Company/GetCompanyPermission";

            try
            {
                var response = await client.GetStringAsync(url);

                if(response!=null)
                {
                    var Items = await Task.Run(() => JsonConvert.DeserializeObject<List<ModuleRightLookUpModel>>(response.ToString()));
                    return Items;
                }

                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
    }

}
