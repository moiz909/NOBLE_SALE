using NOBLE_SALE.Helper;
using NOBLE_SALE.Model.Company;
using NOBLE_SALE.Model.SetupSteps;
using NOBLE_SALE.Services;
using NOBLE_SALE.View;
using System;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.SetupSteps
{
    class SetupCompanyVm : BaseViewModel
    {
        private CompanyDto _Company;

        public CompanyDto Company
        {
            get { return _Company; }
            set { _Company = value;  }
        }

        private NOBLE_SALE.Model.SetupSteps.BusinessVm _CompanyInfo;

        public NOBLE_SALE.Model.SetupSteps.BusinessVm CompanyInfo
        {
            get { return _CompanyInfo; }
            set { _CompanyInfo = value;OnPropertyChanged(); }
        }

        public StepsVm Steps { get; set; }

        public Command UpdateCommand { get; set; }

        private string _BusinessName;

        public string BusinessName
        {
            get { return _BusinessName; }
            set { _BusinessName = value; OnPropertyChanged(); }
        }

        private string _CommercialRegNo;

        public string CommercialRegNo
        {
            get { return _CommercialRegNo; }
            set { _CommercialRegNo = value; OnPropertyChanged(); }
        }

        private string _TaxNo;

        public string TaxNo
        {
            get { return _TaxNo; }
            set { _TaxNo = value; OnPropertyChanged(); }
        }

        private bool validateName;
        public bool ValidateName
        {
            get
            {
                return validateName;
            }
            set
            {
                validateName = value;
                OnPropertyChanged();
            }
        }

        private bool validateCategory;
        public bool ValidateCategory
        {
            get
            {
                return validateCategory;
            }
            set
            {
                validateCategory = value;
                OnPropertyChanged();
            }
        }

        private bool validateAddress;
        public bool ValidateAddress
        {
            get
            {
                return validateAddress;
            }
            set
            {
                validateAddress = value;
                OnPropertyChanged();
            }
        }

        public SetupCompanyVm()
        {
            ValidateAddress = true;
            ValidateCategory = true;
            ValidateName = true;

            GetCompanyDetail();
            Company = new CompanyDto();
            CompanyInfo = new NOBLE_SALE.Model.SetupSteps.BusinessVm();
            Steps = new StepsVm();
            UpdateCommand = new Command(UpdateHandler);
        }

        private async void UpdateHandler(object obj)
        {
            if (CompanyInfo.CompanyNameEnglish == null || CompanyInfo.CompanyNameEnglish == string.Empty)
            {
                ValidateName = false;
            }
            if (CompanyInfo.CategoryInEnglish == null || CompanyInfo.CategoryInEnglish == string.Empty)
            {
                ValidateCategory = false;
            }
            if (CompanyInfo.AddressInEnglish == null || CompanyInfo.AddressInEnglish == string.Empty)
            {
                ValidateAddress = false;
            }

            if(ValidateName && ValidateCategory && ValidateAddress)
            {
                CompanyInfo.NameInArabic = CompanyInfo.NameInEnglish;
                CompanyInfo.CategoryInArabic = string.Empty;
                CompanyInfo.AddressInArabic = string.Empty;
                CompanyInfo.CompanyNameArabic = CompanyInfo.CompanyNameEnglish;
                CompanyInfo.CountryInEnglish = string.Empty;
                CompanyInfo.CountryInArabic = string.Empty;
                CompanyInfo.LandLine = string.Empty;
               
                var service = new SetupService();
                var response = await service.UpdateCompany(CompanyInfo);
                if (response)
                {
                    UserData.CompanySetup = false;
                    Steps.CompanyId = UserData.Current.CompanyId;
                    Steps.Step2 = true;
                    response = await service.UpdateSteps(Steps);
                    if (response)
                    {


                        response = await service.SetFinancialYear();
                        if (response)
                        {
                            Steps.CompanyId = UserData.Current.CompanyId;
                            Steps.Step5 = true;
                            response = await service.UpdateSteps(Steps);
                            if (response)
                            {
                                await Application.Current.MainPage.Navigation.PopAsync();
                                await Application.Current.MainPage.Navigation.PopAsync();
                                await Application.Current.MainPage.Navigation.PushAsync(new SetupPage());
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                            }

                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                        }

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                    }
                }

                else
                {
                    await Application.Current.MainPage.DisplayAlert("errorss", "Request Failed", "ok");
                }
            }
            
        }

        private async void GetCompanyDetail()
        {
            var service = new SetupService();
            Company = await service.GetCompanyDetail(UserData.Current.CompanyId);
            CompanyInfo.Id = Company.Id.ToString();
            BusinessName = Company.NameEnglish;
            CommercialRegNo = Company.VatRegistrationNo;
            TaxNo = Company.VatRegistrationNo;
            CompanyInfo.NameInEnglish = Company.NameEnglish;
            CompanyInfo.CompanyRegNo = Company.VatRegistrationNo;
            CompanyInfo.VatRegistrationNo = Company.VatRegistrationNo;

        }
    }
}
