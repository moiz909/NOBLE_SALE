using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Product
{
    public class TaxRateListModel
    {
        public IList<TaxRateLookUpModel> TaxRates { get; set; }
        public string TaxMethod { get; set; }
    }
}
