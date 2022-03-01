using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NOBLE_SALE.Interfaces
{
    public interface IFile
    {
        Task<string> GetLocalPath(string archivo);
    }
}
