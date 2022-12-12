using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Models
{
    public class Table
    {
        public const string FREE_STATE = "Free";
        public const string TAKEN_STATE = "Taken";

        public int Number { get; set; }
        public int SeatCount { get; set; }
        public int SeatsTaken { get; set; }
        public string Status { get; set; } = FREE_STATE;
        public DateTime TableTakingTime { get; set; } = DateTime.Now;
        public DateTime? TableLeavingTime { get; set; }
        public List<TableOrder> Orders { get; set; }

        public Table(int number, int seatCount, int seatsTaken, string status)
        {
            Number = number;
            SeatCount = seatCount;
            SeatsTaken = seatsTaken;
            Status = status;
            Orders = new List<TableOrder>();
        }
    }
}
