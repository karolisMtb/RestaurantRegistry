using System;

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
        public DateTime TableTakingTime { get; set; } = new DateTime(2022,12,10,8,0,0);
        public DateTime TableLeavingTime { get; set; }
        public int OrdersCount { get; set; }

        public Table(int number, int seatCount, int seatsTaken, string status)
        {
            Number = number;
            SeatCount = seatCount;
            SeatsTaken = seatsTaken;
            Status = status;
        }
    }
}
