using PdfSharpCore.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NOBLE_SALE.Helper
{
	public class FontResolver : IFontResolver
	{
		public string DefaultFontName => "OpenSans";

		public byte[] GetFont(string faceName)
		{
			using (var ms = new MemoryStream())
			{
				var assembly = typeof(FontResolver).GetTypeInfo().Assembly;
				var resources = assembly.GetManifestResourceNames();
				var resourceName = resources.First(x => x == string.Concat("[NOBLE_SALE].Resources.Fonts.", faceName)); // replace the [ProjectName] with the name of your shared project
				using (var rs = assembly.GetManifestResourceStream(resourceName))
				{
					rs.CopyTo(ms);
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
					return new FontResolverInfo("OpenSans-BoldItalic.ttf");
				}
				else if (isBold)
				{
					return new FontResolverInfo("OpenSans-Bold.ttf");
				}
				else if (isItalic)
				{
					return new FontResolverInfo("OpenSans-Italic.ttf");
				}
				else
				{
					return new FontResolverInfo("OpenSans-Regular.ttf");
				}
			}
			return null;
		}
	}
}

