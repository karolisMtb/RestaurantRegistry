using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantRegistry.Repositories
{
    public class TableOrderRepository : ITableOrderRepository
    {
        public List<TableOrder> allOrders;

        public TableOrderRepository()
        {
            allOrders = new List<TableOrder>();
        }

        public DateTime GetTableCloseTime(Table table)
        {
            return table.TableLeavingTime;
        }

        public Guid GetOrderNumber(Table table)
        {
            return allOrders.First(x => x.TableNumber == table.Number && x.IsPaid == false).OrderNumber;
        }

        public List<TableOrder> GetTableOrders(Table table)
        {
            return allOrders.FindAll(x => x.TableNumber == table.Number && x.IsPaid == false).ToList();
        }
        public double GetTableSales(Table table)
        {
            double amountToPay = 0;
            foreach (TableOrder order in allOrders)
            {
                if (table.Number == order.TableNumber && order.IsPaid == false)
                {
                    foreach (FoodItem foodItem in order.foodItems)
                    {
                        amountToPay += foodItem.SalePrice;
                    }
                }
            }
            return amountToPay;
        }

        public double GetTablesAmountToPay()
        {
            double amountToPay = 0;
            foreach (TableOrder order in allOrders)
            {
                if(order.IsPaid == false)
                {
                    foreach(FoodItem foodItem in order.foodItems)
                    {
                        amountToPay += foodItem.SalePrice;
                    }
                }
            }
            return amountToPay;
        }
    }
}
