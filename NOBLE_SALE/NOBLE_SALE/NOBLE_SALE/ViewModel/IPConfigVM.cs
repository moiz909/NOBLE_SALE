using NOBLE_SALE.Helper;
using NOBLE_SALE.View;
using NOBLE_SALE.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel
{
    class IPConfigVM : BaseViewModel
    {
        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private bool _IPError;
        public bool IPError
        {
            get
            {
                return _IPError;
            }
            set
            {
                _IPError = value;
                OnPropertyChanged();
            }
        }

        private bool _PortError;
        public bool PortError
        {
            get
            {
                return _PortError;
            }
            set
            {
                _PortError = value;
                OnPropertyChanged();
            }
        }

        private bool _IPError2;
        public bool IPError2
        {
            get
            {
                return _IPError2;
            }
            set
            {
                _IPError2 = value;
                OnPropertyChanged();
            }
        }

        private bool _PortError2;
        public bool PortError2
        {
            get
            {
                return _PortError2;
            }
            set
            {
                _PortError2 = value;
                OnPropertyChanged();
            }
        }

        private string _IP;
        public string IP
        {
            get
            {
                return _IP;
            }
            set
            {
                _IP = value;
                OnPropertyChanged();
            }
        }

        private string _Port;
        public string Port
        {
            set
            {
                _Port = value;
                OnPropertyChanged();
            }
            get
            {
                return _Port;
            }
        }

        private string _IP2;
        public string IP2
        {
            get
            {
                return _IP2;
            }
            set
            {
                _IP2 = value;
                OnPropertyChanged();
            }
        }

        private string _Port2;
        public string Port2
        {
            set
            {
                _Port2 = value;
                OnPropertyChanged();
            }
            get
            {
                return _Port2;
            }
        }

        private string _SelectedLanguage;
        public string SelectedLanguage
        {
            get
            {
                return _SelectedLanguage;
            }
            set
            {
                _SelectedLanguage = value;
                OnPropertyChanged();
            }
        }
        public ICommand LanguageHandler { get; set; }
        public ICommand ContinueHandler { get; set; }
        
        public IPConfigVM()
        {
            var ip = Preferences.Get("IP", "");
            var port = Preferences.Get("PORT", "");

            if(ip!=null && port!=null)
            {
                IP = ip;
                Port = port;
            }


            UserData.SelectedLanguage = "العربية";
            SelectedLanguage = UserData.SelectedLanguage;
            LocalizationResourceManager.Current.CurrentCulture = CultureInfo.GetCultureInfo("en");
            LanguageHandler = new Command(SelectLanguage);
            ContinueHandler = new Command(ContinueCommand);
        }


        private void SelectLanguage()
        {
            if (UserData.SelectedLanguage == "العربية")
            {
                UserData.SelectedLanguage = "English";
                LocalizationResourceManager.Current.CurrentCulture = CultureInfo.GetCultureInfo("ar");
            }
            else
            {
                UserData.SelectedLanguage = "العربية";
                LocalizationResourceManager.Current.CurrentCulture = CultureInfo.GetCultureInfo("en");
            }

            SelectedLanguage = UserData.SelectedLanguage;
        }

        private async void ContinueCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var flag = true;

            if (!UserData.CheckConnectivity())
            {
                await Application.Current.MainPage.DisplayAlert("", "No internet connection available!", "Ok");
                flag = false;
            }

            else if(!IPError && !PortError )
            {
                if(IP!=null && Port!=null)
                {
                    WebApiSettings.IP = IP.Trim();
                    WebApiSettings.Port = Port.Trim();

                    var url = new WebAPI().URL;
                   
                    var client = new WebAPI().client;
                    client.Timeout = TimeSpan.FromSeconds(5);
                    url += "values";

                    try
                    {
                        var response = await client.PostAsync(url, null);

                        flag = false;
                        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                        Preferences.Set("IP", IP);
                        Preferences.Set("PORT", Port);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message);
                    }
                }
            }

            else if(!IPError2 && !PortError2)
            {
                if (IP2 != null && Port2 != null)
                {
                    if (!IPError2 && !PortError2 && IP2 != "" && Port2 != "")
                    {
                        WebApiSettings.IP = IP2.Trim();
                        WebApiSettings.Port = Port2.Trim();
                        var url2 = new WebAPI().URL;
                        var client2 = new WebAPI().client;
                        client2.Timeout = TimeSpan.FromSeconds(5);
                        url2 += "values";


                        try
                        {
                            var response = await client2.PostAsync(url2, null);
                            flag = false;
                            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                            Preferences.Set("IP", IP2);
                            Preferences.Set("PORT", Port2);
                        }
                        catch (Exception E)
                        {
                            Console.WriteLine(E.Message);
                        }
                    }
                }
            }

            if(flag)
                await Application.Current.MainPage.DisplayAlert("Error", "IP/Port Invalid", "Ok");

            IsBusy = false;
            //await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
