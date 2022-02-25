using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Services;
using NOBLE_SALE.View.Product;
using NOBLE_SALE.View.Sale;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Sale
{
    class SaleInvoice1Vm : BaseViewModel
    {
        private PagedResult<ProductListModel> _Products;
        public PagedResult<ProductListModel> Products
        {
            get { return _Products; }
            set { _Products = value; OnPropertyChanged(); }
        }

        private PagedResult<CategoryListModel> _Categories;
        public PagedResult<CategoryListModel> Categories
        {
            get { return _Categories; }
            set { _Categories = value; OnPropertyChanged(); }
        }

        public CategoryLookUpModel SelectedCategory { get; set; }
        public Command SelectCategoryCommand { get; set; }

        public Command AddProductHandler { get; set; }

        private List<ProductLookUpModel> _ProductList;
        public List<ProductLookUpModel> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged(); }
        }

        private List<Object> _SelectedProducts;
        public List<Object> SelectedProducts
        {
            get { return _SelectedProducts; }
            set { _SelectedProducts = value; }
        }

        public ProductLookUpModel Product { get; set; }

        public Command SelectionCommand { get; set; }
        public Command NextBtnCommand { get; set; }
        private bool _IsRefreshing;

        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { _IsRefreshing = value; OnPropertyChanged(); }
        }

        public Command RefreshCommand { get; set; }

        private bool isBusy;
        public bool IsBusy 
        { 
            get 
            { 
                return isBusy;
            } 
            set 
            {
                isBusy = value;
                OnPropertyChanged();
            } 
        }

        public SaleInvoice1Vm()
        {
            IsBusy = false;
            GetProducts();
            ProductList = new List<ProductLookUpModel>();
            SelectedProducts = new List<object>();
            Products = new PagedResult<ProductListModel>();
            Categories = new PagedResult<CategoryListModel>();
            SelectedCategory = new CategoryLookUpModel();
            SelectCategoryCommand = new Command(SelectedCategoryHandler);
            NextBtnCommand = new Command(NextBtnHandler);
            RefreshCommand = new Command(RefreshCommandExecution);
            AddProductHandler = new Command(AddProductCommand);
        }

        private void RefreshCommandExecution(object obj)
        {
            IsRefreshing = true;
            GetProducts();
            IsRefreshing = false;
        }

        private async void AddProductCommand(object obj)
        {
            if (UserData.Current.InvoiceWoInventory)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AddProduct());
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("Disable", "You Dont Have Permission Contact Support", "ok");
            }
            
        }

        private async void NextBtnHandler(object obj)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            ProductList = new List<ProductLookUpModel>();
            ProductList = SelectedProducts.Cast<ProductLookUpModel>().ToList();
            await Application.Current.MainPage.Navigation.PushAsync(new SaleInvoice2(ProductList));

            IsBusy = false;
        }

        private async void SelectedCategoryHandler(object obj)
        {

            if (IsBusy)
                return;

            IsBusy = true;

            Products = new PagedResult<ProductListModel>();
            var service = new ProductService();

            if (UserData.Current.InvoiceWoInventory)
            {
                Products = await service.GetProducts(SelectedCategory.Id, null, null, 1);
            }
            else
            {
                Products = await service.GetProducts(SelectedCategory.Id, null, UserData.Current.WarehouseId, 1);
            }

            IsBusy = false;
        }

        private async void GetProducts()
        {

            if (IsBusy)
                return;

            IsBusy = true;

            var service = new ProductService();
            if (UserData.Current.InvoiceWoInventory)
            {
                Products = await service.GetProducts(null, null, null, 1);
            }
            else
            {
                Products = await service.GetProducts(null, null, UserData.Current.WarehouseId, 1);
            }
            
            Categories = await service.GetCategories(true,1,null);

            IsBusy = false;
        }
    }
}
