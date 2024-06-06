using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxosApp1.Abstractions
{
    public class AppInfoService : IAppInfoService
    {
        public async Task<string> GetBundleID()
        {
            await Task.Delay(1000);
            return AppInfo.PackageName;
        }
    }
}