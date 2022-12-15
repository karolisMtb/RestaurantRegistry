using System;
using System.Collections.Generic;

namespace RestaurantRegistry.Models
{
    public class CustomerReceipt : Receipt
    {
        public DateTime TableOpenTime { get; set; }
        public DateTime TableCloseTime{ get; set; }

        public CustomerReceipt()
        {
            this.Number = Guid.NewGuid();
        }
    }
}
