using DomainLogic;
using System;
using System.Windows.Forms;

namespace Pizza_Shop
{
    public partial class ManagePizzaMenu : Form
    {
        private readonly PizzaService pizzaservice = new PizzaService();
        public ManagePizzaMenu()
        {
            InitializeComponent();
            showdata();
        }

        private void ManagePizzaMenu_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            string pizzaname = txtpizzaname.Text.Trim();
            string pizzadesc = txtpizzadesc.Text.Trim();
            decimal pizzaprice;
            int pizzasize = 1 + comboBox2.SelectedIndex;
            try
            {
                if (!decimal.TryParse(txtpizzaprice.Text, out pizzaprice) || pizzaprice < 0)
                {
                    MessageBox.Show("Please inter a valid positive number");

                }
                else
                {
                    pizzaservice.pizzaadd(pizzaname, pizzadesc, pizzaprice, pizzasize);
                    MessageBox.Show("Pizza saved successfully");
                    showdata();
                    cleartextbox();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void showdata()
        {
            dataGridView2.DataSource = pizzaservice.GetPizza();
            dataGridView3.DataSource = pizzaservice.GetPizza();
        }

        private void cleartextbox()
        {
            txtpizzaname.Clear();
            txtpizzadesc.Clear();
            txtpizzaprice.Clear();
        }
        private void iconebtnclear_Click(object sender, EventArgs e)
        {
            cleartextbox();
        }

        private void iconbtnupdate_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {

                txtname.Text = dataGridView3.SelectedRows[0].Cells["pizza_name"].Value.ToString();
                txtprice.Text = dataGridView3.SelectedRows[0].Cells["price"].Value.ToString();
                txtdesc.Text = dataGridView3.SelectedRows[0].Cells["description"].Value.ToString();
                comboBox1.Text = dataGridView3.SelectedRows[0].Cells["pizza_size"].Value.ToString();
                panel3.Visible = true;
            }
            else
            {
                MessageBox.Show("Please select a pizza to update");
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconbtndelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this pizza", "pizza delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int pizzaid = (int)dataGridView3.CurrentRow.Cells["pizza_id"].Value;

                if (dataGridView3.SelectedRows.Count > 0)
                {
                    pizzaservice.pizzadelete(pizzaid);
                    MessageBox.Show("pizza has been deleted");
                    showdata();
                }
                else
                {
                    MessageBox.Show("Please select a pizza first");
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView3.CurrentRow.Cells["pizza_id"].Value;
            string name = dataGridView3.SelectedRows[0].Cells["pizza_name"].Value.ToString();
            decimal price = Convert.ToDecimal(dataGridView3.SelectedRows[0].Cells["price"].Value);
            string desc = dataGridView3.SelectedRows[0].Cells["description"].Value.ToString();
            int size = 1 + comboBox1.SelectedIndex;

            if (!decimal.TryParse(txtprice.Text, out decimal newPrice))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (txtname.Text != name || txtdesc.Text != desc || newPrice != price)
            {
                pizzaservice.pizzaupdate(id, txtname.Text, txtdesc.Text, Convert.ToDecimal(txtprice.Text), size);
                MessageBox.Show("Selected pizza has been updated");
                txtdesc.Clear();
                txtname.Clear();
                txtprice.Clear();
                panel3.Visible = false;
                showdata();
            }
            else
            {
                MessageBox.Show("The pizza data is same with the new data you want to enter");
            }
        }
    }
}
