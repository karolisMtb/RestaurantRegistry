using System;
using System.Collections.Generic;

namespace RestaurantRegistry.Models
{
    public class TableOrder
    {
        public Guid OrderNumber { get; set; }
        public int TableNumber { get; set; }
        public List<FoodItem> foodItems { get; set; }
        public bool IsPaid { get; set; } = true;

        public TableOrder(int tableNumber, Guid OrderNumber)
        {
            this.OrderNumber = OrderNumber;
            TableNumber = tableNumber;
            foodItems = new List<FoodItem>();
        }
    }
}
