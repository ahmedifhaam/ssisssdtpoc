using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCreation.ssis
{
    public class SSISExecutor
    {
        Package package;
        public SSISExecutor(PackageLoader packageLoader)
        {
            this.package = packageLoader.getLoadedSSISPack();
            foreach(var param in this.package.Parameters)
            {
                Console.WriteLine(param.ToString());
            }
        }

        public void OverrideVariables(string key,string val)
        {
            this.package.Variables[key].Value = val;
        }

        public void OverrideParameters(string key,string val)
        {
            this.package.Parameters[key].Value = val;
        }

        public void SetDatabase(string value)
        {
            //this.package.Connections["CC"].Properties["InitialCatalog"].SetValue(this.package.Connections["CC"], value);
            this.package.Variables["db"].Value = value;
        }

        public void SetServer(string value)
        {
            //this.package.Connections["CC"].Properties["ServerName"].SetValue(this.package.Connections["CC"], value);
            this.package.Variables["Server"].Value = value;
        }

        public void SetUser(string value)
        {
            //this.package.Connections["CC"].Properties["UserName"].SetValue(this.package.Connections["CC"], value);
            this.package.Variables["Username"].Value = value;
        }

        public void SetPassword(string value)
        {
            //this.package.Connections["CC"].Properties["Password"].SetValue(this.package.Connections["CC"], value);
            this.package.Variables["Password"].Value = value;
        }
                

        public string execute()
        {
            
            Console.WriteLine(package.Connections["CC"].ConnectionString);
            DTSExecResult results = package.Execute();
            
            //Check the results for Failure and Success
            if (results == DTSExecResult.Failure)
            {
                string err = "";
                foreach (DtsError local_DtsError in package.Errors)
                {
                    string error = local_DtsError.Description.ToString();
                    err += error;
                }
                return err;
            }
            if (results == Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Success)
            {
                return "Package Executed Successfully....";
            }

            return "Unknown Failure";

        }
    }
}
