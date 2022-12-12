using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Models
{
    public class RestaurantReceipt : Receipt
    {
        //public double Profit 
        public int TableNumber { get; set; }

        public RestaurantReceipt()
        {
            Number = Guid.NewGuid();
        }
    }
}
