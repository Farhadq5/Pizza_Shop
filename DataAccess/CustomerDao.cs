using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess
{
    public class CustomerDao : ConnectionToSql
    {
        public void CreateCustomer(int userId, string Firstname, string Lastname, string Email, string Phone, string City, string Birthdate, int loyalty)
        {
            using(var connection = GetConnection()) 
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Customers (user_id, first_name, last_name, email, phone, city, date_of_birth, loyalty_points)"+
                        "VALUES (@userId, @Firstname, @Lastname, @Email, @Phone, @City, @Birthdate, @loyalty)";

                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@Firstname", Firstname);
                    command.Parameters.AddWithValue("@Lastname", Lastname);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@City", City);

                    DateTime birthdate;
                    if (DateTime.TryParse(Birthdate, out birthdate))
                    {
                        command.Parameters.AddWithValue("@Birthdate", birthdate);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid birthdate format.");
                    }

                    command.Parameters.AddWithValue("@loyalty", loyalty);
                    command.ExecuteScalar();
                    connection.Close();
                }
            }
        }

        public customserinfo GetUserByUserid(int userid)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT first_name, last_name, loyalty_points FROM Customers WHERE  user_id = @userid"; // Select specific columns
                    command.Parameters.AddWithValue("@userid", userid);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new customserinfo
                            {
                                customername = reader.GetString(0),
                                customersurname = reader.GetString(1),
                                customerloyaty = reader.GetInt32(2),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null; // Return null if user not found
        }

        
    }
    public class customserinfo
    {
        public string customername { get; set; }
        public string customersurname { get; set; }
        public int customerloyaty { get; set; }
    }
}
