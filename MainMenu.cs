﻿using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainLogic;

namespace Pizza_Shop
{
    public partial class MainMenu : Form
    {
        //public string firsname { get; set; }
        //public string lastname { get; set; }
        //public int loyalty { get; set; }

        // Fields
        private IconButton currentbtn;
        private Panel leftmainmenupanel;
        private Form currentchildform;
        public string time;
        private int userRole; // Field to store the user's role
        private int userid;
        public MainMenu(int Userid,int Role)
        {
            InitializeComponent();
            leftmainmenupanel = new Panel();
            leftmainmenupanel.Size = new Size(7, 60);
            panelmenu.Controls.Add(leftmainmenupanel);
            //store useer role passed from login

            userRole = Role;
            userid = Userid;
            //from
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            

        }

        //struct
        private struct RGBcolors
        {
            public static System.Drawing.Color color1 = System.Drawing.Color.FromArgb(172, 126, 241);
            public static System.Drawing.Color color2 = System.Drawing.Color.FromArgb(249, 118, 176);
            public static System.Drawing.Color color3 = System.Drawing.Color.FromArgb(253, 138, 114);
            public static System.Drawing.Color color4 = System.Drawing.Color.FromArgb(95, 77, 221);
            public static System.Drawing.Color color5 = System.Drawing.Color.FromArgb(249, 88, 155);
            public static System.Drawing.Color color6 = System.Drawing.Color.FromArgb(24, 161, 251);
        }
        //Metods

        private void ActivateButton(object senderbtn, System.Drawing.Color color)
        {
            if (senderbtn != null)
            {
                Disablebutton();
                //button
                currentbtn = (IconButton)senderbtn;
                currentbtn.BackColor = System.Drawing.Color.FromArgb(31, 30, 68);
                currentbtn.ForeColor = color;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = color;
                currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentbtn.ImageAlign = ContentAlignment.MiddleRight;
                //left border btn
                leftmainmenupanel.BackColor = color;
                leftmainmenupanel.Location = new Point(0, currentbtn.Location.Y);
                leftmainmenupanel.Visible = true;
                leftmainmenupanel.BringToFront();
                //icone current child form
                iconcurrentchildform.IconChar = currentbtn.IconChar;
                iconcurrentchildform.IconColor = currentbtn.IconColor;
            }
        }

        // Disable previously active button
        private void Disablebutton()
        {
            if (currentbtn != null)
            {
                currentbtn.BackColor = Color.FromArgb(37, 36, 81);
                currentbtn.ForeColor = Color.WhiteSmoke;
                currentbtn.TextAlign = ContentAlignment.MiddleLeft;
                currentbtn.IconColor = Color.WhiteSmoke;
                currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        // Open a child form within the panel
        private void Openchildeform(Form childform)
        {
            if (currentchildform != null)
            {
                currentchildform.Close();
            } 
                 currentchildform = childform;
                childform.TopLevel = false;
                childform.FormBorderStyle = FormBorderStyle.None;
                childform.Dock = DockStyle.Fill;
                mainmenupanel.Controls.Add(childform);
                mainmenupanel.Tag = childform;
                childform.BringToFront();
                childform.Show();
                lbltitlechildform.Text = childform.Text;
                 
            
        }
        
        // user role based logic
        private void visibitybyrole()
        {
            // fix the role no this is test only
            switch (userRole) 
            {
                case 4:
                    //its for admin menu
                    break;
                case 3:
                    //for delevery menu
                    EmployeeService employeeService = new EmployeeService();
                    string[] empinfo = employeeService.employeedata(userid);
                    btnmanageemployess.Visible = false;
                    btnreports.Visible = false;
                    btnmanageorders.Visible = false;
                    btnsetting.Visible = false;
                    lblposition.Text = "Delevery workwr";
                    lblfname.Text = empinfo[0+1];
                    break;
                case 1:
                    //for admin menu
                   // btnmanageemployess.Visible=false;
                    btnreports.Visible = false;
                    btnmanageoizzamenu.Visible = false;
                    //btnsetting.Visible = false;
                    break;
                case 5:
                    CustomerService customerservice = new CustomerService();
                    string[] custinfo = customerservice.GetCustomerInfo(userid);
                    //for customer menu
                    btnmanageemployess.Visible = false;
                    btnreports.Visible = false;
                    btnmanageoizzamenu.Visible = false;
                    btnreports.Visible = false;

                    label2.Text = "Loyalty";
                    lblfname.Text = custinfo[0 + 1];
                    lblposition.Text = custinfo[2];
                    //and more
                    break ;
                default: break;
                
            }

           
        }
        private void btndashboard_Click(object sender, EventArgs e)
        {      
            ActivateButton(sender, RGBcolors.color1);
            Openchildeform(new DashBoard());
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            ManageEmployees manage = new ManageEmployees();
            manage.userid = userid;
            visibitybyrole();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close the application", "warning", MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void lblposition_Click(object sender, EventArgs e)
        {

        }
        public void SetCustomerInfo(string firstname, string lastname, int loyaltyPoints)
        {
            
            // Set the values in the Main Menu, e.g., in labels or text boxes
           
        }

        private void btnmanageemployess_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color1);
            Openchildeform(new ManageEmployees());
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            ActivateButton(sender,RGBcolors.color2);
            Openchildeform(new sethings(userid));
        }
    }
}
