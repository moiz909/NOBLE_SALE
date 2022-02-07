using NOBLE_SALE.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NOBLE_SALE.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            GetFlowDirection();
        }

        private void GetFlowDirection()
        {
            if (UserData.SelectedLanguage == "English")
            {
                ContentPage.FlowDirection = FlowDirection.RightToLeft;
            }
            else if (UserData.SelectedLanguage == "العربية")
            {
                ContentPage.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ShowPass(object sender, EventArgs args)
        {
            userPassword.IsPassword = userPassword.IsPassword ? false : true;

            if (userPassword.IsPassword == true)
                eye.Source = "ic_eye.png";
            else
                eye.Source = "ic_eye_hide.png";
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (UserData.SelectedLanguage == "English")
            {
                ContentPage.FlowDirection = FlowDirection.RightToLeft;
            }
            else if (UserData.SelectedLanguage == "العربية")
            {
                ContentPage.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}