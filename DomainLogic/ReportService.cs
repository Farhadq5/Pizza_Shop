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
            DataTable reporttbshow = new DataTable();
            reporttbshow = reportdoa.showallreport();
            return reporttbshow;
        }

        public DataTable cancelrepot()
        {
            DataTable reporttbcancel = new DataTable();
            reporttbcancel = reportdoa.cancelreport();
            return reporttbcancel;
        }

        public DataTable employeesales()
        {
            DataTable reporttbemp = new DataTable();
            reporttbemp = reportdoa.employeesales();
            return reporttbemp;
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
