using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using NOBLE_SALE.Droid.Helper.Dependency;
using NOBLE_SALE.Droid.Helper.Native;
using NOBLE_SALE.Helper.Enum;
using NOBLE_SALE.Helper.Interface;
using NOBLE_SALE.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(PDFConverter))]
namespace NOBLE_SALE.Droid.Helper.Dependency
{
    public class PDFConverter : IPDFConverter
    {
        public void ConvertHTMLtoPDF(PDFToHtml _PDFToHtml)
        {
            try
            {
                var webpage = new Android.Webkit.WebView(Android.App.Application.Context);
                webpage.Settings.JavaScriptEnabled = true;

#pragma warning disable CS0618 // Type or member is obsolete
                webpage.DrawingCacheEnabled = true;
#pragma warning restore CS0618 // Type or member is obsolete

                webpage.SetLayerType(LayerType.Software, null);
                webpage.Layout(0, 0, (int)_PDFToHtml.PageWidth, (int)_PDFToHtml.PageHeight);
                webpage.LoadData(_PDFToHtml.HTMLString, "text/html; charset=utf-8", "UTF-8");
                webpage.SetWebViewClient(new WebViewCallBack(_PDFToHtml));
            }
            catch
            {
                _PDFToHtml.Status = PDFEnum.Failed;
            }
        }
    }
}