using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Services;
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

        private List<ProductLookUpModel> _ProductList;

        public List<ProductLookUpModel> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged(); }
        }


        private List<Object> _ProductList2;

        public List<Object> ProductList2
        {
            get { return _ProductList2; }
            set { _ProductList2 = value; }
        }

        public ProductLookUpModel Product { get; set; }

        public Command SelectionCommand { get; set; }
        public Command NextBtnCommand { get; set; }

        public SaleInvoice1Vm()
        {
            GetProducts();
            ProductList = new List<ProductLookUpModel>();
            ProductList2 = new List<object>();
            Products = new PagedResult<ProductListModel>();
            Categories = new PagedResult<CategoryListModel>();
            SelectedCategory = new CategoryLookUpModel();
            SelectCategoryCommand = new Command(SelectedCategoryHandler);
            NextBtnCommand = new Command(NextBtnHandler);
        }


        

        private async void NextBtnHandler(object obj)
        {
            ProductList = new List<ProductLookUpModel>();
            ProductList = ProductList2.Cast<ProductLookUpModel>().ToList();
            await Application.Current.MainPage.Navigation.PushAsync(new SaleInvoice2(ProductList));
        }

        private async void SelectedCategoryHandler(object obj)
        {
            Products = new PagedResult<ProductListModel>();
            var service = new ProductService();
            Products = await service.GetProducts(SelectedCategory.Id, null, UserData.Current.WarehouseId, 1);
        }

        private async void GetProducts()
        {
            var service = new ProductService();
            Products = await service.GetProducts(null,null, UserData.Current.WarehouseId, 1);
            Categories = await service.GetCategories(true,1,null);
        }
    }
}
