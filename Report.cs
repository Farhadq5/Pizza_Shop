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
    public partial class Report : Form
    {
        ReportService repotservice = new ReportService();
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = repotservice.showreport();
        }
    }
}
