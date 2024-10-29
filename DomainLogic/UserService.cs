using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DataAccess;

namespace DomainLogic
{
    public class UserService
    {
        private UsersDao usersdao = new UsersDao();
        private CustomerDao customerdao = new CustomerDao();

        public int ValidateUser(string username, string password)
        {
            // Get the user details from the database by username
            User user = usersdao.GetUserByUsername(username);

            if (user != null)
            {
                // Verify the password using BCrypt or another hashing algorithm
                if (password == user.Password)
                {
                    return user.UserId;  // Return the full User object, including UserId and RoleId
                }
            }

            return 0;  // Return null if credentials are invalid
        }

        public int userrole(string username)
        {
            User user = usersdao.GetUserByUsername(username);
            return user.RoleId;
        }

        public int adminadduser(string username, string password, int roleid, int creator)
        {
            return usersdao.CreateUser(username, password, roleid, creator);
        }

        public int RegisterUser (string username, string password)
        {

          return usersdao.CreateUser(username, password);
            //string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);   
        }
    }
}
