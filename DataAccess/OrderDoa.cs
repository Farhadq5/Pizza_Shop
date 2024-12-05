using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess
{
    public class OrderDoa : ConnectionToSql
    {

        public int ordersplaced(int userid)
        {
            int orderid;
            using(var connection = GetConnection()) 
            {
                connection.Open();

                int customerid;
                using (var customercommand = connection.CreateCommand())
                {

                    customercommand.CommandText = @"Select customer_id FROM CUstomers Where user_id = @userid";
                    customercommand.Parameters.AddWithValue("@userid", userid);

                    var result = customercommand.ExecuteScalar();
                    if (result == null)
                    {
                        throw new Exception($"no customner found{userid}"); ;
                    }
                    customerid = (int)result;
                }
                using(var command = connection.CreateCommand()) 
                { 
                    command.CommandText = @"INSERT INTO Orders (customerid,OrderDate,Status) OUTPUT INSERTED.OrderId VALUES (@customerid,GETDATE(),'pending')";
                    command.Parameters.AddWithValue("@customerid", customerid);
                    orderid = (int)command.ExecuteScalar(); //get the order id
                }
            }
            return orderid;
        }

        public void orderitems(int orderid,int pizzaid,int quantity,string pizzaname,string size,decimal subtotal) 
        {
            using(var connection = GetConnection()) 
            {
                connection.Open();
                using (var command = connection.CreateCommand()) 
                {
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO OrderItems (OrderId,PizzaId,Quantity,Status,PizzaName,Size,SubTotal) VALUES (@orderid,@pizzaid,@quantity,'pending',@pizzaname,@size,@subtotal)";
                    command.Parameters.AddWithValue("@orderid", orderid);
                    command.Parameters.AddWithValue("@pizzaid", pizzaid);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@pizzaname", pizzaname);
                    command.Parameters.AddWithValue ("@size", size);
                    command.Parameters.AddWithValue("@subtotal", subtotal);

                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable Showordrs() 
        {
            DataTable Orderdt = new DataTable();

            using(var connection = GetConnection()) 
            {
                connection.Open();
                using (var command = connection.CreateCommand()) 
                {
                    command.Connection = connection;
                    command.CommandText = @" SELECT o.OrderId, o.CustomerId, c.first_name + last_name as Customername,o.OrderDate, o.Status AS OrderStatus,oi.PizzaId," +
                        " oi.PizzaName, oi.Size, oi.Quantity, oi.Status AS ItemStatus, c.city, c.Phone, oi.SubTotal" +
                        " FROM Orders o INNER JOIN OrderItems oi ON o.OrderId = oi.OrderId INNER JOIN Customers c ON o.CustomerId = c.Customer_Id WHERE o.OrderDate >= DATEADD(DAY, -2, CAST(GETDATE() AS DATE)) " +
                        " ORDER BY o.OrderDate DESC;";
                    SqlDataReader datareader = command.ExecuteReader();
                    Orderdt.Load(datareader);
                    datareader.Close();
                    return Orderdt;
                    
                }
            }
        }

       

        public void orderupdate(int userid,int orderid)
        {
            using(var connection = GetConnection()) 
            {
                connection.Open();
                using(var command = connection.CreateCommand()) 
                {
                    command.CommandText = "CompleateOrder";
                    command.CommandType = CommandType.StoredProcedure;

                    //command.CommandText = @" UPDATE Orders SET Status = 'Completed', CompletedBy = @userid, CompletedTime = GETDATE() WHERE OrderId = @orderid AND Status = 'Pending'; 
                    //  --Update the OrderItems table
                    //   UPDATE OrderItems SET Status = 'Completed', CompletedBy = @userid, CompletedTime = GETDATE() WHERE OrderId = @orderId AND Status = 'Pending'";

                    command.Parameters.AddWithValue(@"userid", userid);
                    command.Parameters.AddWithValue(@"orderid", orderid);
                    try
                    {
                        int rowwsaffected = command.ExecuteNonQuery();
                        if (rowwsaffected == 0) 
                        {
                            Console.WriteLine("no rows affected and updated");
                        }
                        else
                        {
                            Console.WriteLine("order hase been compleadet successfully");
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("there has been error while compleating the order please inform the admin or maeneger");
                    }

                }
            }

        }

        public void ordercancel(int userid,int orderid) 
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();
                using( var command = connection.CreateCommand())
                {

                    command.CommandText = @"CancelOrder";
                    command.CommandType= CommandType.StoredProcedure;

                    //command.CommandText = @" UPDATE Orders SET Status = 'Canceled', CompletedBy = @userid, CompletedTime = GETDATE() WHERE OrderId = @orderid AND Status = 'Pending';
                    //  --Update the OrderItems table
                    //   UPDATE OrderItems SET Status = 'Canceled', CompletedBy = @userid, CompletedTime = GETDATE() WHERE OrderId = @orderId AND Status = 'Pending'";

                    command.Parameters.AddWithValue (@"userid", userid);
                    command.Parameters.AddWithValue(@"orderid", orderid);

                    try
                    {
                        int affectedroes = command.ExecuteNonQuery();
                        if(affectedroes == 0)
                        {
                            Console.WriteLine("no rows affected and updated");
                        }
                        else
                            Console.WriteLine("order hase been compleadet successfully");
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("there has been error while compleating the order please inform the admin or maeneger");
                    }
                }
            }
        }
    }
}
