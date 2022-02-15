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
    public partial class IPConfiguration : ContentPage
    {
        public IPConfiguration()
        {
            InitializeComponent();
            GetFlowDirection();
        }
        private void GetFlowDirection()
        {
            if (UserData.SelectedLanguage == "English")
            {
                IpConfigPage.FlowDirection = FlowDirection.RightToLeft;
            }
            else if (UserData.SelectedLanguage == "العربية")
            {
                IpConfigPage.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}