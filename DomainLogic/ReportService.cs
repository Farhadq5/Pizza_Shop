using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace DomainLogic
{
    public class ReportService
    {
        ReportDoa reportdoa = new ReportDoa();

        public DataTable showreport()
        {
            DataTable reporttb = new DataTable();
            reporttb = reportdoa.showallreport();
            return reporttb;
        }

        public DataTable cancelrepot()
        {
            DataTable reporttb = new DataTable();
            reporttb = reportdoa.cancelreport();
            return reporttb;
        }

        public DataTable employeesales()
        {
            DataTable reporttb = new DataTable();
            reporttb = reportdoa.employeesales();
            return reporttb;
        }

        public void backup(string path,int userid)
        {
            reportdoa.databasebackup(path,userid);
        }

        public DataTable backuphistory()
        {
            DataTable backuphistory = new DataTable();
            backuphistory = reportdoa.backhistory();
            return backuphistory;
        }

        public void restorbackup(string path,int userid) 
        {
            reportdoa.loadbackup(path,userid);
        }
    }

    
}
