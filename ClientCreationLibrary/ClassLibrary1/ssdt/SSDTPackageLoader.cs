using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClientCreation.ssdt
{
    public class SSDTPackageLoader : ISSDTPackageLoader
    {
        DacPackage pack;

        public void LoadSSDTPackage(string path)
        {
            pack = DacPackage.Load(path);
        }

        public DacPackage GetLoadedSSDTPack()
        {
            if (pack == null) throw new FileNotFoundException("SSDT Package not found");
            return pack;
        }
    }
}
