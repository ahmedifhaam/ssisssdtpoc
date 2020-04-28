using Microsoft.SqlServer.Dac;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCreation.ssdt
{
    public interface ISSDTExecutor
    {
        void Publish(ISSDTPackageLoader loader, string databasename);
        void OnStatusUpdate(EventHandler<string> eventHandler);

        DacDeployOptions GetOptions();
        void SetDacDeployOptions(DacDeployOptions options);
    }
}
