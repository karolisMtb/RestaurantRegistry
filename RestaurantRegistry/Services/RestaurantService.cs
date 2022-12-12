using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RestaurantRegistry.Services
{
    public class RestaurantService : IRestaurantService
    {

        IFoodItemRepository ifoodItemRepository;
        TableOrderGenerator orderGenerator;
        TableRepository tableRepository;
        TableOrderRepository tableOrderRepository;

        public const int maximumSeatCount = 4;
        public const int minimumSeatCount = 2;

        public RestaurantService(IFoodItemRepository ifoodItemRepository, TableOrderGenerator orderGenerator, TableRepository tableRepository, TableOrderRepository tableOrderRepository)
        {
            this.ifoodItemRepository = ifoodItemRepository;
            this.orderGenerator = orderGenerator;
            this.tableRepository = tableRepository;
            this.tableOrderRepository = tableOrderRepository;
        }

        public async Task StartService()
        {
            Task.Delay(new Random().Next(5000, 10000));

            for(int i = 0; i < 2; i++)
            {
                int customerCount = await GenerateRandomCustomerCount();
                int tableCountNeeded = GetTableCountNeeded(customerCount);
            
                if(GetFreeSeatsCount() >= customerCount)
                {
                    Console.WriteLine($"{customerCount} customer need to be seated");
                    Console.WriteLine($"We have {GetFreeSeatsCount()} seats available");

                    FindAvailableSeats(customerCount, tableCountNeeded);
                    TakeOrders();
                }
                else
                {
                    // throw new Exception
                    Console.WriteLine("Neturim laisvu staliuku");
                }
            }
        }
        // close table, change status GenerateReceipt(), DisplayReceipt()
        // pabandyti async panaudoti, kad kelis staliukai vienu metu uzsisakinetu maista, random laika sedetu ir susimoketu
        // GenerateRestaurantStats();

        public async Task<int> GenerateRandomCustomerCount()
        {
            int randomCustomerCount = new Random().Next(2, 5);
            return randomCustomerCount;
        }

        public int GetTableCountNeeded(int customerNumber)
        {            
            int tableCountNeeded;
            int numOfPeopleToBeSeated = customerNumber;

            if (customerNumber % maximumSeatCount == 0)
            {
                tableCountNeeded = customerNumber / maximumSeatCount;
            }
            else
            {
                tableCountNeeded = customerNumber / maximumSeatCount + 1;
            }
            return tableCountNeeded;
        }

        private int GetFreeSeatsCount()
        {
            int seatsCount = 0;
            foreach (Table table in tableRepository.tables)
            {
                if (table.Status == Table.FREE_STATE)
                    seatsCount += table.SeatCount;
            }
            return seatsCount;
        }

        public List<Table> FindAvailableSeats(int numOfPeopleToBeSeated, int tableCountNeeded)
        {
            int max = 4;
            int min = 2;
            int found = 0;

            do
            {
                foreach(Table table in tableRepository.tables)
                {
                    if (numOfPeopleToBeSeated >= max && table.Status == Table.FREE_STATE)
                    {
                        numOfPeopleToBeSeated -= table.SeatCount;
                        table.Status = Table.TAKEN_STATE;
                        table.SeatsTaken = max;
                        found++;
                        break;
                    }                    
                    else if(numOfPeopleToBeSeated < max && numOfPeopleToBeSeated > min)
                    {
                        int restPeople = numOfPeopleToBeSeated;

                        if ( table.SeatCount == max && table.Status == Table.FREE_STATE)
                        {
                            SeatCustomer(numOfPeopleToBeSeated, table);
                            found++;
                            break;
                        }
                    }
                    else if (numOfPeopleToBeSeated <= min)
                    {
                        if(table.SeatCount == min && table.Status == Table.FREE_STATE)
                        {
                            SeatCustomer(numOfPeopleToBeSeated, table);
                            found++;
                            break;
                        }
                    }
                }
            }
            while (found < tableCountNeeded);

            return tableRepository.tables;
        }

        private void SeatCustomer(int numOfCustomersToBeSeated, Table table)
        {
            Console.WriteLine($"{numOfCustomersToBeSeated} customers are seated at {table.Number} table");
            table.SeatsTaken = numOfCustomersToBeSeated;
            numOfCustomersToBeSeated -= numOfCustomersToBeSeated;
            table.Status = Table.TAKEN_STATE;
        }

        private void TakeOrders() // cia yra naudojama table modelis ir jo listas (Orders)
        {
            foreach (Table table in tableRepository.tables)
            {
                Guid tableOrderNumber = Guid.NewGuid();

                if (table.Orders.Count == 0 && table.Status == Table.TAKEN_STATE)
                {
                    for (int i = 0; i < table.SeatsTaken; i++) // yra sukuriami tiek uzsakymu kiek yra sedimu vietu
                    {
                        table.Orders.Add(new TableOrderGenerator(ifoodItemRepository, tableOrderRepository).GenerateTableOrder(table, tableOrderNumber));
                    }
                }
            }
        }
    }

}
