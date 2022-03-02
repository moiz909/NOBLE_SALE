using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Sale
{
    public class SaleDetailLookupModel
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerAddressWalkIn { get; set; }
        public string RegistrationNo { get; set; }
        public string CashCustomer { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerVat { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTelephone { get; set; }
        public decimal DiscountAmount { get; set; }
        public string CreditCustomer { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? AreaId { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid WareHouseId { get; set; }
        public string WareHouseName { get; set; } //For showing detail purpose
        public string WareHouseNameAr { get; set; } //For showing detail purpose
        public bool IsCredit { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public virtual ICollection<SaleItemLookupModel> SaleItems { get; set; }
        public virtual SalePaymentLookupModel SalePayment { get; set; }
        public string CustomerName { get; set; } //For showing detail purpose
        public string Change { get; set; }
        public string PaymentAmount { get; set; }
        public string RefrenceNo { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public string Email { get; set; }
        public string CashCustomerId { get; set; }
        public CashVoucherLookUpModel CashVoucher { get; set; }
        public string BarCode { get; set; }
        public Guid? QuotationId { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public string LogisticNameEn { get; set; }
        public string LogisticNameAr { get; set; }
        public decimal ReturnInvoiceAmount { get; set; }
        public string ReturnInvoiceRegNo { get; set; }

        public Guid? LogisticId { get; set; }
        public List<PaymentTypeLookupModel> PaymentTypes { get; set; }
        //public List<PaymentVoucher> PaymentVoucher { get; set; }
        public string PartiallyInvoice { get; set; }
        public string SaleOrderNo { get; set; }
    }
}
