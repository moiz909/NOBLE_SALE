using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;
using NOBLE_SALE.Helper;
using NOBLE_SALE.Interfaces;
using NOBLE_SALE.Model.Product;
using NOBLE_SALE.Model.Sale;
using PdfSharpCore.Fonts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NOBLE_SALE.PDFReports
{
    public class SalesReport
    {
        private Document document;
        private SaleDetailLookupModel SaleDetail;
        private decimal Total;
        private decimal TotalItems;
        private decimal TotalVat;
        public static bool FontResolverAlreadySet { get; set; }
        public SalesReport(SaleDetailLookupModel products)
        {

            if (!FontResolverAlreadySet)
            {
                GlobalFontSettings.FontResolver = new GenericFontResolver();
                FontResolverAlreadySet = true;
            }
            
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            this.SaleDetail = products;
            //this.Total = total;
            //this.TotalItems = totalItems;
            //this.TotalVat = totalvat;
        }

        public async Task CreateReport()
        {
            CreateDocument();
            //SetStyles();

            AddHeader();
            AddContent();
            AddFooter();

            await SaveShowPDF();
        }
        private void CreateDocument()
        {
            document = new Document();
            document.Info.Title = "Product Catalog 2021 - Tech Solutions, Inc.";
            document.Info.Subject = "We present you the Product Catalog for this year.";
            document.Info.Author = "Luis Beltran";
            document.Info.Keywords = "Products";
        }

        private void SetStyles()
        {
            // Modifying default style
            Style style = document.Styles["Normal"];
            style.Font.Name = "OpenSans";
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.PageBreakBefore = false;

            // Header style
            style = document.Styles[StyleNames.Header];
            style.Font.Name = "OpenSans";
            style.Font.Size = 18;
            style.Font.Color = Colors.Black;
            style.Font.Bold = true;
            style.Font.Underline = Underline.Single;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            // Footer style
            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Right);

            // Modifying predefined style: HeadingN (where N goes from 1 to 9)
            style = document.Styles["Heading1"];
            style.Font.Name = "OpenSans";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Italic = false;
            style.Font.Color = Colors.DarkBlue;
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Color = Colors.CadetBlue;
            style.ParagraphFormat.SpaceAfter = "1cm";

            // Modifying predefined style: Heading2
            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = false;
            style.Font.Italic = true;
            style.Font.Color = Colors.DeepSkyBlue;
            style.ParagraphFormat.Shading.Color = Colors.White;
            style.ParagraphFormat.Borders.Width = 0;
            style.ParagraphFormat.SpaceAfter = 3;
            style.ParagraphFormat.SpaceBefore = 3;

            // Adding new style
            style = document.Styles.AddStyle("MyParagraphStyle", "Normal");
            style.Font.Size = 10;
            style.Font.Color = Colors.Blue;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles.AddStyle("MyTableStyle", "Normal");
            style.Font.Size = 9;
            style.Font.Color = Colors.SlateBlue;
        }


        private void AddHeader()
        {
            var section = document.AddSection();

            var config = section.PageSetup;
            config.Orientation = Orientation.Portrait;
            config.TopMargin = "3cm";
            config.LeftMargin = 15;
            config.BottomMargin = "3cm";
            config.RightMargin = 15;
            config.PageFormat = PageFormat.A4;
            config.OddAndEvenPagesHeaderFooter = true;
            config.StartingNumber = 1;

            var oddHeader = section.Headers.Primary;

            var content = new Paragraph();
            content.AddText("\t Sale Invoice\t");
            oddHeader.Add(content);
            oddHeader.AddTable();

            var headerForEvenPages = section.Headers.EvenPage;
            headerForEvenPages.AddParagraph("Sale Invoice");
            headerForEvenPages.AddTable();
        }


        void AddContent()
        {
            //AddText1();
            //AddText2();
            AddTable();
            //AddCalculation();


        }



        private void AddCalculation()
        {
            //// Add an invisible row as a space line to the table
            //var section = document.LastSection;
            //var table = section.AddTable();
            //Row row = table.AddRow();
            //row.Borders.Visible = false;

            //// Add the total price row
            //row = table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("Total");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //row.Cells[5].AddParagraph(SaleDetail.SalePayment.DueAmount.ToString("0.00") + " SAR");

            //row = table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("Amount Paid");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //row.Cells[5].AddParagraph(SaleDetail.PaymentAmount + " SAR");


            //row = table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("Change");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //row.Cells[5].AddParagraph(SaleDetail.Change + " SAR");

            // Add the total price row
            var section = document.LastSection;
            var table = section.AddTable();
            var column = table.AddColumn("5cm");
            var row = table.AddRow();
            row.Borders.Visible = false;
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Total");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;
            row.Cells[1].AddParagraph(SaleDetail.SalePayment.DueAmount.ToString("0.00") + " SAR");

            row = table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Amount Paid");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;
            row.Cells[1].AddParagraph(SaleDetail.PaymentAmount + " SAR");


            row = table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Change");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;
            row.Cells[1].AddParagraph(SaleDetail.Change + " SAR");

        }

        private void AddTable()
        {
            var titles = new string[] { "Product", "Name", "Price" };
            var borderColor = new Color(81, 125, 192);

            var section = document.LastSection;

            var table = section.AddTable();
            table.Style = "MyTableStyle";
            table.Borders.Color = borderColor;
            table.Borders.Visible = true;
            table.Borders.Width = 0.75;
            table.Rows.LeftIndent = 5;

            var column = table.AddColumn("5cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("10cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            table.Rows.HeightRule = RowHeightRule.Exactly;
            table.Rows.Height = "1cm";

            var headerRow = table.AddRow();
            headerRow.HeadingFormat = true;
            headerRow.Format.Alignment = ParagraphAlignment.Center;
            headerRow.Format.Font.Bold = true;

            for (int i = 0; i < titles.Length; i++)
            {
                headerRow.Cells[i].AddParagraph(titles[i]);
                headerRow.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                headerRow.Cells[i].VerticalAlignment = VerticalAlignment.Center;
                headerRow.Shading.Color = Colors.PaleGoldenrod;
                headerRow.Borders.Width = 1;
            }

            foreach (var item in SaleDetail.SaleItems)
            {
                var rowItem = table.AddRow();
                rowItem.TopPadding = 1.5;
                rowItem.Borders.Left.Width = 0.25;

                var titleCell = rowItem.Cells[0];
                titleCell.AddParagraph(item.Code);

                var cellAutor = rowItem.Cells[1];
                cellAutor.AddParagraph(item.ProductName);

                var cellFecha = rowItem.Cells[2];
                cellFecha.AddParagraph(item.UnitPrice.ToString("C2"));

                //cellFecha.AddParagraph(string.Format(new CultureInfo("ar-SA"), "{0:C}", item.UnitPrice));

                
            }

            var row = table.AddRow();
            row.Borders.Visible = false;

            // Add an invisible row as a space line to the table
            row.Borders.Visible = false;
        }

        private void AddFooter()
        {
            var content = new Paragraph();
            content.AddText(" Page ");
            content.AddPageField();
            content.AddText(" of ");
            content.AddNumPagesField();

            var section = document.LastSection;
            section.Footers.Primary.Add(content);

            

            var contentForEvenPages = content.Clone();
            contentForEvenPages.AddTab();
            contentForEvenPages.AddText("\tDate: ");
            contentForEvenPages.AddDateField("dddd, MMMM dd, yyyy HH:mm:ss tt");

            section.Footers.EvenPage.Add(contentForEvenPages);
        }

        //private void AddText1()
        //{
        //    var text = "At Tech Solutions Inc, it is our top priority to bring only products of the highest quality to our customers. Products always pass a strict quality control process before they are delivered to you. We put ourselves in the customer's shoes, and only want to offer products that will make our clients happy.";
        //    var section = document.LastSection;
        //    var mainParagraph = section.AddParagraph(text, "Heading1");
        //    mainParagraph.AddLineBreak();

        //    text = "All components of Tech Solutions Inc sample products have undergone strict laboratory tests for lead, nickel and cadmium content. A world-leading inspection, testing, and certification company has conducted these testsm and as you can see below, our products have passed with perfect note.";
        //    section.AddParagraph(text, "Heading2");
        //}

        //private void AddText2()
        //{
        //    var seccion = document.LastSection;

        //    var texto = "We recommend customers to purchase products only from reliable sources where products have been tested, and only when the lead, nickel and cadmium content have passed the laboratory tests. Your health is important.";
        //    var parrafo = seccion.AddParagraph(texto, "MyParagraphStyle");

        //    texto = "\nWearing products that are not tested, or have failed to meet regulatory standards may bring harm to your health and skin";
        //    parrafo = seccion.AddParagraph(texto, "MyParagraphStyle");
        //    parrafo.AddLineBreak();
        //}

        

        private async Task SaveShowPDF()
        {
            var file = Xamarin.Forms.DependencyService.Get<IFile>();
            var fileName = $"{Guid.NewGuid()}.pdf";
            var filePath = await file.GetLocalPath(fileName);

            PdfDocumentRenderer printer = new PdfDocumentRenderer();
            printer.Document = document;
            printer.RenderDocument();
            printer.PdfDocument.Save(filePath);

            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
        }

    }
}
