using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Sale
{
    public class ProductDropdownLookUpModel
    {
        public Guid Id { get; set; }
        public string BarCode { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public bool IsExpire { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public string SaleReturnDays { get; set; }
        public bool Guarantee { get; set; }
        public bool Serial { get; set; }
        public string StyleNumber { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string LevelOneUnit { get; set; }
        public string BasicUnit { get; set; }
    }
}
