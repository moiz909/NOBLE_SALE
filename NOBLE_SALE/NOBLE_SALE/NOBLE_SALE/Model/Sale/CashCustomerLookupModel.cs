using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Sale
{
    public class CashCustomerLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CustomerId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string CashCustomerId { get; set; }
    }
}
