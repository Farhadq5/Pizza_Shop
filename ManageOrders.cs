using DomainLogic;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace Pizza_Shop
{
    public partial class ManageOrders : Form
    {
        private readonly ManageOrderService manageorderservice = new ManageOrderService();
        private int role;
        private int userid;
        public ManageOrders(int UserRole, int Userid)
        {
            InitializeComponent();
            role = UserRole;
            userid = Userid;
        }


        private void ManageOrders_Load(object sender, EventArgs e)
        {
            if (role == 5)
            {
                dataGridView1.DataSource = manageorderservice.customerorder(userid);
                btnCompleat.Visible = false;
            }
            else
            {
                Showdata();
            }

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Ensure this logic applies only to the OrderStatus column
            if (dataGridView1.Columns[e.ColumnIndex].Name == "OrderStatus" && e.Value != null || dataGridView1.Columns[e.ColumnIndex].Name == "ItemStatus" && e.Value != null)
            {
                string OrderStatus = e.Value.ToString();

                // Apply cell colors based on status
                switch (OrderStatus.ToLower())
                {
                    case "pending":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Orange; // Text color
                        break;

                    case "completed":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.LightGreen; // Text color
                        break;

                    case "canceled":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Red; // Text color
                        break;

                    default:
                        // Optional: Set a default style for unhandled statuses
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Black;
                        break;
                }
            }
        }
        private void Showdata()
        {
            dataGridView1.DataSource = manageorderservice.ordersummery();
        }

        private void btnCompleat_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected OrderId
                int orderId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderId"].Value);

                try
                {
                    // Get the order details
                    DataTable orderDetails = manageorderservice.orderdetail(orderId);

                    // Bind the details to another DataGridView
                    dataGridView1.DataSource = orderDetails;

                    dataGridView1.AutoGenerateColumns = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading order details: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select an order first.");
                Showdata();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (role == 5)
            {
                dataGridView1.DataSource = manageorderservice.customerrecentorder(userid);
            }
            else
              dataGridView1.DataSource = manageorderservice.recentorder();

        }
    }
}
