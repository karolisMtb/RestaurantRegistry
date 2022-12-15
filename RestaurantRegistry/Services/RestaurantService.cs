using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantRegistry.Services
{
    public class RestaurantService : IRestaurantService
    {
        TableRepository tableRepository;
        TableOrderRepository tableOrderRepository;
        ReceiptGenerator receiptGenerator;
        ReceiptRepository receiptRepository;

        public const int maximumSeatCount = 4;
        public const int minimumSeatCount = 2;

        public RestaurantService(TableOrderRepository tableOrderRepository)
        {
            tableRepository = new TableRepository();
            this.tableOrderRepository = tableOrderRepository;
        }

        public async Task StartService()
        {
            receiptRepository = new ReceiptRepository();
            Task.Delay(new Random().Next(5000, 10000));

            for(int i = 0; i < 20; i++)
            {
                int customerCount = await GenerateRandomCustomerCount();
                int tableCountNeeded = GetTableCountNeeded(customerCount);
            
                if(GetFreeSeatsCount() >= customerCount)
                {
                    FindAvailableSeats(customerCount, tableCountNeeded);
                    TakeOrders();
                    receiptGenerator = new ReceiptGenerator(tableOrderRepository, receiptRepository, tableRepository);
                    receiptGenerator.GenerateRestaurantReceipt();


                    // visa sita logika prideti po TakeOrder() iskvietimo
                    // delay 15- 30 minutes
                    // Call GenerateReceipts
                    // Change table status
                    // change isPaid status
                }
                else
                {
                    // throw new Exception
                    Console.WriteLine("There are no free tables");
                }
            }


            Console.WriteLine("Pries delay");

            Task.Delay(1000);

            Console.WriteLine("Po delay");

            CustomerReceipt cr = await receiptGenerator.GenerateCustomerReceipt();
            Console.ReadKey();
            GetReceipt();
        }

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

        public int GetFreeSeatsCount()
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
            table.SeatsTaken = numOfCustomersToBeSeated;
            numOfCustomersToBeSeated -= numOfCustomersToBeSeated;
            table.Status = Table.TAKEN_STATE;
            table.TableTakingTime = table.TableTakingTime.AddMinutes(new Random().Next(0, 5));
        }

        private void TakeOrders()
        {
            foreach (Table table in tableRepository.tables)
            {
                Guid tableOrderNumber = Guid.NewGuid();
                FoodItemRepository foodItemRepository = new FoodItemRepository();


                if (table.OrdersCount == 0 && table.Status == Table.TAKEN_STATE)
                {
                    TableOrder newOrder = new TableOrder(table.Number, tableOrderNumber);

                    for (int i = 0; i < table.SeatsTaken; i++)
                    {
                        newOrder = new TableOrderGenerator(foodItemRepository).GenerateTableOrder(table, tableOrderNumber);
                        table.OrdersCount++;
                    }
                    tableOrderRepository.allOrders.Add(newOrder);
                }
            }           
        }
        public void GetReceipt()
        {

        }
    }

}
