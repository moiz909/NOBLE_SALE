using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Model.Sale;
using NOBLE_SALE.Services;
using NOBLE_SALE.View.Sale;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Command DeleteItemHandler { get; set; }
        public Command RemovePage { get; set; }
        public Command WalkinHandler { get; set; }

        private int _TotalItems;
        public int TotalItems
        {
            get { return _TotalItems; }
            set 
            { 
                _TotalItems = value;
                OnPropertyChanged();
            }
        }

        private int _TotalQuantity;
        public int TotalQuantity
        {
            get { return _TotalQuantity; }
            set 
            { 
                _TotalQuantity = value;
                OnPropertyChanged();
            }
        }

        private SaleLookupModel _Sale;
        public SaleLookupModel Sale
        {
            get { return _Sale; }
            set 
            { 
                _Sale = value;
                OnPropertyChanged();
            }
        }




        private ObservableCollection<SaleItemLookupModel> _Products;

        public ObservableCollection<SaleItemLookupModel> Products
        {
            get { return _Products; }
            set 
            { 
                _Products = value;
                OnPropertyChanged();
            }
        }
        public List<ProductLookUpModel> ProductList { get; set; }

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

        private string _TodayDate;

        public string TodayDate
        {
            get { return _TodayDate; }
            set 
            {
                _TodayDate = value;
                OnPropertyChanged();
            }
        }

        private string _TimeNow;

        public string TimeNow
        {
            get { return _TimeNow; }
            set 
            { 
                _TimeNow = value;
                OnPropertyChanged();
            }
        }





        public SaleInvoice2Vm(List<ProductLookUpModel> model)
        {
            ProductList = model;
            Products = new ObservableCollection<SaleItemLookupModel>();
            Sale = new SaleLookupModel();
            GetData(model);
            TotalItems = model.Count;
            TotalQuantity = model.Count;
            DecrementQtyHandler = new Command(DecrementQtyCommand);
            IncrementQtyHandler = new Command(IncrementQtyCommand);
            DeleteItemHandler = new Command(DeleteItemCommand);
            WalkinHandler = new Command(WalkinCommand);
            TodayDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            TimeNow = DateTime.Now.ToString("hh:mm tt");
            RemovePage = new Command(RemovePageCommand);
        }

        public SaleInvoice2Vm()
        {
            Products = new ObservableCollection<SaleItemLookupModel>();
            Sale = new SaleLookupModel();
            DecrementQtyHandler = new Command(DecrementQtyCommand);
            IncrementQtyHandler = new Command(IncrementQtyCommand);
            DeleteItemHandler = new Command(DeleteItemCommand);
            WalkinHandler = new Command(WalkinCommand);
            TodayDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            TimeNow = DateTime.Now.ToString("hh:mm tt");
            RemovePage = new Command(RemovePageCommand);
        }

        private async void WalkinCommand(object obj)
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync(new CustomerPopupPage());
        }

        private async void RemovePageCommand(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public Command<SaleItemLookupModel> RemoveCommand
        {
            get
            {
                return new Command<SaleItemLookupModel>((Product) => {
                    var lastproducts = new List<SaleItemLookupModel>();
                    TotalVat = 0;
                    Total = 0;
                    Products.Remove(Product);
                    foreach (var item in Products)
                    {
                        var saleitem = new SaleItemLookupModel
                        {
                            Code = item.Code,
                            Id = Guid.Empty,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Quantity = item.Quantity,
                            SaleId = Guid.Empty,
                            SaleReturnDays = "0",
                            Total = item.Total,
                            TaxMethod = item.TaxMethod,
                            TaxRateId = item.TaxRateId,
                            UnitPrice = item.UnitPrice,
                            WareHouseId = Guid.Empty
                        };
                        lastproducts.Add(saleitem);
                    }
                    Products.Clear();

                    foreach (var item in lastproducts)
                    {
                        var sale = new SaleItemLookupModel
                        {
                            Code = item.Code,
                            Id = Guid.Empty,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Quantity = item.Quantity,
                            SaleId = Guid.Empty,
                            Total = item.Total,
                            SaleReturnDays = "0",
                            TaxMethod = item.TaxMethod,
                            TaxRateId = item.TaxRateId,
                            UnitPrice = item.UnitPrice,
                            WareHouseId = Guid.Empty
                        };
                        Products.Add(sale);
                    }

                    foreach (var item in Products)
                    {
                        if (item.TaxMethod == "Inclusive")
                        {
                            TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / (100 + TaxRateList.TaxRates[0].Rate));
                            Total = Total + (item.UnitPrice * item.Quantity);
                        }
                        else
                        {
                            TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / 100);
                        }
                    }
                });
            }
        }

        private void DeleteItemCommand(object obj)
        {
            var lastproducts = new List<SaleItemLookupModel>();
            TotalVat = 0;
            Total = 0;
            foreach (var item in Products)
            {
                var saleitem = new SaleItemLookupModel
                {
                    Code = item.Code,
                    Id = Guid.Empty,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SaleId = Guid.Empty,
                    SaleReturnDays = "0",
                    Total = item.Total,
                    TaxMethod = item.TaxMethod,
                    TaxRateId = item.TaxRateId,
                    UnitPrice = item.UnitPrice,
                    WareHouseId = Guid.Empty
                };
                lastproducts.Add(saleitem);
            }
            Products.Clear();
            var content = obj as SaleItemLookupModel;
            foreach (var item in lastproducts)
            {
                var sale = new SaleItemLookupModel
                {
                    Code = item.Code,
                    Id = Guid.Empty,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SaleId = Guid.Empty,
                    Total = item.Total,
                    SaleReturnDays = "0",
                    TaxMethod = item.TaxMethod,
                    TaxRateId = item.TaxRateId,
                    UnitPrice = item.UnitPrice,
                    WareHouseId = Guid.Empty
                };
                Products.Add(sale);
            }

            foreach (var item in Products)
            {
                if (item.ProductId == content.ProductId)
                {
                    Products.Remove(item);
                }
                if (item.TaxMethod == "Inclusive")
                {
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / (100 + TaxRateList.TaxRates[0].Rate));
                    Total = Total + (item.UnitPrice * item.Quantity);
                }
                else
                {
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / 100);
                }
            }
        }

        private void IncrementQtyCommand(object obj)
        {
            var lastproducts = new List<SaleItemLookupModel>();
            TotalVat = 0;
            Total = 0;
            foreach (var item in Products)
            {
                var saleitem = new SaleItemLookupModel
                {
                    Code = item.Code,
                    Id = Guid.Empty,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SaleId = Guid.Empty,
                    SaleReturnDays = "0",
                    Total = item.Total,
                    TaxMethod = item.TaxMethod,
                    TaxRateId = item.TaxRateId,
                    UnitPrice = item.UnitPrice,
                    WareHouseId = Guid.Empty
                };
                lastproducts.Add(saleitem);
            }
            Products.Clear();
            var content = obj as SaleItemLookupModel;
            foreach (var item in lastproducts)
            {
                var sale = new SaleItemLookupModel
                {
                    Code = item.Code,
                    Id = Guid.Empty,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SaleId = Guid.Empty,
                    Total = item.Total,
                    SaleReturnDays = "0",
                    TaxMethod = item.TaxMethod,
                    TaxRateId = item.TaxRateId,
                    UnitPrice = item.UnitPrice,
                    WareHouseId = Guid.Empty
                };
                Products.Add(sale);
            }

            foreach (var item in Products)
            {
                if(item.ProductId == content.ProductId)
                {
                    item.Quantity++;
                    item.Total = item.Quantity * item.UnitPrice;
                    TotalQuantity++;
                }
                if (item.TaxMethod == "Inclusive")
                {
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / (100 + TaxRateList.TaxRates[0].Rate));
                    Total = Total + (item.UnitPrice * item.Quantity);
                }
                else
                {
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / 100);
                }
            }
        }

        private void DecrementQtyCommand(object obj)
        {
            TotalVat = 0;
            Total = 0;
            var lastproducts = new List<SaleItemLookupModel>();
            foreach (var item in Products)
            {
                var saleitem = new SaleItemLookupModel
                {
                    Code = item.Code,
                    Id = Guid.Empty,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SaleId = Guid.Empty,
                    Total = item.Total,
                    SaleReturnDays = "0",
                    TaxMethod = item.TaxMethod,
                    TaxRateId = item.TaxRateId,
                    UnitPrice = item.UnitPrice,
                    WareHouseId = Guid.Empty,
                };
                lastproducts.Add(saleitem);
            }
            Products.Clear();
            var content = obj as SaleItemLookupModel;
            foreach (var item in lastproducts)
            {
                var sale = new SaleItemLookupModel
                {
                    Code = item.Code,
                    Total = item.Total,
                    Id = Guid.Empty,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SaleId = Guid.Empty,
                    SaleReturnDays = "0",
                    TaxMethod = item.TaxMethod,
                    TaxRateId = item.TaxRateId,
                    UnitPrice = item.UnitPrice,
                    WareHouseId = Guid.Empty
                };
                Products.Add(sale);
            }

            foreach (var item in Products)
            {
                if (item.ProductId == content.ProductId)
                {
                    if(item.Quantity <= 0)
                    {
                        item.Total = item.Quantity * item.UnitPrice;
                    }
                    else
                    {
                        item.Total = item.Quantity * item.UnitPrice;
                        TotalQuantity--;
                        item.Quantity--;
                    }
                    
                }

                if (item.TaxMethod == "Inclusive")
                {
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / (100 + TaxRateList.TaxRates[0].Rate));
                    Total = Total + (item.UnitPrice * item.Quantity);
                }
                else
                {
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / 100);
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
                    Quantity = 1,
                    SaleReturnDays = "0",
                    TaxMethod = item.TaxMethod,
                    TaxRateId = (Guid)item.TaxRateId,
                    UnitPrice = item.SalePrice,
                    Total = item.SalePrice,
                    WareHouseId = Guid.Empty
                };
                Products.Add(sale);
            }
            TaxRateList = new TaxRateListModel();
            var service = new ProductService();
            RegistrationNoDetail = await service.GetRegistrationNoDetail();
            TaxRateList = await service.GetTax();
            foreach (var item in Products)
            {
                if(item.TaxMethod == "Inclusive")
                {
                    Total = Total + item.UnitPrice;
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / (100 + TaxRateList.TaxRates[0].Rate));
                }
                else
                {
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * TaxRateList.TaxRates[0].Rate) / 100 );
                }
            }

        }
    }
}
