using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Product
{
    public class ProductVm
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? ProductMasterId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public string BarCode { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }


        public Guid? TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public decimal SalePrice { get; set; }
        public Guid? OriginId { get; set; }
        public string StockLevel { get; set; }
        public string SaleReturnDays { get; set; }
        public string Description { get; set; }
        public string Shelf { get; set; }
        public bool IsExpire { get; set; }
        public bool IsActive { get; set; }
        public bool IsRaw { get; set; }
        public bool Serial { get; set; }
        public bool Guarantee { get; set; }
        public string LevelOneUnit { get; set; }
        public string BasicUnit { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SalePriceUnit { get; set; }
        public string Assortment { get; set; }
        public string StyleNumber { get; set; }
    }
}
