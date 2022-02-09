using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model
{
    public class ModuleRightLookUpModel
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionCategory { get; set; }
        public bool IsActive { get; set; }
    }
}
