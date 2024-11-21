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
    public partial class DashBoard : Form
    {
        private PizzaService pizzaservice;
        public DashBoard()
        {
            InitializeComponent();
            pizzaservice = new PizzaService();
            pizzablocks();

        }
        private void pizzablocks()
        {
            // Retrieve pizza data from the service
            DataTable pizzatb = pizzaservice.GetPizza();

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
                   // Image = Properties.Resources.DefaultPizzaImage, // Placeholder image
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(200, 140),
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

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int pizzaId = Convert.ToInt32(btn.Tag);

            DialogResult result = MessageBox.Show(
                "Do you want to add this pizza to the cart?",
                "Add to Cart",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Logic to add pizza to the cart
                MessageBox.Show($"Pizza with ID {pizzaId} added to the cart!");
            }
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            // Optional: Reload pizzas when the dashboard is loaded
           // pizzablocks();
        }
    }
}

