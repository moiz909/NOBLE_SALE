using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Sale
{
    public class SalePaymentLookupModel
    {
        public Guid Id { get; set; }
        public Decimal DueAmount { get; set; }
        public Decimal Received { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Change { get; set; }
        public SalePaymentType PaymentType { get; set; }
        public List<PaymentTypeLookupModel> PaymentTypes { get; set; }
        public Guid? PaymentOptionId { get; set; }
        public Guid SaleId { get; set; }

    }

    public class PaymentTypeLookupModel
    {
        public SalePaymentType PaymentType { get; set; }
        public Guid? PaymentOptionId { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; } // received Amount
        public string PaymentOptionName { get; set; } //For showing detail purpose
        public decimal Rate { get; set; }
        public decimal OtherAmount { get; set; }
        public Guid Id { get; set; }
        public decimal AmountCurrency { get; set; }
    }
}
