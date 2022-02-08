using NOBLE_SALE.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Company
{
    public class CompanyLicenceDto
    {
        public Guid? Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfTransactions { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyType CompanyType { get; set; }
    }
}
