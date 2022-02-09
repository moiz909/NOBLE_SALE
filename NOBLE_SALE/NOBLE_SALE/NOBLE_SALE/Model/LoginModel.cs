using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model
{
    public class LoginModel
    {
        public string Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? CounterId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNo { get; set; }
        public object Token { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool IsProduction { get; set; }
        public bool InvoiceWoInventory { get; set; }
        public string UserRoleName { get; set; }
        public bool? IsTouchInvoice { get; set; }
        public bool IsDayStart { get; set; }
        public string PrintSizeA4 { get; set; }
        public string TermsInAr { get; set; }
        public string TermsInEng { get; set; }
        public bool IsArea { get; set; }
        public bool IsProceed { get; set; }
        public bool Arabic { get; set; }
        public bool English { get; set; }
        public bool? AllowAll { get; set; }
        public bool Terminal { get; set; }
        public bool DayStart { get; set; }
        public bool Step1 { get; set; }
        public bool Step2 { get; set; }
        public bool Step3 { get; set; }
        public bool Step4 { get; set; }
        public bool Step5 { get; set; }
        public bool? IsExpenseDay { get; set; }
        public bool TermsCondition { get; set; }
        public Guid? WarehouseId { get; set; }
        public string LoginUserName { get; set; }
        public bool CashVoucher { get; set; }
        public decimal? ReturnDays { get; set; }
        public bool IsOpenDay { get; set; }
        public bool IsTransferAllow { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsPermissionToStartDay { get; set; }
        public bool IsPermissionToCloseDay { get; set; }
        public string DayStartTime { get; set; }
        public bool TransferCounter { get; set; }
        public bool IsMasterProductPermission { get; set; }
        public string PrinterName { get; set; }
        public bool IsHeaderFooter { get; set; }
        public string NobleRole { get; set; }
        public string SaleOrder { get; set; }
    }
}
