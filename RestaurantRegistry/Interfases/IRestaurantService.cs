namespace RestaurantRegistry.Interfases
{
    public interface IRestaurantService
    {
        int GetTableCountNeeded(int customerNumber);
        int GetFreeSeatsCount();
    }
}
