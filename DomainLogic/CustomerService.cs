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

        public void Updatecustomer(int userId,string Firstname ,string Lastname, string Email,string phone,string city,string Birtdate)
        {
            customerdao.UpdateCustomer(userId, Firstname, Lastname, Email, phone, city, Birtdate);
        }

        public void deletecustomer(int userid)
        {
            customerdao.deleteuser(userid);
        }

        public string[] GetCustomerInfo(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            // Use your data access class to retrieve customer information
            customserinfo info = customerdao.GetUserByUserid(userId);
            try
            {
                if (info == null)
                {
                    throw new Exception("Customer not found.");
                }
            }
            catch (Exception)
            {
                throw; 
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
