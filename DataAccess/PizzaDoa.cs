using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PizzaDoa : ConnectionToSql
    {
        public DataTable GetAllPizzas()
        {
            DataTable pizzatable = new DataTable();
            try
            {
                using(var connection = GetConnection())
                {
                    connection.Open();
                    using( var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT p.pizza_id, p.pizza_name, p.description, p.price, s.size_name AS pizza_size " +
                                               "FROM pizza p INNER JOIN PizzaSizes s ON p.pizza_size = s.size_id";

//                        @"SELECT p.pizza_id, p.pizza_name, p.description, p.price, s.size_name AS pizza_size " +
//"FROM pizza p INNER JOIN PizzaSizes s ON p.pizza_size = s.size_id";


                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(pizzatable);

                        return pizzatable;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
