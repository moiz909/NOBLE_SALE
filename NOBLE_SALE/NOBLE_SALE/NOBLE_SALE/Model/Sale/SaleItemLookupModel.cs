using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Sale
{
    public class SaleItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Total { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal IncludingVat { get; set; }
        public Guid SaleId { get; set; }
        public Guid WareHouseId { get; set; }
        public decimal OfferQuantity { get; set; }
        public decimal Get { get; set; }
        public decimal Buy { get; set; }
        public string Code { get; set; }
        public string SaleReturnDays { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? BundleId { get; set; }
        public decimal InclusiveVat { get; set; }
        public string TaxMethod { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal BundleAmount { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal ReturnQuantity { get; set; }
        public decimal SoQty { get; set; }
    }
}
