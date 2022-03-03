using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NOBLE_SALE.Droid.Helper.Dependency;
using NOBLE_SALE.Helper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace NOBLE_SALE.Droid.Helper.Dependency
{
    public class FileHelper : IFileHelper
    {
        public string DocumentFilePath => GetLocalFilePath();

        private string GetLocalFilePath()
        {
            //For dummy file path creation.
            //return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
        }

        public string ResourcesBaseUrl => "file:///android_asset/";
    }
}