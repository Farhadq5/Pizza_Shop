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
    }

}
