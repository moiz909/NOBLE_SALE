using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.Company
{
    class NobleRoleDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public bool isActive { get; set; }
    }
}
