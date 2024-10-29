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
    public partial class ManageEmployees : Form
    {
        public int userid { get; set; }
        UserService userService;
        private string id = "";
        private bool Edit;
        
        public ManageEmployees()
        {
            userService = new UserService();
            InitializeComponent();
        }

        private void ManageEmployees_Load(object sender, EventArgs e)
        {
            hidetextbox();
           
        }
        private void hidetextbox()
        {
            txtupmail.Visible = false;
            txtupfirstname.Visible = false;
            txtuplastname.Visible = false;
            txtuppass.Visible = false;
            txtupposition.Visible = false;
            txtupusername.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            iconbtnsave.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int roleid = 0;
                string username = txtusername.Text.Trim();
                string password = txtpass.Text.Trim();
                if (comboBox2.Text == "manneger")
                {
                    roleid = 1;
                }
                else if (comboBox2.Text == "Delevery worker")
                {
                    roleid = 3;
                }
                int userid2 = 4;

                userService.adminadduser(username, password, roleid, userid2);
                cleartextboxes();
            }
            catch (Exception)
            {

                throw;
            }        
        }

        private void cleartextboxes()
        {
            txtusername.Clear();
            txtpass.Clear();
            comboBox2.Items.Clear();
        }
    }
}
