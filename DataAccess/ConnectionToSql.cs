﻿using System;
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
            ConnectionString = "Server=88.244.65.29,49170;Database=pizzashopDb;User Id=farhad;Password=ff;TrustServerCertificate=True;Connect Timeout=30;Encrypt=False;Max Pool Size=100;";

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
