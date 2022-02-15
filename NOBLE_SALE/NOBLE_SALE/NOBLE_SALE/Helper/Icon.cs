using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace NOBLE_SALE.Helper
{
    class Icon : Frame
    {
        private readonly SKCanvasView _canvasView = new SKCanvasView();

        public Icon()
        {
            Padding = new Thickness(0);

            // Thanks to TheMax for pointing out that on mobile, the icon will have a shadow by default.
            // Also it has a white background, which we might not want.
            HasShadow = false;
            BackgroundColor = Color.Transparent;

            Content = _canvasView;
            _canvasView.PaintSurface += CanvasViewOnPaintSurface;
        }

        public static readonly BindableProperty IconFilePathProperty = BindableProperty.Create(
        nameof(ResourceId),
        typeof(string),
        typeof(Icon),
        default(string),
        propertyChanged: RedrawCanvas);

        public string ResourceId
        {
            get => (string)GetValue(IconFilePathProperty);
            set => SetValue(IconFilePathProperty, value);
        }

        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            Icon svgIcon = bindable as Icon;
            svgIcon?._canvasView.InvalidateSurface();
        }

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            if (string.IsNullOrEmpty(ResourceId))
                return;

            using (Stream stream = GetType().Assembly.GetManifestResourceStream(ResourceId))
            {
                SkiaSharp.Extended.Svg.SKSvg svg = new SkiaSharp.Extended.Svg.SKSvg();
                svg.Load(stream);

                SKImageInfo info = args.Info;
                canvas.Translate(info.Width / 2f, info.Height / 2f);

                SKRect bounds = svg.ViewBox;
                float xRatio = info.Width / bounds.Width;
                float yRatio = info.Height / bounds.Height;

                float ratio = Math.Min(xRatio, yRatio);

                canvas.Scale(ratio);
                canvas.Translate(-bounds.MidX, -bounds.MidY);

                canvas.DrawPicture(svg.Picture);
            }
        }
    }
}
