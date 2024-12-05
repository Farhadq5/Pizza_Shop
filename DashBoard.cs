using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainLogic;

namespace Pizza_Shop
{
    public partial class DashBoard : Form
    {
        private PizzaService pizzaservice;
        private OrderService orderservice;
        private DataTable carttable;
        private DataTable pizzatb;
        private int userid;
        private int userrole;
        public DashBoard(int UserId, int UserRole)
        {
            InitializeComponent();
            pizzaservice = new PizzaService();
            orderservice = new OrderService();
            userid = UserId;
            userrole = UserRole;
            
            if (UserRole == 5)
            {
                carttable = new DataTable();
                pizzatb = new DataTable();
                pizzablocks();
                CartBlock();
                CartPanel();
                panelsvisibality(true);
                panel5.Visible = false;
            }
            else if(UserRole == 3)
            {
                panel5.Dock = DockStyle.Fill;
                panelsvisibality(false);
                showdata();
            }
            else if (UserRole == 1)
            {
                panel5.Dock = DockStyle.Fill;
                panelsvisibality(false);
                showdata();
            }
        }
        private void panelsvisibality(bool visible)
        {
            panel1.Visible = visible;
            panel2.Visible = visible;
            panel3.Visible = visible;
            panel4.Visible = visible;
        }

        private void showdata()
        {
            dataGridView1.DataSource = orderservice.orderstable();

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


        #region for customers they can order pizza
        private void pizzablocks()
        {
            // Retrieve pizza data from the service
             pizzatb = pizzaservice.GetPizza();

            // Clear any existing blocks
            flowLayoutPanel1.Controls.Clear();

            // Generate UI for each pizza
            foreach (DataRow row in pizzatb.Rows)
            {
                // Create a panel for each pizza
                Panel pizzapanel = new Panel
                {
                    Size = new Size(220, 320),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                // PictureBox for the pizza image
                PictureBox pizzaImage = new PictureBox
                {
                    Image = Image.FromFile(@"images\defualtpizza.jpg"), // Placeholder image
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(180, 120),
                    Location = new Point(10, 10)
                };
             
                // Label for pizza name
                Label lblName = new Label
                {
                    Text = row["pizza_name"].ToString(),
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Location = new Point(10, 140),
                    AutoSize = true
                };

                // Label for pizza description
                Label lblDescription = new Label
                {
                    Text = row["description"].ToString(),
                    Location = new Point(10, 160),
                    AutoSize = true,
                    MaximumSize = new Size(180, 40) // Limit description size
                };

                // Label for pizza size
                Label lblSize = new Label
                {
                    Text = $"Size: {row["pizza_size"]}",
                    Location = new Point(10, 200),
                    AutoSize = true
                };

                // Label for pizza price
                Label lblPrice = new Label
                {
                    Text = $"Price: ${row["price"]}",
                    Location = new Point(10, 220),
                    AutoSize = true
                };

                // Button to add pizza to the cart
                Button btnAddToCart = new Button
                {
                    Text = "Add to Cart",
                    Tag = row["pizza_id"], // Store pizza ID in the Tag property
                    Location = new Point(10, 250),
                    Size = new Size(180, 30)
                };

                // Event handler for the button
                btnAddToCart.Click += btnAddToCart_Click;

                flowLayoutPanel1.AutoScroll = true;
                CartflowLayoutPanel.WrapContents = false;

                // Add all components to the pizza panel
                pizzapanel.Controls.Add(pizzaImage);
                pizzapanel.Controls.Add(lblName);
                pizzapanel.Controls.Add(lblDescription);
                pizzapanel.Controls.Add(lblSize);
                pizzapanel.Controls.Add(lblPrice);
                pizzapanel.Controls.Add(btnAddToCart);

                // Add the pizza panel to the FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(pizzapanel);
            }
        }
        
        
        private void CartBlock()
        {
            CartflowLayoutPanel.Controls.Clear();
           
            foreach (DataRow row in carttable.Rows)
            {
                //create panel for each cart item
                Panel cartitempanel = new Panel
                {
                    Size = new Size(CartflowLayoutPanel.Width - 15,70),
                    //AutoSize =true,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(3)
                };

                PictureBox pizzaImage = new PictureBox
                {
                    Image = Image.FromFile(@"images\defualtpizza.jpg"), // Placeholder image
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(63,63),
                    Location = new Point(2, 2)
                };

                Label lblname = new Label
                {
                    Text = row["PizzaName"].ToString(),
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Location = new Point(70, 10),
                    AutoSize = true
                };

                // Label for pizza size
                Label lblSize = new Label
                {
                    Text = $"Size: {row["Size"]}",
                    Location = new Point(70, 30),
                    AutoSize = true
                };

                Label lblQuantity = new Label
                {
                    Text = $"Quantity: {row["Quantity"]}",
                    Location = new Point(70, 50),
                    AutoSize = true
                };

                Label lblTotalPrice = new Label
                {
                    Text = $"Total: ${row["TotalPrice"]}",
                    Location = new Point(130, 50),
                    AutoSize = true
                };
                
                Button btnremove = new Button
                {   
                    Text = "remove",
                    Tag = row["PizzaID"],
                    Size = new Size(80, 30),
                    Location = new Point(CartflowLayoutPanel.Width - 100, 36)
                };

                btnremove.Click += btnremove_Click;
                //adding controls to the cart item panel
                cartitempanel.Controls.Add(lblname);
                cartitempanel.Controls.Add(lblSize);
                cartitempanel.Controls.Add(lblQuantity);
                cartitempanel.Controls.Add(lblTotalPrice);
                cartitempanel.Controls.Add(btnremove);
                cartitempanel.Controls.Add(pizzaImage);

                CartflowLayoutPanel.FlowDirection = FlowDirection.TopDown;
                CartflowLayoutPanel.AutoScroll = true;
                CartflowLayoutPanel.WrapContents = false;
               

                CartflowLayoutPanel.Controls.Add(cartitempanel);
            }
        }

        private void totalprice()
        {
            decimal total = 0;
            foreach(DataRow row in carttable.Rows)
            {
                total += Convert.ToDecimal(row["TotalPrice"]);
            }
            lblSubTotyal.Text = $"Total Price: ${total}";

        }

        private void referashcart()
        {
         CartflowLayoutPanel.Controls.Clear();
            foreach(DataRow row in carttable.Rows) 
            {
                CartBlock();
            }
            totalprice();
        }
        private void CartPanel()
        {

           //carttable = new DataTable(); // there is already one instance for cartable
            
            carttable.Columns.Add("PizzaID", typeof(int));
            carttable.Columns.Add("PizzaName", typeof(string));
            carttable.Columns.Add("Size", typeof(string));
            carttable.Columns.Add("Price", typeof(decimal));
            carttable.Columns.Add("Quantity", typeof(int));
            carttable.Columns.Add("TotalPrice", typeof(decimal), "Price * Quantity"); // Auto-calculated column
           
        }
    
   
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int pizzaId = Convert.ToInt32(btn.Tag);

            DialogResult result = MessageBox.Show(
                "Do you want to add this pizza to the cart?",
                "Add to Cart",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Logic to add pizza to the cart
                //MessageBox.Show($"Pizza with ID {pizzaId} added to the cart!");

                // Fetch pizza details from DataTable
                DataRow[] pizzaRows = pizzatb.Select($"pizza_id = {pizzaId}");
                 if (pizzaRows.Length > 0 )
                 {
                    DataRow row = pizzaRows[0];

                    // Check if the item already exists in the cart
                    DataRow[] cartrow = carttable.Select($"PizzaID = {pizzaId}");
                    if(cartrow.Length > 0 )
                    {
                        // Update the quantity and total price
                        cartrow[0]["Quantity"] = Convert.ToInt32(cartrow[0]["Quantity"])+1;

                    }
                    else
                    {
                        // Add a new row to the cart
                        carttable.Rows.Add(
                         
                            pizzaId,
                            row["pizza_name"],
                            row["pizza_size"],
                            Convert.ToDecimal(row["price"]),
                            1   //this the defualt quantity
                            
                        );
                    }

                    referashcart();
                 }
                
            }
        }
        private void btnremove_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int pizzaid = Convert.ToInt32(button.Tag);

            //rewmoving the item
            DataRow[] cartrow = carttable.Select($"PizzaID = {pizzaid}");
            if ((cartrow.Length > 0))
            {
                cartrow[0].Delete();
            }

            referashcart();
        }

