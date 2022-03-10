using NOBLE_SALE.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model
{
    public class ContactLookUpModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string DisplayName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string CommercialRegistrationNo { get; set; }
        public string VatNo { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PaymentTerms { get; set; }
        public string Remarks { get; set; }
        public string RegistrationDate { get; set; }
        public SupplierType SupplierType { get; set; }
        public string Expiry { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string DeliveryTerm { get; set; }
        public string CreditDays { get; set; }
        public string CreditLimit { get; set; }
        public string CreditPeriod { get; set; }
        public bool IsExpire { get; set; }
        public bool IsActive { get; set; }
        public bool IsCustomer { get; set; }
        public bool Status { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime CaptureDate { get; set; }
        public string Reason { get; set; }
        public decimal OpeningBalance { get; set; }
        public Guid? AdvanceAccountId { get; set; }
    }
}
