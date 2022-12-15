using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;

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
            double totalSales = 0;

            foreach (var order in allTableOrders.allOrders) // yra kiekvienas tableOrder kuriame yra listas visu foodItems kuriuos uzsisake
            {
                if(order.IsPaid == false) // && table.Status == TAKEN_STATUS tikrinti
                {
                    foreach(var foodItem in order.foodItems)
                    {
                        totalSales += foodItem.SalePrice;
                    }
                }
                else
                {
                    Console.WriteLine("Nobody has paid yet");
                }
            }

            Console.WriteLine($"Total sales for the day is {totalSales}");
            return totalSales;
        }

        public double GetTotalProfit()
        {
            return 0;
        }

        public double GetTableAmountToPay(Table table)
        {
            return 0;
        }
    }
}
