using NOBLE_SALE.Model.Company;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Registration
{
    class RegisterLocationVM : BaseViewModel
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidReg;
        public bool IsValidReg
        {
            get
            {
                return _IsValidReg;
            }
            set
            {
                _IsValidReg = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidEmail;
        public bool IsValidEmail
        {
            get
            {
                return _IsValidEmail;
            }
            set
            {
                _IsValidEmail = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidVat;
        public bool IsValidVat
        {
            get
            {
                return _IsValidVat;
            }
            set
            {
                _IsValidVat = value;
                OnPropertyChanged();
            }

        } 
        
        private bool _IsValidBuisnessName;
        public bool IsValidBuisnessName
        {
            get
            {
                return _IsValidBuisnessName;
            }
            set
            {
                _IsValidBuisnessName = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidBuisnessEmail;
        public bool IsValidBuisnessEmail
        {
            get
            {
                return _IsValidBuisnessEmail;
            }
            set
            {
                _IsValidBuisnessEmail = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidNumber;
        public bool IsValidNumber
        {
            get
            {
                return _IsValidNumber;
            }
            set
            {
                _IsValidNumber = value;
                OnPropertyChanged();
            }

        }
        
        private bool _IsValidAddress;
        public bool IsValidAddress
        {
            get
            {
                return _IsValidAddress;
            }
            set
            {
                _IsValidAddress = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidUsername;
        public bool IsValidUsername
        {
            get
            {
                return _IsValidUsername;
            }
            set
            {
                _IsValidUsername = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidPassword;
        public bool IsValidPassword
        {
            get
            {
                return _IsValidPassword;
            }
            set
            {
                _IsValidPassword = value;
                OnPropertyChanged();
            }

        }

        private bool _IsValidConfirmPassword;
        public bool IsValidConfirmPassword
        {
            get
            {
                return _IsValidConfirmPassword;
            }
            set
            {
                _IsValidConfirmPassword = value;
                OnPropertyChanged();
            }

        }

        private BusinessVm business;
        public BusinessVm Business
        {
            set
            {
                business = value;
                OnPropertyChanged();
            }
            get
            {
                return business;
            }
        }


        public ICommand SignUpHandler { get; set; }
           
        public RegisterLocationVM()
        {
            IsValidEmail = true;
            IsValidReg = true;
            IsValidVat = true;
            IsValidBuisnessName = true;
            IsValidBuisnessEmail = true;
            IsValidNumber = true;
            IsValidAddress = true;
            IsValidUsername = true;
            IsValidPassword = true;
            IsValidConfirmPassword = true;

            IsBusy = false;
            Business = new BusinessVm();
            SignUpHandler = new Command(async () => await SignUpCommand());
        }

        private async Task SignUpCommand()
        {
            IsBusy = true;

            if (Business.NameInEnglish == null || Business.NameInEnglish == string.Empty)
            {
                IsValidBuisnessName = false;
            }
            if (Business.CompanyRegNo == null || Business.CompanyRegNo == string.Empty)
            {
                IsValidReg = false;
            }
            if (Business.VatRegistrationNo == null || Business.VatRegistrationNo == string.Empty)
            {
                IsValidVat = false;
            }
            if (Business.Email == null || Business.Email == string.Empty)
            {
                IsValidBuisnessEmail = false;
            }
            if (Business.UserEmail == null || Business.UserEmail == string.Empty)
            {
                IsValidEmail = false;
            }
            if (Business.LandLine == null || Business.LandLine == string.Empty)
            {
                IsValidNumber = false;
            }
            if (Business.AddressInEnglish == null || Business.AddressInEnglish == string.Empty)
            {
                IsValidAddress = false;
            }
            if (Business.FirstName == null || Business.FirstName == string.Empty)
            {
                IsValidUsername = false;
            }
            if (Business.Password == null || Business.Password == string.Empty) 
            {
                IsValidPassword = false;
            }
            if (Business.Password != Business.ConfirmPassword) 
            {
                IsValidConfirmPassword = false;
            }    
            else
                IsValidConfirmPassword = true;

            IsBusy = false;
        }
    }
}
