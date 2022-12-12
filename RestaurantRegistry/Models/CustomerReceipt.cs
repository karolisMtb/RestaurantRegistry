using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Models
{
    public class CustomerReceipt : Receipt
    {
        public List<FoodItem> FoodItems { get; set; }
        public DateTime TableOpenTime { get; set; }
        public DateTime TableClosedDateTime { get; set; }

        public CustomerReceipt()
        {
            this.Number = Guid.NewGuid();
        }
    }
}
