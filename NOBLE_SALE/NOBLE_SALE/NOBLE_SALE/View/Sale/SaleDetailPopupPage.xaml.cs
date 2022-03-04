using NOBLE_SALE.ViewModel.Sale;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NOBLE_SALE.View.Sale
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaleDetailPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SaleDetailPopupPage(decimal due, decimal recieved, decimal balance, string html)
        {
            InitializeComponent();
            BindingContext = new SaleDetailPopupVm(due,recieved,balance,html);
        }
    }
}