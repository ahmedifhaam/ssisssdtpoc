using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinetCreation.ssdt
{
    public class AuthorizationUpdater
    {
        string ConnectionString;
        string User;
        string Database;

        public AuthorizationUpdater(string ConnectionString,string loginname,string database)
        {
            this.ConnectionString = ConnectionString;
            this.User = loginname;
            this.Database = database;
        }

        public void updateAuthorization()
        {

            string query = String.Format("ALTER AUTHORIZATION ON DATABASE::{0} TO {1}", this.Database, this.User);
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                
                
            }
        }

        //string queryString = "SELECT tPatCulIntPatIDPk, tPatSFirstname, tPatSName, tPatDBirthday  FROM  [dbo].[TPatientRaw] WHERE tPatSName = @tPatSName";
        //string connectionString = "Server=.\PDATA_SQLEXPRESS;Database=;User Id=sa;Password=2BeChanged!;";


    }
}
