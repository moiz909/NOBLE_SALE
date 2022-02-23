using NOBLE_SALE.Model.Sale;
using NOBLE_SALE.ViewModel.Sale;
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
    public partial class SaleInvoice3 : ContentPage
    {
        //public SaleInvoice3(SaleLookupModel sale)
        //{
        //    InitializeComponent();
        //    //BindingContext = new SaleInvoice3Vm(sale);
        //}
        public SaleInvoice3()
        {
            InitializeComponent();
        }
    }
}