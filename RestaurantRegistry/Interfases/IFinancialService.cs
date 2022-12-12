using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Interfases
{
    public interface IFinancialService
    {
        double GetTotalSales();
        double GetTotalProfit();
    }
}
