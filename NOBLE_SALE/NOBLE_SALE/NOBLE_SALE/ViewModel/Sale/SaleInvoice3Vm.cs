using NOBLE_SALE.Helper;
using NOBLE_SALE.Model;
using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Model.Sale;
using NOBLE_SALE.PDFReports;
using NOBLE_SALE.Services;
using NOBLE_SALE.View.Sale;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.ViewModel.Sale
{
    class SaleInvoice3Vm : BaseViewModel
    {

        private SaleLookupModel _Sale;

        public SaleLookupModel Sale
        {
            get { return _Sale; }
            set 
            {
                _Sale = value;
                OnPropertyChanged();
            }
        }

        private SalePaymentLookupModel _SalePayment;

        public SalePaymentLookupModel SalePayment
        {
            get { return _SalePayment; }
            set 
            {
                _SalePayment = value;
                OnPropertyChanged();
            }
        }
        public Command SaveSaleHandler { get; set; }

        private decimal _due;

        public decimal due
        {
            get { return _due; }
            set 
            { 
                _due = value;
                OnPropertyChanged();
            }
        }

        private decimal _balance;

        public decimal balance
        {
            get { return due-recieved; }
            //set 
            //{ 
            //    _balance = value;
            //    OnPropertyChanged();
            //}
        }

        private decimal _recieved;

        public decimal recieved
        {
            get { return _recieved; }
            set
            {
                _recieved = value;
                OnPropertyChanged("recieved");
                OnPropertyChanged("balance");
            }
        }

        private decimal _cash;

        public decimal cash
        {
            get { return _cash; }
            set 
            { 
                _cash = value;
                OnPropertyChanged();
            }
        }

        private decimal _bank;

        public decimal bank
        {
            get { return _bank; }
            set 
            {
                _bank = value;
                OnPropertyChanged();
            }
        }




        private decimal _change;

        public decimal change
        {
            get { return _change; }
            set
            {
                _change = value;
                OnPropertyChanged();
            }
        }
        public PaymentTypeLookupModel Payment { get; set; }


        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private PaymentOptionsListModel _PaymentOption;

        public PaymentOptionsListModel PaymentOption
        {
            get { return _PaymentOption; }
            set 
            { 
                _PaymentOption = value;
                OnPropertyChanged();
            }
        }

        private bool _IsCash;

        public bool IsCash
        {
            get { return _IsCash; }
            set 
            { 
                _IsCash = value;
                OnPropertyChanged();
            }
        }

        private bool _IsBank;

        public bool IsBank
        {
            get { return _IsBank; }
            set 
            { 
                _IsBank = value;
                OnPropertyChanged();
            }
        }

        private bool _IsMasterCard;

        public bool IsMasterCard
        {
            get { return _IsMasterCard; }
            set 
            { 
                _IsMasterCard = value;
                OnPropertyChanged();
            }
        }

        private bool _IsVisa;

        public bool IsVisa
        {
            get { return _IsVisa; }
            set 
            { 
                _IsVisa = value;
                OnPropertyChanged();
            }
        }

        private bool _IsMada;

        public bool IsMada
        {
            get { return _IsMada; }
            set 
            { 
                _IsMada = value;
                OnPropertyChanged();
            }
        }
        private bool _IsStcPay;

        public bool IsStcPay
        {
            get { return _IsStcPay; }
            set 
            { 
                _IsStcPay = value;
                OnPropertyChanged();
            }
        }

        public SaleDetailLookupModel SaleDetail { get; set; }   




        public Command CashHandler { get; set; }
        public Command BankHandler { get; set; }
        public Command MasterCardHandler { get; set; }
        public Command VisaHandler { get; set; }
        public Command MadaHandler { get; set; }
        public Command StcHandler { get; set; }


        public decimal Total { get; set; }

        public decimal TotalVat { get; set; }

        private int _TotalItems;
        public int TotalItems
        {
            get { return _TotalItems; }
            set
            {
                _TotalItems = value;
                OnPropertyChanged();
            }
        }

        private int _TotalQuantity;
        public int TotalQuantity
        {
            get { return _TotalQuantity; }
            set
            {
                _TotalQuantity = value;
                OnPropertyChanged();
            }
        }



        private PDFToHtml PDFToHtml { get; set; }

        public SaleInvoice3Vm(SaleLookupModel sale)
        {
            //Total = total;
            //TotalItems = totalItems;
            //TotalVat = totalvat;
            PaymentOption = new PaymentOptionsListModel();
            GetData();
            IsMasterCard = false;
            IsVisa = false;
            IsMada = false;
            IsStcPay = false;
            IsBank = false;
            IsCash = true;
            IsBusy = false;
            Sale = new SaleLookupModel();
            Payment = new PaymentTypeLookupModel();
            Sale = sale;
            due = new decimal();
            due = sale.SalePayment.DueAmount;
            SaveSaleHandler = new Command(SaveSaleCommand);
            CashHandler = new Command(CashCommand);
            BankHandler = new Command(BankCommand);
            MasterCardHandler = new Command(MasterCardCommand);
            VisaHandler = new Command(VisaCommand);
            MadaHandler = new Command(MadaCommand);
            StcHandler = new Command(StcCommand);
        }

        private void StcCommand(object obj)
        {
            IsStcPay = true;
            IsMada = false;
            IsMasterCard = false;
            IsVisa = false;
        }

        private void MadaCommand(object obj)
        {
            IsStcPay = false;
            IsMada = true;
            IsMasterCard = false;
            IsVisa = false;
        }

        private void VisaCommand(object obj)
        {
            IsStcPay = false;
            IsMada = false;
            IsMasterCard = false;
            IsVisa = true;
        }

        private void MasterCardCommand(object obj)
        {
            IsStcPay = false;
            IsMada = false;
            IsMasterCard = true;
            IsVisa = false;
        }

        private void BankCommand(object obj)
        {
            cash = 0;
            IsBank = true;
        }

        private void CashCommand(object obj)
        {
            bank = 0;
            IsBank = false;
        }

        private async void GetData()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var services = new ProductService();
            PaymentOption = await services.GetPaymentOptions();
            isBusy = false;
        }


        private void GetCalculations(SaleDetailLookupModel saleDetail)
        {
            foreach (var item in saleDetail.SaleItems)
            {
                if (item.TaxMethod == "Inclusive")
                {
                    Total = Total + item.UnitPrice;
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * item.TaxRate) / (100 + item.TaxRate));
                }
                else
                {
                    var itemvat = ((item.UnitPrice * item.Quantity * item.TaxRate) / 100);
                    TotalVat = TotalVat + ((item.UnitPrice * item.Quantity * item.TaxRate) / 100);
                    Total = Total + item.UnitPrice + itemvat;
                }
            }
        }

        private async void SaveSaleCommand(object obj)
        {

            if (IsBusy)
                return;

            IsBusy = true;

            if (!IsBank)
            {
                Payment.Amount = recieved;
                Payment.Id = Guid.Empty;
                Payment.PaymentType = SalePaymentType.Cash;
                Sale.SalePayment.PaymentType = SalePaymentType.Cash;
            }
            else
            {
                Sale.SalePayment.PaymentType = SalePaymentType.Bank;
                if (IsMasterCard)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[1].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "MasterCard";
                }
                if (IsVisa)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[2].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "Visa";
                }
                if (IsMada)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[0].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "Mada";
                }
                if (IsStcPay)
                {
                    Sale.SalePayment.PaymentOptionId = PaymentOption.PaymentOptions[0].Id;
                    Payment.Amount = recieved;
                    Payment.Id = Guid.Empty;
                    Payment.PaymentType = SalePaymentType.Bank;
                    Payment.Name = "STC Pay";
                }
            }

            if (balance < 0)
            {
                Sale.Change = balance * -1;
            }
            else
            {
                Sale.Change = balance;
            }


            Sale.SalePayment.PaymentTypes = new List<PaymentTypeLookupModel>();
            Sale.SalePayment.PaymentTypes.Add(Payment);

            var service = new ProductService();
            
            var response = await service.SaveSale(Sale);

            if (response!=null)
            {
                SaleDetail = await service.GetSaleDetail(Guid.Parse(response));
                //await App.Current.MainPage.DisplayAlert("Message", "Sale Successfull", "ok");
                
                
                GetCalculations(SaleDetail);
                //var pdfReport = new SalesReport(SaleDetail);



                //await pdfReport.CreateReport();

                StringBuilder sb = new StringBuilder();

                sb.Append("<div>");
                sb.Append("<div style=\"width: 400px; z-index:-9999;\">");

                sb.Append("<div  style=\"width: 400px; margin-left:20px; \">");
                sb.Append("<div style=\"text-align: center;margin-bottom:3px; \">");
                sb.Append("<div style=\"text-align: center; \">");
                sb.Append("<span style=\"font-size:30px;font-weight:bold;color:black; \">Business Name (Ar)<br /></span>");
                sb.Append("<span style=\"font-size:30px;font-weight:bold;color:black; \">Business Name (Eng)<br /></span>");
                sb.Append("<span style=\"font-size:18px;font-weight:bold;color:black; \">Business Type (Ar)<br /></span>");
                sb.Append("<span style=\"font-size:18px;font-weight:bold;color:black; \">Business Type (Eng)<br /></span>");
                sb.Append("<span style=\"font-size:18px;font-weight:bold;color:black; \">Welcome/Tagline (Ar))<br /></span>");
                sb.Append("<span style=\"font-size:18px;font-weight:bold;color:black; \">Welcome/Tagline (Eng)<br /></span>");
                sb.Append("<span style=\"font-size:15px;color:black; \"><span>Management No:</span><span> :رقم الإدارة</span><br /></span>");
                sb.Append("<span style=\"font-size:15px;color:black; \"><span>Store Contact Number: </span><span> :الـرقم الإدارة / المحل</span><br /></span>");
                sb.Append("<p style=\"border:1px solid; width:350px;color:black;margin-left:auto;margin-right:auto;margin-top:0.1rem;margin-bottom:0.3rem;\">");
                sb.Append("<span style=\"font-weight: bold; \"><span style=\"font-weight: bold; \">VAT No :</span><span style=\"font-weight: bold; \"> :الرقم الضريبي</span><br /></span>");
                sb.Append("<span style=\"font-weight: bold; \"><span style=\"font-weight: bold; \">CR Number  :</span><span style=\"font-weight: bold; \"> :الرقـم السـجـل الـتـجـارى</span><br /></span>");
                sb.Append("<span style=\"font-size:18px;color:black; \">Company Name (Ar)<br /></span>");
                sb.Append("<span style=\"font-size:18px;color:black; \">Company Name (Eng)<br /></span>");
                sb.Append("<span style=\"font-size:18px;color:black; \">Company Address (Ar)<br /></span>");
                sb.Append("<span style=\"font-size:18px;color:black; \">Company Address (Eng)<br /></span>");
                sb.Append("</p>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div style=\"width:100%;\">");
                sb.Append("<div style=\"text-align: center;\">");
                sb.Append("<p style=\"font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;\"><span> Invoice No.</span><span style=\"font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;text-align:right \"> رقم الفاتورة</span></p>");
                sb.Append("<p style=\"border: 1px solid; font-size: 15px; font-weight: bold; color: black; margin-bottom: 2px; padding-bottom: 2px; text-align: center\"><span> Invoice No.<span style=\"font-size: 18px; \">registrationNo</span></span></p>");
                sb.Append("<p style=\"border: 1px solid; font-size: 15px; font-weight: bold; color: black; margin-bottom: 2px; padding-bottom: 2px; text-align: center\"><span><span style=\"font-size: 18px; \">registrationNo:</span>رقم الفاتورة</span></p>");
                sb.Append("<p style=\"font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;\"><span> Invoice No.</span><span style=\"border:4px solid;font-size:25px;font-weight:bold;padding-left:2px;padding-right:2px;color:black;text-align:center; \">registrationNo</span> <span style=\"font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;text-align:right;\">رقم الفاتورة</span></p>");
                sb.Append("<barcode style=\"width:2rem; height:30px;\"></barcode>");
                sb.Append("</div>");
                sb.Append("<table style=\"width:400px;\">");
                sb.Append("<tr style=\"font-size:16px;\">");
                sb.Append("<td style=\"border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:right;padding-bottom:0px !important;\">1446: رقم الكاونتر</td>");
                sb.Append("<td style=\"text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;padding-bottom:0px !important;\">Hamza: أسم المستخد </td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:16px;\">");
                sb.Append("<td style=\"border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:left;padding-top:0px !important;padding-bottom:0px !important;\">11-1-2:</td>");
                sb.Append("<td style=\"text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;padding-top:0px !important;padding-bottom:0px !important\">550: اسم العميل </td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:18px;\">");
                sb.Append("<td style=\"border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:left\">11-1-2</td>");
                sb.Append("<td style=\"text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;\">: اسم العميل</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("<div style=\"width:100%;\">");
                sb.Append("<table style=\"width:400px;margin-top:15px;\">");
                sb.Append("<tr style=\"font-size:15px;border-bottom:1px solid black;border-top:1px solid; color:black;\" >");
                sb.Append("<th style=\"text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;\">الإجـمـالي </th>");
                sb.Append("<th style=\"text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;\"> الضـريبـة </th>");
                sb.Append("<th style=\"text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;\">السـعـر</th>");
                sb.Append("<th style=\"text-align: center; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;\">الكـمـية</th>");
                sb.Append("<th style=\"text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;\">الصـنـف</th>");
                sb.Append("<th style=\"text-align: right; width: 5%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;\">رقـم </th>");
                sb.Append("</tr>");
                //sb.Append("<div>");
                sb.Append("<tr style=\"font-size:15px;\">");
                sb.Append("<td style=\"text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;\">item.total</td>");
                sb.Append("<td style=\"text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;\">item.including</td>");
                sb.Append("<td style=\"text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;\">item.unitPrice</td>");
                sb.Append("<td style=\"text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;\">item.quantity</td>");
                sb.Append("<td style=\"text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; \">item.code</td>");
                sb.Append("<td style=\"text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;\">index+1</td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:14px;\"><td colspan=\"7\" style=\"text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;\">arabicName &nbsp; &nbsp; &nbsp; productName</td></tr>");
                sb.Append("<tr style=\"font-size:14px;\"><td colspan=\"7\" style=\"text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;\">arabicName</td></tr>");
                sb.Append("<tr style=\"font-size:14px;\"><td colspan=\"7\" style=\"text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;\">productName</td></tr>");
                sb.Append("<tr style=\"font-size:14px;\"><td colspan=\"7\" style=\"text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;\">arabicName &nbsp; &nbsp; &nbsp; productName</td></tr>");
                //sb.Append("</div>");
                sb.Append("<tr style=\"font-size:16px;border-top:1px solid #000000;border-color:black !important;\">");
                sb.Append("<td colspan=\"1\" style=\" text-align:right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center;\">12</td>");
                sb.Append("<td colspan=\"5\" style=\"text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important;\">عدد الصـنـف</td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:16px;\">");
                sb.Append("<td colspan=\"1\" style=\"text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center;\">TotalExclVAT</td>");
                sb.Append("<td colspan=\"5\" style=\"text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important;\"> Total without VAT / Tax  الإجمالي بدون الضريبة</td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:16px;\">");
                sb.Append("<td colspan=\"1\" style=\"text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center;\">DiscountAmount</td>");
                sb.Append("<td colspan=\"5\" style=\"text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important;\">الـخـصـم</td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:16px;\">");
                sb.Append("<td colspan=\"1\" style=\"text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center;\">20</td>");
                sb.Append("<td colspan=\"5\" style=\"text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important;\">Total after Discount  المجموع بعد الخصم</td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:16px;\">");
                sb.Append("<td colspan=\"1\" style\" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center;\">calulateTotalVAT</td>");
                sb.Append("<td colspan=\"5\" style=\"text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important;\"> Total VAT / Tax   إجمالي الضريبة</td>");
                sb.Append("</tr>");
                sb.Append("<tr style=\"font-size:16px;\">");
                sb.Append("<td colspan=\"1\" style=\"text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center;\">40</td>");
                sb.Append("<td colspan=\"5\" style=\"text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important;\"> Total with VAT / Tax   الإجمالي مع  الضريبة </td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div style=\"width:400px;border-top:1px solid #000000;border-color:black !important;\">");
                sb.Append("<div style=\"margin-top:15px;display:flex;\">");
                sb.Append("<div style=\"width:400px;display:block;margin-left:20px;margin-top:1.5rem;text-align:center;\">");
                sb.Append("<p style=\"text-align:center;color:black;\"> <span style=\"margin-right:5px;color:black;\">*</span> <span style=\"margin-right:10px;color:black;\">*</span> <span style=\"font-size:25px; font-weight:bold;color:black;\">--------</span>  <span style=\"margin-left: 10px;color:black;\">*</span> <span style=\"margin-left: 5px;color:black;\">*</span> </p>");
                sb.Append("<p  style=\"text-align:center;color:black;\"> <span style=\"font-size:25px; font-weight:bold;color:black;\">-----</span>   </p>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");


                //sb.Append("<header class='clearfix'>");

                //sb.Append("<header class='clearfix'>");




                //sb.Append("<h1>INVOICE</h1>");
                //sb.Append("<div id='company' class='clearfix'>");
                //sb.Append("<div>Company Name</div>");
                //sb.Append("<div>Leads Center,<br /> AZ 85004, US</div>");
                //sb.Append("<div>(+92) 310472-4361</div>");
                //sb.Append("<div><a href='mailto:company@techqode.com'>company@example.com</a></div>");
                //sb.Append("</div>");
                //sb.Append("<div id='project'>");
                //sb.Append("<div><span>PROJECT</span> Website development</div>");
                //sb.Append("<div><span>CLIENT</span> John Doe</div>");
                //sb.Append("<div><span>ADDRESS</span> 796 Silver Harbour, TX 79273, US</div>");
                //sb.Append("<div><span>EMAIL</span> <a href='mailto:john@example.com'>john@example.com</a></div>");
                //sb.Append("<div><span>DATE</span> April 13, 2016</div>");
                //sb.Append("<div><span>DUE DATE</span> May 13, 2016</div>");
                //sb.Append("</div>");
                //sb.Append("</header>");
                //sb.Append("<main>");
                //sb.Append("<table>");
                //sb.Append("<thead>");
                //sb.Append("<tr>");
                ////sb.Append("<th class='service'>Product</th>");
                //sb.Append("<th class='desc' >DESCRIPTION</th>");
                //sb.Append("<th colspan='2'>PRICE</th>");
                //sb.Append("<th colspan='2'>QTY</th>");
                //sb.Append("<th colspan='2'>TOTAL</th>");
                //sb.Append("</tr>");
                //sb.Append("</thead>");
                //sb.Append("<tbody>");

                //foreach (var item in SaleDetail.SaleItems)
                //{
                //    sb.Append("<tr>");
                //    //sb.Append("<td class='service'>" + item.Code + "</td>");
                //    sb.Append("<td class='desc'>" + item.ProductName + "</td>");
                //    sb.Append("<td class='unit'colspan='2' >SAR " + String.Format("{0:0.00}", item.UnitPrice) + "</td>");
                //    sb.Append("<td class='total' colspan='2' style='margin-left: 2.5em;'> " + String.Format("{0:0.00}", item.Quantity) + "</td>");
                //    sb.Append("<td class='total' colspan='2'>SAR " + String.Format("{0:0.00}", item.UnitPrice) + "</td>");
                //    sb.Append("</tr>");
                //    sb.Append("<tr>");
                //}



                //sb.Append("<td colspan='4'>SUBTOTAL</td>");
                //sb.Append("<td class='total'>SAR " + String.Format("{0:0.00}", Total-TotalVat)+"</td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td colspan='4'>TAX 15%</td>");
                //sb.Append("<td class='total'>SAR "+ String.Format("{0:0.00}", TotalVat) +"</td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td colspan='4' class='grand total'>GRAND TOTAL</td>");
                //sb.Append("<td class='grand total'>SAR"+ String.Format("{0:0.00}", Total) + "</td>");
                //sb.Append("</tr>");
                //sb.Append("</tbody>");
                //sb.Append("</table>");
                //sb.Append("<div id='notices'>");
                //sb.Append("<div>NOTICE:</div>");
                //sb.Append("<div class='notice'>A finance charge of 1.5% will be made on unpaid balances after 30 days.</div>");
                //sb.Append("</div>");
                //sb.Append("</main>");
                //sb.Append("<footer>");
                //sb.Append("Invoice was created on a computer and is valid without the signature and seal.");
                //sb.Append("</footer>");

                //sb.Append("<div style='width: 100 %; '> < div style = 'text-align: center;' >< span style = 'font-size:30px;font-weight:bold;color:black;' >Company Name</ span >< br />");






                isBusy = false;
                await Application.Current.MainPage.Navigation.PushAsync(new SaleDetailPopupPage(due, recieved, balance, sb.ToString()));
                
                
            
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Contact Support", "ok");
                isBusy = false;
            }
        }
    }
}
