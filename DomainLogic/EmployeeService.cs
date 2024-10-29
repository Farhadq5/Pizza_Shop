using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.Win32;

namespace DomainLogic
{
    public class EmployeeService
    {
        private EmployeeDao employeedao = new EmployeeDao();

        // check if the user has info
        public bool isfirstlogin(int userid)
        {
            return !employeedao.EmployeeExist(userid);
        }

        // methode to insert info to employee table
        public void firsttimelodin(int userid, string empname, string empsurname, string Phone, string Email, int roleid)
        {
           if (!employeedao.EmployeeExist(userid)) 
           {
                employeedao.InsertEmployetable(userid, empname, empsurname, Phone, Email, roleid);
           } 
           else
            {
                throw new InvalidOperationException();
            }
        }

        public string[] employeedata(int userid) 
        {
            employeeinfo empinfo = employeedao.empinfo(userid);

            string[] empname_surname = {empinfo.firstname,empinfo.lastname};
            return empname_surname;
        }
    }
}
