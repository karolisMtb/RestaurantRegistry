﻿using System;

namespace RestaurantRegistry.Models
{
    public class RestaurantReceipt : Receipt
    {
        public double TotalProfit { get; set; }

        public double TableAmountToPay { get; set; }
        public RestaurantReceipt()
        {
            Number = Guid.NewGuid();
        }
    }
}
