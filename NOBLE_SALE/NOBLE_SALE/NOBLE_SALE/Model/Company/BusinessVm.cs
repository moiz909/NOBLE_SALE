using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Company
{
    class BusinessVm
    {
        public String Id { get; set; }
        public string NameInArabic { get; set; }
        public string NameInEnglish { get; set; }
        public string AddressInArabic { get; set; }
        public string AddressInEnglish { get; set; }
        public string CityInArabic { get; set; }
        public string CityInEnglish { get; set; }
        public string CountryInArabic { get; set; }
        public string CountryInEnglish { get; set; }
        public string CategoryInEnglish { get; set; }
        public string CategoryInArabic { get; set; }
        public string PhoneNumber { get; set; }
        public string LandLine { get; set; }
        public string VatRegistrationNo { get; set; }
        public string CompanyRegNo { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserEmail { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LogoPath { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public Guid ClientParentId { get; set; }
        public Guid BusinessParentId { get; set; }
    }
}
