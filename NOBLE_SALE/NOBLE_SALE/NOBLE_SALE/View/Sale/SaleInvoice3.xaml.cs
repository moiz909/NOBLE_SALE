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
        public SaleInvoice3(SaleLookupModel sale )
        {
            InitializeComponent();
            BindingContext = new SaleInvoice3Vm(sale);
        }


        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            SetStyle(1);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            SetStyle(2);
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            SetStyle(3);
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            SetStyle(4);
        }

        void SetStyle(int num)
        {
            List<Frame> frames = new List<Frame>() { frame_1, frame_2, frame_3, frame_4 };

            for (int i = 1; i < 5; i++)
            {
                Frame frame = frames[i - 1];

                if (i == num)
                {
                    frame.BorderColor = Color.FromHex("#0280FD");
                }
                else
                {
                    frame.BorderColor = Color.FromHex("#EDF3FA");
                }

            }

        }
    }
}