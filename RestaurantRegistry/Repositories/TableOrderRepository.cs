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
           // return table.TableLeavingTime.Value; // pirma stalas yra uzdaromas, o tada pakeiciamas statusas i table free and paid
           return DateTime.Now;
        }

        public Guid GetOrderNumber(Table table)
        {
            return allOrders.First(x => x.TableNumber == table.Number && x.IsPaid == false).OrderNumber;
        }

        public List<TableOrder> GetTableOrders(Table table)
        {
            return allOrders.FindAll(x => x.TableNumber == table.Number && x.IsPaid == false).ToList();
        }

        public double GetTableAmountToPay(Table table)
        {
            double amountToPay = 0;
            foreach (TableOrder order in allOrders)
            {
                if(order.TableNumber == table.Number && order.IsPaid == false)
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
