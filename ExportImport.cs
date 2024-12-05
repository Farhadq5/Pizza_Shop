using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainLogic;
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml;

namespace Pizza_Shop
{
    public partial class ExportImport : Form
    {
        ReportService reportservice = new ReportService();
        private int userid;
        public ExportImport(int Userid)
        {
            InitializeComponent();
            showhistory();
            userid = Userid;
        }

        private void ExportImport_Load(object sender, EventArgs e)
        {

        }

        private void showhistory()
        {
            dataGridView1.DataSource = reportservice.backuphistory();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Backup files (*.bak)|*.back";
                saveFile.Title = "Select a location to save backup";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    string backupPath = saveFile.FileName;
                    try
                    {
                        reportservice.backup(backupPath, userid);
                        showhistory();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openfile = new OpenFileDialog())
            {
                openfile.Filter = "Backup files (*.bak)|*.bak";
                openfile.FileName = "Select a database backup file";
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    string backup = openfile.FileName;
                    try
                    {
                      reportservice.restorbackup(backup, userid);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
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

        private void iconButton2_Click(object sender, EventArgs e)
        {
            using (var saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFile.Title = "Export Data";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    // Export to Excel
                    ExportToExcel(dataGridView1, saveFile.FileName);
                }
            }
        }
    }
}
