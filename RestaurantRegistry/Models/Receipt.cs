using System;
using System.Collections.Generic;

namespace RestaurantRegistry.Models
{
    public class Receipt
    {
        public Guid Number { get; set; }
        public Guid OrderNumber { get; set; }
        public DateTime DateTime { get; set; }
        public double TotalSales { get; set; }
        public List<TableOrder> TableOrders { get; set; }
    }
}
