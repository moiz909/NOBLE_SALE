using NOBLE_SALE.Resources;
using NOBLE_SALE.View;
using NOBLE_SALE.View.Sale;
using System;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NOBLE_SALE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
            MainPage = new NavigationPage(new SaleInvoice3()) { BarBackgroundColor = Color.FromHex("#FFFFFF") };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
