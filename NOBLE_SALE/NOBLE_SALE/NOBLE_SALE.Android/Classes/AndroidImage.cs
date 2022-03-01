using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using NOBLE_SALE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOBLE_SALE.Droid.Classes
{
    public class AndroidImage : IImage
    {
        public string Prefix { get; set; }
        public ImageSource Implementation { get; set; }
        public bool Extension { get; set; }

        public AndroidImage()
        {
            Implementation = new AndroidImageSource();
            Prefix = string.Empty;
            Extension = false;
        }
    }
}