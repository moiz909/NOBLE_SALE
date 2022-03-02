using NOBLE_SALE.PDFReports;
using PdfSharpCore.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace NOBLE_SALE.Helper
{
    public class FileFontResolver : IFontResolver // FontResolverBase
    {


        public string DefaultFontName => "OpenSans";

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                using (var fs = File.Open(faceName, FileMode.Open))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("OpenSans", StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo("Resources/Fonts/OpenSans-BoldItalic.ttf");
                }
                else if (isBold)
                {
                    return new FontResolverInfo("Resources/Fonts/OpenSans-Bold.ttf");
                }
                else if (isItalic)
                {
                    return new FontResolverInfo("Resources/Fonts/OpenSans-Italic.ttf");
                }
                else
                {
                    return new FontResolverInfo("Resources/Fonts/OpenSans-Regular.ttf");
                }
            }
            return null;
        }
    }
}
