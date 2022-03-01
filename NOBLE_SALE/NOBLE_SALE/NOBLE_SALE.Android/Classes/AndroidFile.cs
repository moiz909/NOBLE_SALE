using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NOBLE_SALE.Droid.Classes;
using NOBLE_SALE.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


[assembly: Xamarin.Forms.Dependency(typeof(AndroidFile))]
namespace NOBLE_SALE.Droid.Classes
{
    public class AndroidFile : IFile
    {
        public async Task<string> GetLocalPath(string file)
        {
            var status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
            {
                var folder = Path.Combine(
                    Application.Context.GetExternalFilesDir(
                        Android.OS.Environment.DirectoryDocuments).AbsolutePath, "reports");

                Directory.CreateDirectory(folder);
                return Path.Combine(folder, file);
            }

            return string.Empty;
        }
    }
}