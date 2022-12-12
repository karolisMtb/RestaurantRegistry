using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Models
{
    public class TableOrder
    {
        public Guid OrderNumber { get; set; }
        public int TableNumber { get; set; }
        public List<FoodItem> foodItems { get; set; }
        public double AmountToPay { get; set; }

        public TableOrder(int tableNumber, Guid OrderNumber)
        {
            this.OrderNumber = OrderNumber;
            TableNumber = tableNumber;
            foodItems = new List<FoodItem>();
        }
    }
}
