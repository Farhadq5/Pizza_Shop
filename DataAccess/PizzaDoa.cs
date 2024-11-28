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

        public void Pizzaadd(string pizzaname,string desciption,decimal price,int pizzasize)
        {
            try
            {


                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"INSERT INTO Pizza VALUES ( @pizzaname, @desciption, @price, @pizzasize)";

                        command.Parameters.AddWithValue(@"pizzaname", pizzaname);
                        command.Parameters.AddWithValue(@"desciption", desciption);
                        command.Parameters.AddWithValue(@"price", price);
                        command.Parameters.AddWithValue(@"pizzasize", pizzasize);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deletepizza(int pizzaid)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"DELETE Pizza WHERE pizza_id = @pizzaid";
                        command.Parameters.AddWithValue(@"pizzaid", pizzaid);
                        command.ExecuteNonQuery ();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void updatepizza(int pizzaid, string pizzaname, string description, decimal pizzaprice,int pizzasize)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"UPDATE Pizza SET pizza_name = @pizzaname, description = @description, price = @pizzaprice,pizza_size = @pizzasize " +
                            "WHERE pizza_id = @pizzaid";
                        command.Parameters.AddWithValue(@"pizzaid", pizzaid);
                        command.Parameters.AddWithValue(@"pizzaname", pizzaname);
                        command.Parameters.AddWithValue("description", description);
                        command.Parameters.AddWithValue(@"pizzaprice", pizzaprice);
                        command.Parameters.AddWithValue(@"pizzasize", pizzasize);

                        command.ExecuteNonQuery();
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
