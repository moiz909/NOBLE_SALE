using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Company
{
    class NobleRolesPermissionsLookUpModel
    {
        public Guid RoleId { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Guid NobleModuleId { get; set; }
        public bool isNobel { get; set; }
        public bool IsEdit { get; set; }
        public string AllowAll { get; set; }
        public Guid? PermissionId { get; set; }
    }
}
