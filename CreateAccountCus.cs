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
    public partial class CreateAccountCus : Form
    {
        private UserService userService = new UserService(); 
        public CreateAccountCus()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string Username = txtusernameCreate.Text.Trim();
            string Password = txtpasswordCreate.Text.Trim();
            string ConfirmPassword = txtconfiermpasswordCreate.Text.Trim();
            try
            {
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("password dosenot match");
                    return;
                }
                
                    int userId = userService.RegisterUser(Username, Password);
                
                if(userId > 0) 
                {
                    MessageBox.Show("usser created succssefully");

                    CustomerInformation customerInformation = new CustomerInformation(userId,Username);
                    customerInformation.Show();
                    this.Hide();
                    customerInformation.FormClosed += (s, args) => this.Close();

                }

            }
            catch (Exception)
            {

                throw;
            }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateAccountCus_Load(object sender, EventArgs e)
        {

        }
    }
}
