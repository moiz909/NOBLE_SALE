using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Model.SetupSteps
{
    public class StepsVm
    {
        public bool Step1 { get; set; }
        public Guid CompanyId { get; set; }
        public bool Step2 { get; set; }
        public bool Step3 { get; set; }
        public bool Step4 { get; set; }
        public bool Step5 { get; set; }
    }
}
