using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;
using System.Linq;

namespace RestaurantRegistry.Services
{
    public class FinancialService : IFinancialService
    {
        TableOrderRepository allTableOrders;
        public FinancialService(TableOrderRepository allTableOrders)
        {
            this.allTableOrders = allTableOrders;
        }

        double totalSales;
        double totalProfit;

        public double GetTotalSales()
        {
            totalSales = 0;

            foreach (var order in allTableOrders.allOrders)
            {
                if(order.IsPaid == true)
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
            totalProfit = 0;

            foreach (var order in allTableOrders.allOrders)
            {
                if (order.IsPaid == true)
                {
                    foreach (var foodItem in order.foodItems)
                    {
                        totalProfit += foodItem.Profit;
                    }
                }
                else
                {
                    Console.WriteLine("Table has not paid yet");
                }
            }
            return totalProfit;
        }

        public double GetTableAmountToPay(Table table)
        {
            double amountToPay = 0;

            TableOrder tableOrder = allTableOrders.allOrders.First(x => x.TableNumber == table.Number && x.IsPaid == false);
            foreach(var foodItem in tableOrder.foodItems)
            {
                amountToPay += foodItem.SalePrice;
            }
            return amountToPay;
        }
    }
}
