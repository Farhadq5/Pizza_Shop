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
            // Retrieve input values
            string Firstname = txtfirstname.Text.Trim();
            string Lastname = txtlastname.Text.Trim();
            string Email = txtemail.Text.Trim();
            string Phone = txtphone.Text.Trim();
            string City = txtcity.Text.Trim();
            string Birthdate = dateTimePicker1.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(Firstname))
            {
                MessageBox.Show("First name cannot be empty. Please provide a valid first name.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(Lastname))
            {
                MessageBox.Show("Last name cannot be empty. Please provide a valid last name.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(Email) || !Email.Contains("@"))
            {
                MessageBox.Show("Please provide a valid email address.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Phone.Length != 11 || !Phone.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must be exactly 11 digits long and contain only numbers.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(City))
            {
                MessageBox.Show("City cannot be empty. Please provide a valid city.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(Birthdate, out DateTime parsedBirthdate))
            {
                MessageBox.Show("Invalid birthdate format. Please select a valid date.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Update customer information
                customerService.Updatecustomer(custid, Firstname, Lastname, Email, Phone, City, Birthdate);
                MessageBox.Show($"{Firstname}, your information was successfully updated!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Log exception if necessary
                MessageBox.Show("There was an error updating your information. Please try again later.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
