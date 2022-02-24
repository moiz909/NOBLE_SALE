using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Product
{
    public class PaymentOptionsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
    }
}
