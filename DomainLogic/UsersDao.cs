using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
    internal class UsersDao : ConnectionToSql
    {
        public void CreateUser(string username, string password, int roleId)
        {
            if (UsernameExists(username))
            {
                throw new InvalidOperationException("Username already exists. Please try a new username.");
            }
            else
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // Hash the password
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO Users (username, password_hash, role_id) VALUES (@username, @password, @roleId)";

                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", hashedPassword); // Store the hashed password
                        command.Parameters.AddWithValue("@roleId", roleId);
                        command.ExecuteNonQuery();
                    }
                }
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
                                PasswordHash = reader.GetString(2),
                                RoleId = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return null; // Return null if user not found
        }
    }

    // User class for holding user data
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
    }
}
