using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ManageOrdersDoa : ConnectionToSql
    {
        public DataTable Getordersummery()
        {
            DataTable ordersummery = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT o.OrderId, o.CustomerId, c.first_name + ' ' + c.last_name AS CustomerName, o.OrderDate, o.Status AS OrderStatus," +
                    " COUNT(oi.ItemId) AS TotalItems, SUM(oi.SubTotal) AS TotalAmount, e.employee_name + ' ' + e.employee_surname AS CompletedBy, o.CompletedTime FROM Orders o  LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId LEFT JOIN Customers c ON o.CustomerId = c.customer_id LEFT JOIN Employees e ON o.CompletedBy = e.user_id" +
                    " GROUP BY  o.OrderId, o.CustomerId, c.first_name, c.last_name, o.OrderDate, o.Status, e.employee_name, e.employee_surname, o.CompletedTime ORDER BY o.OrderDate DESC;";

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ordersummery);
                    return ordersummery;
                }
            }
        }

       

        public DataTable getorderdetail(int orderid)
        {
            DataTable orderdetailtable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @" SELECT 
                     oi.ItemId,
                     oi.OrderId,
                     oi.PizzaId,
                     oi.PizzaName,
                     oi.Size,
                     oi.Quantity,
                     oi.Status AS ItemStatus,
                     oi.SubTotal,
                     c.City AS DeliveryCity,
                     c.Phone AS CustomerPhone,
                     e.employee_name + ' ' + e.employee_surname AS CompletedBy
                     FROM OrderItems oi 
                     INNER JOIN Orders o ON oi.OrderId = o.OrderId
                     INNER JOIN Customers c ON o.CustomerId = c.Customer_Id
                     LEFT JOIN Employees e ON oi.CompletedBy = e.user_id
                     WHERE oi.OrderId = @orderid
                     ORDER BY oi.ItemId";
                        command.Parameters.AddWithValue(@"orderid", orderid);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(orderdetailtable);
                        return orderdetailtable;
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }

                }
            }
        }

        public DataTable Getrecentorder()
        {
            DataTable recentordertable = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @" SELECT 
                    o.OrderId,
                    o.CustomerId,
                    c.first_name + ' ' + c.last_name AS CustomerName,
                    o.OrderDate,
                    o.Status AS OrderStatus,
                    COUNT(oi.ItemId) AS TotalItems,
                    SUM(oi.SubTotal) AS TotalAmount,
                    e.employee_name + ' ' + e.employee_surname AS CompletedBy,
                    o.CompletedTime
                    FROM Orders o
                   LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId
                    LEFT JOIN Customers c ON o.CustomerId = c.Customer_Id
                    LEFT JOIN Employees e ON o.CompletedBy = e.user_id
                    WHERE o.OrderDate >= DATEADD(DAY, -2, GETDATE())
                  GROUP BY 
                    o.OrderId, 
                    o.CustomerId, 
                    c.First_Name, 
                    c.Last_Name, 
                    o.OrderDate, 
                    o.Status, 
                    e.employee_name, 
                    e.employee_surname, 
                    o.CompletedTime
                  ORDER BY o.OrderDate DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(recentordertable);
                    return recentordertable;
                }
            }
        }

        public DataTable showcustomerdata(int userid)
        {
            DataTable customerOrderdt = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT 
                         oi.OrderId,
                         oi.PizzaName,
                          oi.Size,
                         oi.Quantity,
                         oi.Status AS ItemStatus,
                       oi.SubTotal,
                      c.City AS DeliveryCity,
                       c.Phone AS CustomerPhone,
                        e.employee_name + ' ' + e.employee_surname AS CompletedBy
                       FROM OrderItems oi
                        INNER JOIN Orders o ON oi.OrderId = o.OrderId
                       INNER JOIN Customers c ON o.CustomerId = c.Customer_Id
                     INNER JOIN Users u ON c.User_Id = u.User_Id
                      LEFT JOIN Employees e ON oi.CompletedBy = e.user_id
                        WHERE u.User_Id = @userid 
                        ORDER BY oi.ItemId;";

                    command.Parameters.AddWithValue(@"userid", userid);

                    SqlDataAdapter dataadapter = new SqlDataAdapter(command);
                    dataadapter.Fill(customerOrderdt);

                }
            }
            return customerOrderdt;
        }

        public DataTable showrecentcustomerorder(int userid) 
        {
            DataTable recentcustomerordertable = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT 
                         oi.OrderId,
                         oi.PizzaName,
                          oi.Size,
                         oi.Quantity,
                         oi.Status AS ItemStatus,
                       oi.SubTotal,
                      c.City AS DeliveryCity,
                       c.Phone AS CustomerPhone,
                        e.employee_name + ' ' + e.employee_surname AS CompletedBy
                       FROM OrderItems oi
                        INNER JOIN Orders o ON oi.OrderId = o.OrderId
                       INNER JOIN Customers c ON o.CustomerId = c.Customer_Id
                     INNER JOIN Users u ON c.User_Id = u.User_Id
                      LEFT JOIN Employees e ON oi.CompletedBy = e.user_id
                        WHERE u.User_Id = @userid AND o.OrderDate >= DATEADD(DAY, -2, GETDATE())
                        ORDER BY oi.ItemId";

                    command.Parameters.AddWithValue(@"userid", userid);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(recentcustomerordertable);
                    

                }
            }
            return recentcustomerordertable;
        }
    }
}
