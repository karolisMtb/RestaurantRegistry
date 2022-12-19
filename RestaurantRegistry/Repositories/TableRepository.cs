using RestaurantRegistry.Models;
using System.Collections.Generic;

namespace RestaurantRegistry.Repositories
{
    public class TableRepository
    {
        public List<Table> tables = new List<Table>()
        {
            new Table(10, 4, 0, "Free"),
            new Table(11, 4, 0, "Free"),
            new Table(12, 4, 0, "Free"),
            new Table(13, 4, 0, "Free"),
            new Table(14, 4, 0, "Free"),
            new Table(15, 4, 0, "Free"),
            new Table(16, 2, 0, "Free"),
            new Table(17, 2, 0, "Free"),
            new Table(18, 2, 0, "Free"),
            new Table(19, 2, 0, "Free"),
        };
        public TableRepository()
        {

        }
    }
}
