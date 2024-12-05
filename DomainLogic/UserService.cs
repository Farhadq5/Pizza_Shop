using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable datatable()
        {
            DataTable table = new DataTable();
            table = usersdao.dataTable();
            return table;
        }
        public DataTable disableuserdatatavle() 
        {
            DataTable dt = new DataTable();
            dt = usersdao.disableuserdataTable();
            return dt;
        }

        public int userrole(string username)
        {
            User user = usersdao.GetUserByUsername(username);
            if (user != null)
            {
                return user.RoleId;
            }
            else 
            { 
                return 0; 
            }
            
          
        }

        #region users tablea crude operations

        public void deleteuser(int userid) 
        {
            try
            {
                usersdao.userdelete(userid);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void edituser(int userid,int roleid) 
        {
            usersdao.EditUser(userid, roleid);
        }

        #endregion

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
