using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Product
{
    public class PaymentOptionsListModel
    {
        public IList<PaymentOptionsLookUpModel> PaymentOptions { get; set; }
    }
}
