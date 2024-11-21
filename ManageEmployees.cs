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
        private string id = "";
        UserService userService;
        private bool Edit;
        
        public ManageEmployees()
        {
            userService = new UserService();
            InitializeComponent();
        }

        private void ManageEmployees_Load(object sender, EventArgs e)
        {
            hidetextbox();
            datatable();
        }
        private void hidetextbox()
        {
            
            label13.Visible = false;
            iconbtnsave.Visible = false;
            comboBox1.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datatable()
        {

            dataGridView2.DataSource = userService.datatable();
            dataGridView3.DataSource = userService.datatable();

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
                datatable();
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

        private void iconbtnsave_Click(object sender, EventArgs e)
        {
            if(Edit == true) 
            {
                try
                {
                    int combrole = 0;
                    string rolename = dataGridView3.CurrentRow.Cells["role_name"].Value.ToString();
                    comboBox1.SelectedItem = rolename;
                    switch (comboBox1.SelectedItem)
                    {
                        case "Delevery worker":
                            combrole = 3;
                            break;
                        case "manneger":
                            combrole = 1;
                            break;
                        default: combrole = 3; 
                            break;
                    }

                    userService.edituser(Convert.ToInt32(id),combrole);
                    datatable();
                    hidetextbox();
                   Edit = false;
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }

        private void iconbtnupdate_Click(object sender, EventArgs e)
        {
            id = dataGridView3.CurrentRow.Cells["user_id"].Value.ToString();
            int userid =Convert.ToInt32(id);
            if (userid < 0)
            {

                label13.Visible = false;
                comboBox1.Visible = false;

            }
            else
            {

                label13.Visible = true;
                iconbtnsave.Visible = true;
                comboBox1.Visible = true;

                iconbtnsave.Visible = true;
                Edit = true;
                datatable();
            }
           
        }

        private void iconbtndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult reasult = MessageBox.Show("Are you sure you want to delete user?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (reasult == DialogResult.Yes)
                {
                    int selectedindex = dataGridView3.CurrentCell.RowIndex;

                    dataGridView2.DataSource = userService.datatable();
                    dataGridView3.DataSource = userService.datatable();
                    id = dataGridView3.CurrentRow.Cells["user_id"].Value.ToString();
                    userService.deleteuser(Convert.ToInt32(id));
                    dataGridView3.Rows.RemoveAt(selectedindex);
                    datatable();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("User not deleted. Please try again.");
            }
        }

        private void btnDisableUser_Click(object sender, EventArgs e)
        {
            if(btnDisableUser.Text == "Disable Employees") 
            {
                dataGridView3.DataSource = userService.disableuserdatatavle();
                btnDisableUser.Text = "Active Employees";
            }
            else if(btnDisableUser.Text =="Active Employees")
            {
                dataGridView3.DataSource = userService.datatable();
                btnDisableUser.Text = "Disable Employees";
            }

            
        }
    }
}
