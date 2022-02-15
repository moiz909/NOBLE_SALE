using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Services;
using System;
using System.Collections.Generic;
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


        private List<ProductLookUpModel> _ProductList2;

        public List<ProductLookUpModel> ProductList2
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
            ProductList2 = new List<ProductLookUpModel>();
            Products = new PagedResult<ProductListModel>();
            Categories = new PagedResult<CategoryListModel>();
            SelectedCategory = new CategoryLookUpModel();
            SelectCategoryCommand = new Command(SelectedCategoryHandler);
            NextBtnCommand = new Command(NextBtnHandler);
            SelectionCommand = new Command(SelectionHandler);
        }

        private void SelectionHandler(object obj)
        {
            ProductList.Add(Product);
            Product = new ProductLookUpModel();
        }

        private void NextBtnHandler(object obj)
        {
            ProductList2 = ProductList;
        }

        private async void SelectedCategoryHandler(object obj)
        {
            Products = new PagedResult<ProductListModel>();
            var service = new ProductService();
            Products = await service.GetProducts(SelectedCategory.Id, null, Guid.Parse("ebcd6522-fc96-4be7-ddbe-08d9b7cf900e"), 1);
        }

        private async void GetProducts()
        {
            var service = new ProductService();
            Products = await service.GetProducts(null,null, Guid.Parse("ebcd6522-fc96-4be7-ddbe-08d9b7cf900e"),1);
            Categories = await service.GetCategories(true,1,null);
        }
    }
}
