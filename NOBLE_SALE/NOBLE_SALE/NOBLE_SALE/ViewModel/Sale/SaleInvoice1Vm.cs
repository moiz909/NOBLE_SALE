﻿using IronPdf;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Helper.Utility;
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
            set 
            { 
                _SelectedProducts = value;
                OnPropertyChanged();
            }
        }

        public ProductLookUpModel Product { get; set; }

        public Command SelectionCommand { get; set; }
        public Command NextBtnCommand { get; set; }

        public Command SearchHandler { get; set; }

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

        private PDFToHtml PDFToHtml { get; set; }


        private decimal _TotalVat;

        public decimal TotalVat
        {
            get { return _TotalVat; }
            set
            {
                _TotalVat = value;
                OnPropertyChanged();
            }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set
            {
                _Total = value;
                OnPropertyChanged();
            }
        }

        private string _SearchTerm;

        public string SearchTerm
        {
            get { return _SearchTerm; }
            set 
            {
                _SearchTerm = value;
                OnPropertyChanged();
            }
        }

        private IList<ProductLookUpModel> _ProductList1;

        public IList<ProductLookUpModel> ProductList1
        {
            get { return _ProductList1; }
            set 
            {
                _ProductList1 = value;
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
            SearchHandler = new Command(SearchCommand);
        }

        private void SearchCommand(object obj)
        {
            GetProducts();
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

            //var service = new ProductService();
            //var html = await service.GetHTML();
            //PDFToHtml = new PDFToHtml();
            //PDFToHtml.HTMLString = html;

            //PDFToHtml.GeneratePDF();
        }

        

        private async void NextBtnHandler(object obj)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            ProductList = new List<ProductLookUpModel>();
            ProductList = SelectedProducts.Cast<ProductLookUpModel>().ToList();
            SelectedProducts = new List<object>();

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
                Products = await service.GetProducts(SelectedCategory.Id, null, null);
            }
            else
            {
                Products = await service.GetProducts(SelectedCategory.Id, null, UserData.Current.WarehouseId);
            }

            IsBusy = false;
        }

        private async void GetProducts()
        {

            if (IsBusy)
                return;

            IsBusy = true;

            if(SearchTerm == string.Empty)
            {
                SearchTerm = null;
            }

            var service = new ProductService();
            if (UserData.Current.InvoiceWoInventory)
            {
                Products = await service.GetProducts(null, SearchTerm, null);

                

                if(Products == null)
                {
                    Products = new PagedResult<ProductListModel>();

                    
                }

                else
                {
                    foreach (var item in Products.Results.Products)
                    {
                        if (item.EnglishName == null || item.EnglishName == string.Empty)
                        {
                            item.DisplayName = item.ArabicName;
                        }
                        else if (item.ArabicName == null || item.ArabicName == string.Empty)
                        {
                            item.DisplayName = item.EnglishName;
                        }
                        else if(item.ArabicName!=null || item.ArabicName!=string.Empty || item.EnglishName!=null || item.EnglishName != string.Empty)
                        {
                            item.DisplayName = item.EnglishName;
                        }
                    }

                    ProductList1 = Products.Results.Products;
                }

                
            }
            else
            {
                Products = await service.GetProducts(null, SearchTerm, UserData.Current.WarehouseId);

                
                if (Products == null)
                {
                    Products = new PagedResult<ProductListModel>();

                    
                }

                else
                {
                    foreach (var item in Products.Results.Products)
                    {
                        if (item.EnglishName == null || item.EnglishName == string.Empty)
                        {
                            item.DisplayName = item.ArabicName;
                        }
                        if (item.ArabicName == null || item.ArabicName == string.Empty)
                        {
                            item.DisplayName = item.EnglishName;
                        }
                    }
                    ProductList1 = Products.Results.Products;
                }
                
            }
            
            Categories = await service.GetCategories(true,1,null);

            IsBusy = false;
        }
    }
}
