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
    public partial class EmployeeInfo : Form
    {
        private int userid;
        private int roleid;
        EmployeeService empService;
        public EmployeeInfo(int Userid,int Roleid)
        {
            InitializeComponent();
            empService = new EmployeeService();
            userid = Userid;
            roleid = Roleid;
        }

        private void Insertcustomer_Click(object sender, EventArgs e)
        {
            try
            {
                empService.firsttimelodin(userid, txtfirstname.Text.Trim(), txtlastname.Text.Trim(), txtphone.Text.Trim(), txtemail.Text.Trim(), roleid);

                MainMenu mainmenu = new MainMenu(userid,roleid);
                mainmenu.Show();
                this.Hide();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("please fill all the informations" + ex.Message);
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void EmployeeInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
