using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Models
{
    public class Receipt
    {
        public Guid Number { get; set; }
        public DateTime DateTime { get; set; }
        public double TotalSales { get; set; }
    }
}
