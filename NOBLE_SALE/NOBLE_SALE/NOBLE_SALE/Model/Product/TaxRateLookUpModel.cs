using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Product
{
    public class TaxRateLookUpModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
    }
}
