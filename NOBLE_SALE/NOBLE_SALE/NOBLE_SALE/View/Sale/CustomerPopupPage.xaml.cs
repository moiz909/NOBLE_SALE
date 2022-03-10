
using NOBLE_SALE.ViewModel.Sale;
using System;
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

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var model = this.BindingContext as CustomerPopupVm;

            if (model.FieldChange.CanExecute(null))
            {
                model.FieldChange.Execute(null);
            }
        }
    }
}