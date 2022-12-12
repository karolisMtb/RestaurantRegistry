using RestaurantRegistry.Interfases;
using RestaurantRegistry.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Services
{
    public class FinancialService : IFinancialService
    {
        TableOrderRepository allTableOrders;
        public FinancialService(TableOrderRepository allTableOrders)
        {
            this.allTableOrders = allTableOrders;
        }

        public double GetTotalSales()
        {
            return 0;
        }

        public double GetTotalProfit()
        {
            return 0;
        }
    }
}
