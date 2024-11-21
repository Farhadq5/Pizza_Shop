using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainLogic;
namespace Pizza_Shop
{
    public partial class CustomerInformation : Form
    {
        UserService userService = new UserService();
        

        private int loyalty = 0;
        private int userId;
        private string Username;
        private CustomerService customerService = new CustomerService();
        public CustomerInformation(int userId,string username)
        {
            InitializeComponent();
            this.Username = username;
            this.userId = userId;
        }

        private void CustomerInformation_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
           

        }

        private void Insertcustomer_Click(object sender, EventArgs e)
        {
            string Firstname = txtfirstname.Text.Trim();
            string Lastname = txtlastname.Text.Trim();
            string Email = txtemail.Text.Trim();
            string Phone = txtphone.Text.Trim();
            string City = txtcity.Text.Trim();
            string Birthdate = dateTimePicker1.Text.Trim();

            if(string.IsNullOrEmpty(Firstname)||string.IsNullOrEmpty(Lastname)||string.IsNullOrEmpty(Email)|| string.IsNullOrEmpty(Birthdate)) 
            {
                MessageBox.Show("please fill all customer info");
                return;
            }

            try
            {
                int userrole = userService.userrole(Username);

                customerService.CreateCustomer(userId, Firstname, Lastname, Email, Phone, City, Birthdate, loyalty);
                MessageBox.Show(Firstname + "your information successfuly saved");
                MainMenu mainmenu = new MainMenu(userId,userrole);
                mainmenu.Show();
                this.Hide();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
