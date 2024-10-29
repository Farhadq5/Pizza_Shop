using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class ConnectionToSql
    {
       private readonly string ConnectionString;
        public ConnectionToSql()
        {
            ConnectionString = "Server=88.243.252.9,49170;Database=pizzashopDb;User Id=farhad;Password=ff;ConnectRetryCount=10;";

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
