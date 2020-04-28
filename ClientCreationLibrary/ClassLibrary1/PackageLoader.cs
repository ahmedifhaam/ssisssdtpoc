using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dac;

namespace ClientCreation.ssis
{
    public class PackageLoader
    {
        DacPackage DacPackage;
        Package SSISPack;


        public void loadSSISPackage(string path)
        {
            this.SSISPack = new Application().LoadPackage(path, null);
        }

        internal Package getLoadedSSISPack()
        {
            return this.SSISPack;
        }

        public void loadDacPac(string path) 
        {
            this.DacPackage = DacPackage.Load(path);
        }

        public DacPackage getLoadedDacPackage()
        {
            return this.DacPackage;
        }
    }
}
