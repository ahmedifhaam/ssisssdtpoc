using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ClientCreation.connection
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        const string TEMPLATE = "server = (local); user id =;password=;initial catalog=";
        SqlConnectionStringBuilder builder;
        public ConnectionStringBuilder()
        {
            builder = new SqlConnectionStringBuilder(TEMPLATE);
        }
        public IConnectionStringBuilder SetServer(string server)
        {
            builder.DataSource = server;
            return this;
        }

        public IConnectionStringBuilder SetDatabase(string db)
        {
            builder.InitialCatalog = db;
            return this;
        }

        public IConnectionStringBuilder SetPassword(string pass)
        {
            builder.Password = pass;
            return this;
        }

        public IConnectionStringBuilder SetUserName(string name)
        {
            builder.UserID = name;
            return this;
        }


        public string GetConnectioString()
        {
            return this.builder.ConnectionString;
        }
    }
}
