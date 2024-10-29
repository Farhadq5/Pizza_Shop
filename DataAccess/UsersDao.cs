using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DataAccess
{
    public class UsersDao : ConnectionToSql
    {
        public int CreateUser(string username, string password)
        {
            const int roleId = 5;
            int userId = 0;
            if (UsernameExists(username))
            {
                throw new InvalidOperationException("Username already exists. Please try a new username.");
            }
            else
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        // Insert the user and retrieve the generated user_id using OUTPUT INSERTED
                        command.CommandText = "INSERT INTO Users (username, password_hash, role_id) " +
                                              "OUTPUT INSERTED.user_id " +
                                              "VALUES (@username, @password, @roleId)";


                        // Add parameters for the SQL query
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);  // Store the hashed password
                        command.Parameters.AddWithValue("@roleId", roleId);

                        // Execute the SQL command and get the generated user_id
                        userId = (int)command.ExecuteScalar();
                    }
                    connection.Close();
                }
                

                // Return the generated user_id
                return userId;
            }
        }
        public int CreateUser(string username, string password, int roleId, int creator)
        {

            int userId = 0;
            if (UsernameExists(username))
            {
                throw new InvalidOperationException("Username already exists. Please try a new username.");
            }
            else
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        // Insert the user and retrieve the generated user_id using OUTPUT INSERTED
                        command.CommandText = "INSERT INTO Users (username, password_hash, role_id, created_by) " +
                                              "OUTPUT INSERTED.user_id " +
                                              "VALUES (@username, @password, @roleId, @creator)";


                        // Add parameters for the SQL query
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);  // Store the hashed password
                        command.Parameters.AddWithValue("@roleId", roleId);
                        command.Parameters.AddWithValue("@creator", creator);

                        // Execute the SQL command and get the generated user_id
                        userId = (int)command.ExecuteScalar();
                    }
                    connection.Close();
                }

                // Return the generated user_id
                return userId;
            }
        }


        private bool UsernameExists(string username)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM Users WHERE username = @username"; // Fixed the case
                    command.Parameters.AddWithValue("@username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }


        public User GetUserByUsername(string username)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT user_id, username, password_hash, role_id FROM Users WHERE username = @username"; // Select specific columns
                    command.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                RoleId = reader.GetInt32(3)
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null; // Return null if user not found
        }

    }


    // User class for holding user data
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}

