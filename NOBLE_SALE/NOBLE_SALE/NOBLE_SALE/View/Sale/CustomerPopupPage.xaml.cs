using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NOBLE_SALE.View.Sale
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public CustomerPopupPage()
        {
            InitializeComponent();
        }
    }
}