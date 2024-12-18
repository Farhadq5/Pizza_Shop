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

            // Check if required fields are filled
            if (string.IsNullOrEmpty(Firstname) || string.IsNullOrEmpty(Lastname) ||
                string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Birthdate))
            {
                MessageBox.Show("Please fill all customer info", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate phone number
            if (Phone.Length < 11 || !Phone.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must be at least 11 digits and contain only numbers.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate email format
            if (!Email.Contains("@") || !Email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Assuming Username and userId are properly set before this call
                int userrole = userService.userrole(Username);

                // Create customer
                customerService.CreateCustomer(userId, Firstname, Lastname, Email, Phone, City, Birthdate, loyalty);

                // Success message and redirect to Main Menu
                MessageBox.Show($"{Firstname}, your information has been successfully saved.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainMenu mainmenu = new MainMenu(userId, userrole);
                mainmenu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the customer information: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
