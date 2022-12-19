using RestaurantRegistry.Interfases;
using RestaurantRegistry.Repositories;
using RestaurantRegistry.Services;
using System;

namespace RestaurantRegistry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestaurantService restaurantService = new RestaurantService();

            int daysOfOperation = 3;

            for(int i = 0; i < daysOfOperation; i++)
            {
                restaurantService.StartService();
                restaurantService.WriteRestraurantReportToFile();
                restaurantService.SendReportToEmail();
            }

        }
    }
}
