using NOBLE_SALE.Model.Product;
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
    public partial class SaleInvoice2 : ContentPage
    {
        public SaleInvoice2(List<ProductLookUpModel> model)
        {
            InitializeComponent();
            BindingContext = new SaleInvoice2Vm(model);
        }
    }
}