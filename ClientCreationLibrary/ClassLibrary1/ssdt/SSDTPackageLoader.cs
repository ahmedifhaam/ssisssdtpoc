using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCreation.ssdt
{
    public class SSDTPackageLoader : ISSISPackageLoader
    {
        Package SSISPack;
        public void LoadSSISPackage(string path)
        {
            this.SSISPack = new Application().LoadPackage(path, null);
        }


        public Package GetLoadedSSISPack()
        {
            return this.SSISPack;
        }
    }
}
