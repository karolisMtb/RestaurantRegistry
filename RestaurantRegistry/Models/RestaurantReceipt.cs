using System;

namespace RestaurantRegistry.Models
{
    public class RestaurantReceipt : Receipt
    {
        //public double Profit 
        public double TotalProfit { get; set; }
        public RestaurantReceipt()
        {
            Number = Guid.NewGuid();
        }
    }
}
