using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxosApp1.Abstractions
{
    public interface IAppInfoService
    {
         Task<string> GetBundleID();
    }
}