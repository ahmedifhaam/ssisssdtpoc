using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCreation.connection
{
    public interface IConnectionStringBuilder
    {
        string GetConnectioString();

        IConnectionStringBuilder SetServer(string server);

        IConnectionStringBuilder SetDatabase(string db);

        IConnectionStringBuilder SetPassword(string pass);

        IConnectionStringBuilder SetUserName(string name);
    }
}
