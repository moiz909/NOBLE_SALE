using System;
using System.Collections.Generic;
using System.Text;

namespace NOBLE_SALE.Helper.Interface
{
    public interface IFileHelper
    {
        string DocumentFilePath { get; }

        string ResourcesBaseUrl { get; }
    }
}
