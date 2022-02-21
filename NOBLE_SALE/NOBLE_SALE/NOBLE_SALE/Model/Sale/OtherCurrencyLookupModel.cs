using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Sale
{
    public class OtherCurrencyLookupModel
    {
        public Guid? CurrencyId { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
