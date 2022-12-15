using RestaurantRegistry.Models;

namespace RestaurantRegistry.Interfases
{
    public interface IFinancialService
    {
        double GetTotalSales();
        double GetTotalProfit();
        double GetTableAmountToPay(Table table);
    }
}
