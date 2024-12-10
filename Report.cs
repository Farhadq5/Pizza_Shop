using DomainLogic;
using System;
using System.IO;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml;


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
            dataGridView1.AutoGenerateColumns = false;

        }

        public void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Report");

                    // Add column headers
                    for (int col = 0; col < dataGridView.ColumnCount; col++)
                    {
                        worksheet.Cells[1, col + 1].Value = dataGridView.Columns[col].HeaderText;
                    }

                    // Add rows
                    for (int row = 0; row < dataGridView.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataGridView.Columns.Count; col++)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dataGridView.Rows[row].Cells[col].Value?.ToString() ?? "";
                        }
                    }

                    // Save to file
                    FileInfo file = new FileInfo(filePath);
                    package.SaveAs(file);

                    MessageBox.Show("Export to Excel completed successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}");
            }
        }


 
        

        private void iconButton3_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Export Data";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export to Excel
                    ExportToExcel(dataGridView1, saveFileDialog.FileName);
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = repotservice.cancelrepot();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = repotservice.employeesales();
        }
    }
}
