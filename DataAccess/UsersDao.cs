using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess
{
    public class UsersDao : ConnectionToSql
    {

        #region user creation admin and customer
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
                    }


                    // Return the generated user_id
                    return userId;
                }         
        }

        public int CreateUser(string username, string password, int roleId, int creator)
        {
            int userId = 0;

            // Check if the username already exists
            if (UsernameExists(username))
            {
                return -1;
            }
            else
            {

                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        command.CommandText = "INSERT INTO Users (username, password_hash, role_id, created_by) " +
                                              "OUTPUT INSERTED.user_id " +
                                              "VALUES (@username, @password, @roleId, @creator)";

                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@roleId", roleId);
                        command.Parameters.AddWithValue("@creator", creator);

                        userId = (int)command.ExecuteScalar();
                    }
                }

                // Return the generated user_id
                return userId;
            }
        }

        #endregion

        #region data table here shows the users to the admin data grid view
        public DataTable dataTable()
        {
            DataTable table = new DataTable();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT user_id, employee_name, employee_surname, role_name,hire_date, is_active " +
                      "FROM Employees JOIN Roles ON Employees.role_id = Roles.role_id " +
                      "WHERE Employees.role_id <> 5 AND is_active <> 0";
                        SqlDataReader reader = command.ExecuteReader();

                        table.Load(reader);
                        reader.Close();

                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public DataTable disableuserdataTable()
        {
            DataTable table = new DataTable();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT user_id, employee_name, employee_surname, role_name,hire_date, is_active " +
                      "FROM Employees JOIN Roles ON Employees.role_id = Roles.role_id " +
                      "WHERE Employees.role_id <> 5 AND is_active <> 1";
                        SqlDataReader reader = command.ExecuteReader();

                        table.Load(reader);
                        reader.Close();

                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        #endregion

        //checking if the ueser already exists
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

        //here ve get the user cridentials with the username 
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
            }
            return null; // Return null if user not found
        }



        #region crude operations is going to be here

        public void EditUser(int userid , int roleid) 
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Start a transaction to ensure both updates happen atomically
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update role in Users table
                        using (var command = new SqlCommand("UPDATE Users SET role_id = @roleId WHERE user_id = @userId", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@roleId", roleid);
                            command.Parameters.AddWithValue("@userId", userid);
                            command.ExecuteNonQuery();
                        }

                        // Update role in Employees table
                        using (var command = new SqlCommand("UPDATE Employees SET role_id = @roleId WHERE user_id = @userId", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@roleId", roleid);
                            command.Parameters.AddWithValue("@userId", userid);
                            command.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch
                    {
                        // Rollback if something goes wrong
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public void userdelete(int userId) 
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using(var command = new SqlCommand()) 
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE Users SET is_active = 0 WHERE user_id = @userId";
                    command.Parameters.AddWithValue("@userid", userId);
                    //command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

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

