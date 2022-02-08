using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Company
{
    public class CompanyDto
    {

        public Guid Id { get; set; }

        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public string VatRegistrationNo { get; set; }
        public string CompanyEmail { get; set; }
        public string CityEnglish { get; set; }
        public string CityArabic { get; set; }

        public string CountryEnglish { get; set; }
        public string CountryArabic { get; set; }
        public string CategoryEnglish { get; set; }
        public string CategoryArabic { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressArabic { get; set; }
        public string DfeNumber { get; set; }
        public string CreatedDate { get; set; }
        public string LogoPath { get; set; }
        public string CompanyRegNo { get; set; }
        public string HouseNumber { get; set; }
        public string Postcode { get; set; }
        public string Town { get; set; }
        public string LandLine { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ClientParentId { get; set; }
        public Guid? BusinessId { get; set; }
        public string ClientNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsBlock { get; set; }
        public bool? IsActive { get; set; }
        public int NumberOfUsers { get; set; }
        public string CompanyType { get; set; }
        public decimal NoOfTransactionsAllow { get; set; }
        public Guid CompanyLicenceId { get; set; }
        public List<CompanyLicenceDto> CompanyLicences { get; set; }
        public string Currency { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool IsProduction { get; set; }
        public bool IsMulti { get; set; }
        public bool InvoiceWoInventory { get; set; }
        public bool IsArea { get; set; }
        public bool IsProceed { get; set; }
        public bool English { get; set; }
        public bool Arabic { get; set; }
        public bool Terminal { get; set; }
        public bool DayStart { get; set; }
        public bool IsDayStart { get; set; }
        public bool CashVoucher { get; set; }
        public bool IsCashVoucher { get; set; }
        public bool IsOpenDay { get; set; }
        public bool OpenDay { get; set; }
        public bool IsDailyExpense { get; set; }
        public bool DailyExpense { get; set; }
        public bool IsTransferAllow { get; set; }
        public bool TransferAllow { get; set; }
        public bool MasterProduct { get; set; }
        public bool IsMasterProduct { get; set; }
        public bool Step1 { get; set; }
        public bool Step2 { get; set; }
        public bool Step3 { get; set; }
        public bool Step4 { get; set; }
        public bool Step5 { get; set; }
        public bool SimpleInvoice { get; set; }
        public bool SoInventoryReserve { get; set; }
        public bool IsSaleOrder { get; set; }
        public string SaleOrder { get; set; }
        public bool PurchaseOrder { get; set; }
        public bool InternationalPurchase { get; set; }
        public bool PartiallyPurchase { get; set; }
        public bool VersionAllow { get; set; }
        public bool ExpenseToGst { get; set; }
        public bool IsInternationalPurchase { get; set; }
        public bool AutoPurchaseVoucher { get; set; }
        public bool IsForMedical { get; set; }
        public bool SuperAdminDayStart { get; set; }
        public bool BankDetail { get; set; }
    }
}
