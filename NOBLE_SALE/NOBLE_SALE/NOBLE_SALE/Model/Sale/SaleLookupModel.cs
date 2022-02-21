using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Sale
{
    public class SaleLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string CustomerAddressWalkIn { get; set; }
        public string VoucherNo { get; set; }
        public string CashCustomer { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public string RefrenceNo { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? AreaId { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid WareHouseId { get; set; }
        public bool IsCredit { get; set; }
        public decimal Change { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public virtual ICollection<SaleItemLookupModel> SaleItems { get; set; }
        public virtual SalePaymentLookupModel SalePayment { get; set; }
        public virtual OtherCurrencyLookupModel OtherCurrency { get; set; }
        public string Email { get; set; }
        public string CashCustomerId { get; set; }
        public bool IsCashVoucher { get; set; }
        public bool DayStart { get; set; }
        public string CounterId { get; set; }
        public string BarCode { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? QuotationId { get; set; }
        public Guid? LogisticId { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public string SoInventoryReserve { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public decimal ReturnInvoiceAmount { get; set; }
        public string AutoPaymentVoucher { get; set; }
        public string UserName { get; set; }
    }

    
}
