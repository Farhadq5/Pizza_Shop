using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Windows.Forms;

namespace DataAccess
{
    public class ReportDoa : ConnectionToSql
    {
        private const string NEWCON = " Server=95.10.198.33,49170;Database=pizzashopDb;User Id=farhad;Password=ff;TrustServerCertificate=True;Connect Timeout=30;Encrypt=False;Max Pool Size=100; ;Initial Catalog=master";

        public DataTable showallreport()
        {
            DataTable allreport = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @" SELECT 
                       o.OrderId,
                       o.OrderDate,
                        o.Status AS OrderStatus,
                       o.CompletedBy AS EmployeeCompletedBy,
                        o.CompletedTime AS OrderCompletionTime,
                         c.first_name AS CustomerFirstName,
                       c.last_name AS CustomerLastName,
                      c.email AS CustomerEmail,
                       c.phone AS CustomerPhone,
                          c.loyalty_points AS CustomerLoyaltyPoints,
                          oi.ItemId AS OrderItemId,
                          oi.PizzaName AS PizzaName,
                          oi.Quantity AS PizzaQuantity,
                            oi.SubTotal AS ItemSubTotal,
                         oi.Status AS ItemStatus,
                        ei.employee_name AS EmployeeName,
                         ei.employee_surname AS EmployeeSurname,
                         ei.phone AS EmployeePhone,
                           ei.email AS EmployeeEmail,
                       ei.hire_date AS EmployeeHireDate,
                          ei.is_active AS EmployeeActiveStatus,
                          p.pizza_name AS PizzaDescription,
                             p.price AS PizzaPrice,
                            ps.size_name AS PizzaSizeDescription,
	                       ps.size_id
                            FROM Orders o
                            JOIN Customers c ON o.CustomerId = c.customer_id
                         JOIN OrderItems oi ON o.OrderId = oi.OrderId
                         JOIN Employees ei ON o.CompletedBy = ei.user_id
                          JOIN Pizza p ON oi.PizzaId = p.pizza_id
                         JOIN PizzaSizes ps on p.pizza_id = ps.size_id
                         WHERE o.Status IN ('pending','Completed','Canceled') 
                           ORDER BY o.OrderDate desc";

                    SqlDataAdapter adaptar = new SqlDataAdapter(command);
                    adaptar.Fill(allreport);
                    return allreport;
                }
            }
        }

        public DataTable cancelreport()
        {
            DataTable datatable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"canceledrepot";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(datatable);
                    return datatable;
                }

            }
        }

        public DataTable employeesales()
        {
            DataTable employeesales = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"employeesales";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(employeesales);
                    return employeesales;
                }
            }
        }

        public DataTable backhistory()
        {
            DataTable bakhistory = new DataTable();
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT backupId,filepath,backupdate,performedby,IsRestore,RestoreDate FROM BackupHistory ORDER BY backupdate DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(bakhistory);
                    return bakhistory;

                }
            }
        }

        public void databasebackup(string path, int adminid)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"BACKUP DATABASE pizzashopDb TO DISK = @path";

                        command.Parameters.AddWithValue(@"path", path);

                        command.ExecuteNonQuery();
                    }
                    using (var historycommand = connection.CreateCommand()) 
                    {
                        historycommand.CommandText = @"INSERT INTO BackupHistory(filepath,performedby) VALUES (@path,@adminid)";

                        historycommand.Parameters.AddWithValue(@"path",path);
                        historycommand.Parameters.AddWithValue(@"adminid",adminid);
                        historycommand.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Backup compleated successfully");
            }
            catch (System.Exception)
            { 
                MessageBox.Show("An error occurred during the backup");
            }
            
        }

        public void loadbackup(string path, int adminid)
        {
            try
            {
                
                using (var connection = new SqlConnection(NEWCON))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @" ALTER DATABASE pizzashopDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                        command.ExecuteNonQuery();
                    }
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"RESTORE DATABASE pizzashopDb FROM DISK = @path WITH REPLACE";
                        command.Parameters.AddWithValue(@"path", path);
                        command.ExecuteNonQuery();
                    }
                    using (var reastorcommand = connection.CreateCommand())
                    {
                        reastorcommand.CommandText = @"ALTER DATABASE pizzashopDb SET MULTI_USER";
                        reastorcommand.ExecuteNonQuery();
                    }
                   
                }
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var historyCommand = connection.CreateCommand())
                    {
                        historyCommand.CommandText = @"UPDATE BackupHistory SET IsRestore = 1, RestoreDate = GETDATE() WHERE filepath = @path AND IsRestore = 0;
                          IF @@ROWCOUNT = 0 BEGIN INSERT INTO BackupHistory (filepath, backupdate, performedby, IsRestore, RestoreDate) VALUES (@path, NULL, @adminid, 1, GETDATE()); END";
                        historyCommand.Parameters.AddWithValue("@path", path);
                        historyCommand.Parameters.AddWithValue("@adminid", adminid);
                        historyCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Database restored successfully");
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("An error occurred during the restore");
            }
           
        }
        
    }
}
