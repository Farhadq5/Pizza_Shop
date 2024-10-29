using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DomainLogic
{
    public class CustomerService
    {
        private CustomerDao customerdao = new CustomerDao();
        

        public void CreateCustomer(int userId,string Firstname, string Lastname, string Email, string Phone, string City, string Birtdate, int loyalty)
        {
            customerdao.CreateCustomer(userId ,Firstname, Lastname, Email, Phone, City, Birtdate, loyalty);
        }

        public string[] GetCustomerInfo(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            // Use your data access class to retrieve customer information
            customserinfo info = customerdao.GetUserByUserid(userId);

            if (info == null)
            {
                throw new Exception("Customer not found.");
            }

            // Create a formatted string with the customer's details
            string firstname = info.customername;
            string lastname = info.customersurname;
            string loyaltyPoints = info.customerloyaty.ToString();
            string[] custinfo = { firstname, lastname, loyaltyPoints };
            
            return custinfo;
        }
    }
}
