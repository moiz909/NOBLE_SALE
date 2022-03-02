using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Product
{
    public class Message
    {
        public string id { get; set; }
        public string isAddUpdate { get; set; }
        public object isDeleted { get; set; }
        public string saleId { get; set; }
        public bool isDoublePrint { get; set; }
    }

    public class Root
    {
        public Message message { get; set; }
        public string action { get; set; }
    }
}
