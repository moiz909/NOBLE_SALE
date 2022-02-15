using NOBLE_SALE.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace NOBLE_SALE.Helper
{
    public class UserData
    {
        public static Guid BusinessParentId { get; set; }
        public static Guid ClientParentId { get; set; }
        public static LoginModel Current { get; set; }
        public static bool TwoFactor { get; set; }
        public static InputModel _InputModel { get; set; }
        public static string SelectedLanguage { get; set; }
        public static bool CurrencyandVat { get; set; }
        public static bool CompanySetup { get; set; }
        public static List<ModuleRightLookUpModel> Permissions {get; set;}

        public static bool CheckConnectivity()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }
    }
}
