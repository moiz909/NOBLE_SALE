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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            GetFlowDirection();
        }

        private void GetFlowDirection()
        {
            if (UserData.SelectedLanguage == "English")
            {
                NobleLogin.FlowDirection = FlowDirection.RightToLeft;
            }
            else if (UserData.SelectedLanguage == "العربية")
            {
                NobleLogin.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ShowPass(object sender, EventArgs args)
        {
            userPassword.IsPassword = userPassword.IsPassword ? false : true;

            if (userPassword.IsPassword == true)
                eye.ResourceId = "NOBLE_SALE.Resources.Icons.visibilityOff.svg";
            else
                eye.ResourceId = "NOBLE_SALE.Resources.Icons.visibility.svg";

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (UserData.SelectedLanguage == "English")
            {
                NobleLogin.FlowDirection = FlowDirection.RightToLeft;
            }
            else if (UserData.SelectedLanguage == "العربية")
            {
                NobleLogin.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}