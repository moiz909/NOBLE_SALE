using NOBLE_SALE.Model.Product;
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
    public partial class SaleInvoice2 : ContentPage
    {
        public SaleInvoice2(List<ProductLookUpModel> model)
        {
            InitializeComponent();
            BindingContext = new SaleInvoice2Vm(model);
        }

        private void SwipeItemView_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            SaleInvoice2Vm model = item.BindingContext as SaleInvoice2Vm;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var gesture = sender as TapGestureRecognizer;
            var product = gesture.BindingContext as SaleItemLookupModel;
            var vm = BindingContext as SaleInvoice2Vm;
            vm.RemoveCommand.Execute(product);
        }

        private void SwipeItemView_Invoked_1(object sender, EventArgs e)
        {

        }
    }
}