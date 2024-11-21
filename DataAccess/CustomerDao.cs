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
        public void CreateCustomer(int userId, string firstName, string lastName, string email, string phone, string city, string birthDate, int loyaltyPoints)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                    INSERT INTO Customers (user_id, first_name, last_name, email, phone, city, date_of_birth, loyalty_points)
                    VALUES (@UserId, @FirstName, @LastName, @Email, @Phone, @City, @BirthDate, @LoyaltyPoints)";

                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@FirstName", firstName ?? throw new ArgumentNullException(nameof(firstName)));
                        command.Parameters.AddWithValue("@LastName", lastName ?? throw new ArgumentNullException(nameof(lastName)));
                        command.Parameters.AddWithValue("@Email", email ?? throw new ArgumentNullException(nameof(email)));
                        command.Parameters.AddWithValue("@Phone", phone ?? throw new ArgumentNullException(nameof(phone)));
                        command.Parameters.AddWithValue("@City", city ?? throw new ArgumentNullException(nameof(city)));

                        if (DateTime.TryParse(birthDate, out DateTime parsedBirthDate))
                        {
                            command.Parameters.AddWithValue("@BirthDate", parsedBirthDate);
                        }
                        else
                        {
                            throw new ArgumentException("Invalid birth date format. Please provide a valid date.", nameof(birthDate));
                        }

                        command.Parameters.AddWithValue("@LoyaltyPoints", loyaltyPoints);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (log4net, Serilog, etc., can be used for real-world applications)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throwing the exception for further handling if needed
            }
        }

        public void UpdateCustomer(int userId, string firstName, string lastName, string email, string phone, string city, string birthDate)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand()) 
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE Customers " +
                            "Set first_name = @firstName, last_name = @lastName, email = @email, phone = @phone, city = @city, date_of_birth = @birthDate " +
                            "Where user_id = @USerId";

                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@FirstName", firstName ?? throw new ArgumentNullException(nameof(firstName)));
                        command.Parameters.AddWithValue("@LastName", lastName ?? throw new ArgumentNullException(nameof(lastName)));
                        command.Parameters.AddWithValue("@Email", email ?? throw new ArgumentNullException(nameof(email)));
                        command.Parameters.AddWithValue("@Phone", phone ?? throw new ArgumentNullException(nameof(phone)));
                        command.Parameters.AddWithValue("@City", city ?? throw new ArgumentNullException(nameof(city)));

                        if (DateTime.TryParse(birthDate, out DateTime parsedBirthDate))
                        {
                            command.Parameters.AddWithValue("@BirthDate", parsedBirthDate);
                        }
                        else
                        {
                            throw new ArgumentException("Invalid birth date format. Please provide a valid date.", nameof(birthDate));
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
         

        public void deleteuser(int userId) 
        {
            using(var connection = GetConnection()) 
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Customers " +
                        "Where user_id = @userId";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
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
