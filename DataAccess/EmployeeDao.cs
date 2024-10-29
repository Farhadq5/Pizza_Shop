using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeDao:ConnectionToSql
    {
        // Check if employee info exists for the given userId
        public bool EmployeeExist(int userid)
        {
            using(var connection = GetConnection()) 
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM Employees WHERE user_id = @userId";
                    command.Parameters.AddWithValue("@userid", userid);
                    return (int)command.ExecuteScalar()>0;
                }
            }
        }

        //insert to the employee table
        public void InsertEmployetable(int userid, string empname, string empsurname, string Phone, string Email, int roleid)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand()) 
                {
                    command.Connection = connection;
                    command.CommandText= "INSERT INTO Employees (user_id, employee_name, employee_surname, phone, email,role_id) " +
                                      "VALUES (@userid, @empname, @empsurname, @Phone, @Email, @roleid)";

                    command.Parameters.AddWithValue ("userid", userid);
                    command.Parameters.AddWithValue("@empname", empname);
                    command.Parameters.AddWithValue("@empsurname", empsurname);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@roleid", roleid);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                
            }
        }

        public employeeinfo empinfo(int userid)
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand()) 
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT employee_name, employee_surname FROM Employees WHERE user_id = @userid";

                    command.Parameters.AddWithValue("@userid", userid);
                    using(SqlDataReader reader = command.ExecuteReader()) 
                    {
                        while (reader.Read()) 
                        {
                            return new employeeinfo
                            {
                                firstname = reader.GetString(0),
                                lastname = reader.GetString(1),
                            };
                        }
                    }
                    connection.Close();
                }
                return null;
            }
        }
    }

    public class employeeinfo
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
    
}
