using DomainLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Pizza_Shop
{
    public partial class sethings : Form
    {
        CustomerService customerService = new CustomerService();
        UserService userService = new UserService();
        int custid = 0;
        public sethings(int id)
        {
            InitializeComponent();
            custid = id;     
        }

        private void sethings_Load(object sender, EventArgs e)
        {
        }

        private void btnupdatecustomer_Click(object sender, EventArgs e)
        {
            string Firstname = txtfirstname.Text.Trim();
            string Lastname = txtlastname.Text.Trim();
            string Email = txtemail.Text.Trim();
            string Phone = txtphone.Text.Trim();
            string City = txtcity.Text.Trim();
            string Birthdate = dateTimePicker1.Text.Trim();

            if (string.IsNullOrEmpty(Firstname) || string.IsNullOrEmpty(Lastname) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Birthdate))
            {
                MessageBox.Show("please fill all customer info");
                return;
            }
            try
            {
                customerService.Updatecustomer(custid,Firstname, Lastname, Email, Phone, City, Birthdate);
                MessageBox.Show(Firstname + "your information successfuly saved");
            }
            catch (Exception)
            {

                MessageBox.Show("there is a error on update please try again later");
            }
        }

        private void btndeleteacount_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult reasult = MessageBox.Show("Are you sure you want to delete your accounr?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                   MessageBoxDefaultButton.Button2);
                if (reasult == DialogResult.Yes)
                {
                    customerService.deletecustomer(custid);
                    MessageBox.Show("Your account have been succesfully deleted ");
                    LogInForm login = new LogInForm();
                    login.Show();
                    this.Close();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("THere was an error while seleting your accounbt please try again later");
            }
        }
    }
}
