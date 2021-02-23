using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DBConfig
    {
        public const string CONNECTION_STRING = @"Server=tcp:odontotech-sql-srv.database.windows.net,1433;Initial Catalog=odontotech-sql-db;Persist Security Info=False;User ID=Administrador;Password=#Odontotech;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlCommand Cmd;
        public static SqlDataReader Reader;
    }
}
