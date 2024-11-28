using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DomainLogic
{
    public class PizzaService
    {
        PizzaDoa pizzadao = new PizzaDoa();

       public DataTable GetPizza()
       {
            DataTable pizzatable = new DataTable();
            pizzatable = pizzadao.GetAllPizzas();
            return pizzatable;
       }

        public void pizzaadd(string pizzaname,string pizzadescription,decimal price,int size)
        {
            pizzadao.Pizzaadd(pizzaname, pizzadescription, price,size);
        }

        public void pizzadelete(int pizzaid) 
        {
            pizzadao.deletepizza(pizzaid);
        }

        public void pizzaupdate(int pizzaid, string pizzaname, string description, decimal price, int size)
        {
            pizzadao.updatepizza(pizzaid, pizzaname, description, price, size);
        }
    }

}
