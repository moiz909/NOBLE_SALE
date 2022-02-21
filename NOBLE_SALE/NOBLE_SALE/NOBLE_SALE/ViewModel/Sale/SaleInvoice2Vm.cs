using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Model.Sale;
using NOBLE_SALE.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Sale
{
    class SaleInvoice2Vm : BaseViewModel
    {
        private TaxRateListModel _TaxRateList;

        public TaxRateListModel TaxRateList
        {
            get { return _TaxRateList; }
            set { _TaxRateList = value; OnPropertyChanged(); }
        }

        private RegistrationNoLookUp _RegistrationNoDetail;

        public RegistrationNoLookUp RegistrationNoDetail
        {
            get { return _RegistrationNoDetail; }
            set { _RegistrationNoDetail = value; OnPropertyChanged(); }
        }

        public Command DecrementQtyHandler { get; set; }
        public Command IncrementQtyHandler { get; set; }

        //private List<ProductLookUpModel> _Products;

        //public List<ProductLookUpModel> Products
        //{
        //    get { return _Products; }
        //    set 
        //    { 
        //        _Products = value;
        //        OnPropertyChanged();
        //    }
        //}

        private List<SaleItemLookupModel> _Products;

        public List<SaleItemLookupModel> Products
        {
            get { return _Products; }
            set 
            { 
                _Products = value;
                OnPropertyChanged();
            }
        }
        public List<ProductLookUpModel> ProductList { get; set; }




        public SaleInvoice2Vm(List<ProductLookUpModel> model)
        {
            ProductList = model;
            Products = new List<SaleItemLookupModel>();
            GetData(model);
            DecrementQtyHandler = new Command(DecrementQtyCommand);
            IncrementQtyHandler = new Command(IncrementQtyCommand);
        }

        private void IncrementQtyCommand(object obj)
        {
            //Products.Clear();
            var content = obj as SaleItemLookupModel;
            //foreach (var item in ProductList)
            //{
            //    var sale = new SaleItemLookupModel
            //    {
            //        Code = item.Code,
            //        Id = Guid.Empty,
            //        ProductId = item.Id,
            //        ProductName = item.EnglishName,
            //        Quantity = 1,
            //        SaleId = Guid.Empty,
            //        SaleReturnDays = "0",
            //        TaxMethod = item.TaxMethod,
            //        TaxRateId = (Guid)item.TaxRateId,
            //        UnitPrice = item.SalePrice,
            //        WareHouseId = Guid.Empty
            //    };
            //    Products.Add(sale);
            //}

            foreach (var item in Products)
            {
                if(item.ProductId == content.ProductId)
                {
                    item.Quantity++;
                }
            }
        }

        private void DecrementQtyCommand(object obj)
        {
            //Products.Clear();
            var content = obj as SaleItemLookupModel;
            //foreach (var item in ProductList)
            //{
            //    var sale = new SaleItemLookupModel
            //    {
            //        Code = item.Code,
            //        Id = Guid.Empty,
            //        ProductId = item.Id,
            //        ProductName = item.EnglishName,
            //        Quantity = 1,
            //        SaleId = Guid.Empty,
            //        SaleReturnDays = "0",
            //        TaxMethod = item.TaxMethod,
            //        TaxRateId = (Guid)item.TaxRateId,
            //        UnitPrice = item.SalePrice,
            //        WareHouseId = Guid.Empty
            //    };
            //    Products.Add(sale);
            //}

            foreach (var item in Products)
            {
                if (item.ProductId == content.ProductId)
                {
                    item.Quantity--;
                }
            }
        }

        private async void GetData(List<ProductLookUpModel> products)
        {
            foreach (var item in products)
            {
                var sale = new SaleItemLookupModel
                {
                    Code = item.Code,
                    Id = Guid.Empty,
                    ProductId = item.Id,
                    ProductName = item.EnglishName,
                    SaleId = Guid.Empty,
                    SaleReturnDays = "0",
                    TaxMethod = item.TaxMethod,
                    TaxRateId = (Guid)item.TaxRateId,
                    UnitPrice = item.SalePrice,
                    WareHouseId = Guid.Empty
                };
                Products.Add(sale);
            }
            TaxRateList = new TaxRateListModel();
            var service = new ProductService();
            RegistrationNoDetail = await service.GetRegistrationNoDetail();
            TaxRateList = await service.GetTax();
        }
    }
}
