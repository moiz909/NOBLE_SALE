using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using NOBLE_SALE.Services;
using NOBLE_SALE.View;
using NOBLE_SALE.View.Sale;

namespace NOBLE_SALE.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
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

        private InputModel _inputModel;
        public InputModel InputModel
        {
            get
            {
                return _inputModel;
            }
            set
            {
                _inputModel = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginButtonCommand { get; set; }
        public ICommand LanguageHandler { get; set; }
        public ICommand SignupButtonHandler { get; set; }

        private bool _isBusy;
        public bool isBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidEmail;
        public bool IsValidEmail
        {
            get
            {
                return _IsValidEmail;
            }
            set
            {
                _IsValidEmail = value;
                OnPropertyChanged();
            }
        }

        private bool _IsValidPassword;
        public bool IsValidPassword
        {
            get
            {
                return _IsValidPassword;
            }
            set
            {
                _IsValidPassword = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            InputModel = new InputModel();
            InputModel.RememberMe = true;
            LoginButtonCommand = new Command(async () => await LoginCommandAsync());
            LanguageHandler = new Command(SelectLanguage);
            SignupButtonHandler = new Command(async () => await SignUpCommand());

            SelectedLanguage = UserData.SelectedLanguage;

            var email = Preferences.Get("Email", "");
            var pass = Preferences.Get("Password", "");

            if (email != null && pass != null)
            {
                InputModel.Email = email;
                InputModel.Password = pass;
            }
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

        private async Task SignUpCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterLocation());
        }

        private async Task LoginCommandAsync()
        {
            if (isBusy)
            {
                return;
            }


            if (!UserData.CheckConnectivity())
            {
                await Application.Current.MainPage.DisplayAlert("", "No internet connection available!", "Ok");
            }

            else if (InputModel.Email == null || InputModel.Email == "")
            {
                await Application.Current.MainPage.DisplayAlert("", "Please check Email/Password", "Ok");
            }

            else if (InputModel.Password == null || InputModel.Password == "")
            {
                IsValidPassword = false;
                await Application.Current.MainPage.DisplayAlert("", "Please check Email/Password", "Ok");
            }

            else
            {
                isBusy = true;
                var service = new LoginService();
                var DataSet = await service.LoginUser(InputModel);

                if (DataSet == null && UserData.TwoFactor)
                {
                    UserData._InputModel = InputModel;
                   // await Application.Current.MainPage.Navigation.PushAsync(new TwoFactorScreen());
                }
                else if (DataSet == null)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Network Error. Check IP/Port", "Ok");
                }
                else if (DataSet != null && DataSet.Token == null)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Please check Email/Password", "Ok");
                }
                //else if(DataSet.TermsCondition == false )
                //{
                //    UserData.CurrencyandVat = true;
                //    UserData.CompanySetup = true;

                //    await Application.Current.MainPage.Navigation.PushAsync(new TermsAndConditionView());
                //}
                else if( DataSet.Step1 == false || DataSet.Step2 == false
                    || DataSet.Step3 == false || DataSet.Step4 == false )
                {
                    if(!DataSet.Step2)
                    {
                        UserData.CompanySetup = true;
                    }
                    if(!DataSet.Step3)
                    {
                        UserData.CurrencyandVat = true;
                    }
                    else
                    {
                        UserData.CompanySetup = false;
                        UserData.CurrencyandVat = false;
                    }
                    await Application.Current.MainPage.Navigation.PushAsync(new SetupPage());
                }
                else if (DataSet != null && DataSet.RoleName != null)
                {
                    UserData.Current = DataSet;
                    UserData.Permissions = await service.GetCompanyPermissions();


                    Preferences.Set("Email", InputModel.Email);
                    Preferences.Set("Password", InputModel.Password);


                   

                    if (DataSet.RoleName == "User")
                    {
                        if (DataSet.Step1 == false || DataSet.Step2 == false || DataSet.Step3 == false || DataSet.Step4 == false)
                        {
                            await Application.Current.MainPage.DisplayAlert("", "Please Setup your account configuration", "Ok");
                        }
                        else
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new SaleInvoice1());
                        }

                    }

                    else if (DataSet.RoleName == "Super Admin")
                    {
                        //await Application.Current.MainPage.Navigation.PushAsync(new CompanyDashboard());
                    }

                    else if (DataSet.RoleName == "Admin")
                    {
                        //await Application.Current.MainPage.Navigation.PushAsync(new BusinessDashboard());

                    }
                }
            }

            isBusy = false;
        }
    }
}
