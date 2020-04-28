using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCreation.ssdt
{
    public interface ISSDTPackageLoader
    {
        void LoadSSDTPackage(string path);
        DacPackage GetLoadedSSDTPack();
    }
}