        private void btnmanageorders_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Do you want to place this order?",
                 "Order Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (carttable.Rows.Count > 0)
                {
                    int orderid = orderservice.orders(userid);
                    foreach (DataRow row in carttable.Rows)
                    {
                        int pizzaid = Convert.ToInt32(row["PizzaID"]);
                        string pizzaName = row["PizzaName"].ToString();
                        string pizzasize = row["Size"].ToString();
                        decimal price = Convert.ToDecimal(row["Price"]);
                        int quantity = Convert.ToInt32(row["Quantity"]);
                        decimal totalprice = Convert.ToInt32(row["TotalPrice"]);

                        orderservice.orderitems(orderid, pizzaid, quantity, pizzaName, pizzasize, price * quantity);

                    }
                    carttable.Clear();
                    referashcart();
                    MessageBox.Show("Order Placed successfully");
                }
                else
                {
                    MessageBox.Show("cart is empty. please add item");
                }

            }

        }
        #endregion
        private void DashBoard_Load(object sender, EventArgs e)
        {

        }

        private void btnCompleat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you cannot Change it later?",
            "Order Complation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string orderstatus = dataGridView1.SelectedRows[0].Cells["OrderStatus"].Value.ToString();
                    if (orderstatus == "Completed" || orderstatus == "Canceled")
                    {
                        MessageBox.Show($"Order is already {orderstatus}");
                    }
                    else
                    {
                        int orderid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderId"].Value);

                        try
                        {
                            orderservice.orderstatusupdate(userid, orderid);

                            MessageBox.Show("Order has Been compleated successfully.");

                            showdata();
                        }
                        catch (Exception)
                        {

                            throw new Exception($"Error updating order");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("please slelect order first");
                }
            }
        }

        private void btnRemove_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you cannot change it again","Order Cancelled",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string orderstatus = dataGridView1.SelectedRows[0].Cells["OrderStatus"].Value.ToString();
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (orderstatus == "Completed" || orderstatus == "Canceled")
                    {
                        MessageBox.Show($"Order is already {orderstatus}");
                    }
                    else
                    {

                        int orderid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderId"].Value);
                        try
                        {
                            orderservice.ordercanel(userid, orderid);
                            MessageBox.Show("Order hase been Canceled");
                            showdata();
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Error Canceling order");
                        }
                    }
                }
                else
                    MessageBox.Show("Please select an order first");
            }
        }
    }
}

