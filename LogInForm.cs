using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using DomainLogic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Pizza_Shop
{
    public partial class LogInForm : Form
    {
        private UserService userservice = new UserService();
        private EmployeeService employeeservice = new EmployeeService();
        CustomerService customerservice = new CustomerService();

        public LogInForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = txtusername_login.Text.Trim();
            string password = txtpassword_login.Text.Trim();
            try
            {
                //int userid = userservice.ValidateUser(username, password);
                int userid = userservice.ValidateUser(username,password);
                int role = userservice.userrole(username);
                
                if (userid > 0)
                {
                    if (role == 3)
                    {
                        if (employeeservice.isfirstlogin(userid))
                        {
                            //go to the employee info form
                            EmployeeInfo emp = new EmployeeInfo(userid,role);                          
                            emp.Show();
                            Hide();
                            //send the userid to form too
                        }
                        else
                        {
                            MainMenu mainMenu = new MainMenu(userid, role);
                            mainMenu.Show();
                            Hide();
                        }
                    }
                    else if (role == 4)
                    {
                        MainMenu mainMenu = new MainMenu(userid, role);
                        mainMenu.Show();
                        Hide();
                    }
                    else if (role == 5)
                    {

                        //string firstname = customerservice.GetCustomerInfo(userid);
                        //customerservice.GetCustomerInfo(userid);
                        MainMenu mainMenu = new MainMenu(userid, role);
                        mainMenu.Show();
                        Hide();
                    }
                    else if (role == 1)
                    {
                        if (employeeservice.isfirstlogin(userid))
                        {
                            //go to the employee info form
                            EmployeeInfo emp = new EmployeeInfo(userid, role);
                            emp.Show();
                            Hide();
                            //send the userid to form too
                        }
                        else
                        {
                            MainMenu mainMenu = new MainMenu(userid, role);
                            mainMenu.Show();
                            Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("no user have found please cheack your username and password");
                    }
   
                }
                else if(userid >= 0)
                {
                    MessageBox.Show("you dont have employee acount"+
                       "invalid usrname or password");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGotoCreate_Click(object sender, EventArgs e)
        {
          
            CreateAccountCus createAccountCus = new CreateAccountCus();
           
            createAccountCus.Show();

            this.Hide();
            createAccountCus.FormClosed += (s, args) => this.Close();
        }
    }
}
