using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace DomainLogic
{
    public class ManageOrderService
    {
        ManageOrdersDoa manageordersdoa = new ManageOrdersDoa();

        public DataTable ordersummery()
        {
            DataTable ordersummery = new DataTable();
            ordersummery=manageordersdoa.Getordersummery();
            return ordersummery;
        }

        public DataTable orderdetail(int userid) 
        {
            DataTable orderdetail = new DataTable();
            orderdetail = manageordersdoa.getorderdetail(userid);
            return orderdetail;
        }

        public DataTable recentorder() 
        {
            DataTable recentorder = new DataTable();
            recentorder = manageordersdoa.Getrecentorder();
            return recentorder;
        }

        public DataTable customerorder(int userid)
        {
            DataTable customerordertable = new DataTable();
            customerordertable = manageordersdoa.showcustomerdata(userid);
            return customerordertable;
        }

        public DataTable customerrecentorder(int userid)
        {
            DataTable customerrecentorder = new DataTable();
            customerrecentorder = manageordersdoa.showrecentcustomerorder(userid);
            return customerrecentorder;
        }
    }
}
