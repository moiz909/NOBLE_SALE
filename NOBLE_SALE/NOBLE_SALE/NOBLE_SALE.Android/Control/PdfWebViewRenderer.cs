using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NOBLE_SALE.Control;
using NOBLE_SALE.Droid.Control;
using NOBLE_SALE.Helper.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

[assembly: ExportRenderer(typeof(PdfWebView), typeof(PdfWebViewRenderer))]
namespace NOBLE_SALE.Droid.Control
{
    public class PdfWebViewRenderer : WebViewRenderer
    {
        private PdfWebView PdfWebView { get { return this.Element as PdfWebView; } }

        private string PdfJsViewerUri => PDFUtils.PdfJsViewerUri;

        public PdfWebViewRenderer(Android.Content.Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.Settings.AllowFileAccess = true;
                Control.Settings.AllowFileAccessFromFileURLs = true;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.Settings.UseWideViewPort = true;
                Control.Settings.LoadWithOverviewMode = true;
                this.UpdateDisplayZoomControls();
                this.UpdateEnableZoomControls();
                this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                this.LoadPdfFile(this.PdfWebView.Uri);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == PdfWebView.UriProperty.PropertyName)
                this.LoadPdfFile(this.PdfWebView.Uri);
        }

        void UpdateEnableZoomControls()
        {
            // BuiltInZoomControls supported as of API level 3
            if (Control != null && ((int)Build.VERSION.SdkInt >= 3))
            {
                var value = Element.OnThisPlatform().ZoomControlsEnabled();
                Control.Settings.SetSupportZoom(value);
                Control.Settings.BuiltInZoomControls = value;
            }
        }

        void UpdateDisplayZoomControls()
        {
            // DisplayZoomControls supported as of API level 11
            if (Control != null && ((int)Build.VERSION.SdkInt >= 11))
            {
                var value = Element.OnThisPlatform().ZoomControlsDisplayed();
                Control.Settings.DisplayZoomControls = value;
            }
        }

        void LoadPdfFile(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return;

            string url = $"?file={WebUtility.UrlEncode(uri)}";
            Control.LoadUrl(PdfJsViewerUri + url);
        }
    }
}