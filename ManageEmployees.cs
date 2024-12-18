using DomainLogic;
using System;
using System.Windows.Forms;

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
                // Retrieve and validate username and password
                string username = txtusername.Text.Trim();
                string password = txtpass.Text.Trim();

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Username cannot be empty. Please enter a valid username.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Password cannot be empty. Please enter a valid password.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Determine role ID based on selection
                int roleid = 0;
                if (comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a role from the dropdown.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (comboBox2.SelectedItem.ToString() == "Manager")
                {
                    roleid = 1;
                }
                else if (comboBox2.SelectedItem.ToString() == "Delivery Worker")
                {
                    roleid = 3;
                }
                else
                {
                    MessageBox.Show("Invalid role selected. Please choose a valid role.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hardcoded admin ID for now
                int adminId = 4;

                // Call the user creation method
                int result = userService.adminadduser(username, password, roleid, adminId);

                // Handle the result
                if (result > 0)
                {
                    MessageBox.Show("User added successfully!",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartextboxes();
                    datatable();
                }
                else if (result == -1)
                {
                    MessageBox.Show("Username already exists. Please try a different username.",
                                    "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Failed to add user. Please try again.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (Edit == true)
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
                        default:
                            combrole = 3;
                            break;
                    }

                    userService.edituser(Convert.ToInt32(id), combrole);
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
            int userid = Convert.ToInt32(id);
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
            if (btnDisableUser.Text == "Disable Employees")
            {
                dataGridView3.DataSource = userService.disableuserdatatavle();
                btnDisableUser.Text = "Active Employees";
            }
            else if (btnDisableUser.Text == "Active Employees")
            {
                dataGridView3.DataSource = userService.datatable();
                btnDisableUser.Text = "Disable Employees";
            }


        }
    }
}
