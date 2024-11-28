using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ReportDoa : ConnectionToSql
    {
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
    }
}
