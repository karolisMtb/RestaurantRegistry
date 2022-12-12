using LINQtoCSV;
using System;

namespace RestaurantRegistry.Models
{
    [Serializable]
    public class FoodItem
    {
        [CsvColumn(Name = "ID", FieldIndex = 0)]
        public int Id { get; set; }
        [CsvColumn(Name = "Description", FieldIndex = 1)]
        public string Description { get; set; }
        [CsvColumn(Name = "SalePrice", FieldIndex = 2)]
        public double SalePrice { get; set; }
        public virtual double Profit { get; set; }
    }
}
