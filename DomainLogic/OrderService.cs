using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DomainLogic
{
    public class OrderService
    {
        OrderDoa orderdao = new OrderDoa();

        public int orders(int customerid)
        {
            int orderid = orderdao.ordersplaced(customerid);
            return orderid;
        }

        public void orderitems(int orderid, int pizzaid, int quantity,string pizzaname,string size,decimal subtotal)
        {
            orderdao.orderitems(orderid, pizzaid, quantity,pizzaname,size,subtotal);
        }

        public DataTable orderstable() 
        {
            DataTable orderitemtable = new DataTable();
            orderitemtable = orderdao.Showordrs();
            return orderitemtable;
        }

        public void orderstatusupdate(int userid,int orderid) 
        {
            orderdao.orderupdate(userid, orderid);
        }

        public void ordercanel(int userid,int orderid) 
        {
            orderdao.ordercancel(userid, orderid);
        }
    }
}
