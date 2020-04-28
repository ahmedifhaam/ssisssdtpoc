using ClientCreation.ssis;
using Microsoft.SqlServer.Dac;
using System;
using System.Collections.Generic;
using System.Text;
using ClientCreation.connection;

namespace ClientCreation.ssdt
{
    public class SSDTExecutor : ISSDTExecutor
    {
        DacServices dacService;
        DacDeployOptions options;
        EventHandler<string> message;

        public SSDTExecutor(IConnectionStringBuilder connectionStringBuilder)
        {
            dacService = new DacServices(connectionStringBuilder.GetConnectioString());
            options = new DacDeployOptions() { CreateNewDatabase = true, RegisterDataTierApplication = true };
            dacService.Message += statusupdate;
        }


        public void OnStatusUpdate(EventHandler<string> eventHandler)
        {
            this.message += eventHandler;
        }

        public void Publish(ISSDTPackageLoader loader, string databasename)
        {
            dacService.Deploy(loader.GetLoadedSSDTPack(), databasename, true, options: options);
        }

        public DacDeployOptions GetOptions()
        {
            return this.options;
        }

        public void SetDacDeployOptions(DacDeployOptions options)
        {
            this.options = options;
        }

        // This methods hides the dependency of this library 
        private void statusupdate(object sender, DacMessageEventArgs e)
        {
            this.message.Invoke(sender, e.Message.Message);
        }


    }
}
