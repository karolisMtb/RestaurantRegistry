using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

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

        public RestaurantService()
        {
            tableRepository = new TableRepository();
            receiptRepository = new ReceiptRepository();
        }

        public void StartService()
        {
            tableOrderRepository = new TableOrderRepository();

            for(int i = 0; i < 20; i++)
            {
                int customerCount = GenerateRandomCustomerCount();
                int tableCountNeeded = GetTableCountNeeded(customerCount);
            
                if(GetFreeSeatsCount() >= customerCount)
                {
                    FindAvailableSeats(customerCount, tableCountNeeded);
                    TakeOrders();
                }
                else
                {
                    Console.WriteLine("There are no free tables");
                    break;
                }
            }

            receiptGenerator = new ReceiptGenerator(tableOrderRepository, receiptRepository, tableRepository);
            receiptGenerator.GenerateCustomerReceipt();
            receiptGenerator.GenerateRestaurantReceipt();
        }

        public int GenerateRandomCustomerCount()
        {
            int randomCustomerCount = new Random().Next(2, 5);
            return randomCustomerCount;
        }

        public int GetTableCountNeeded(int customerNumber)
        {            
            int tableCountNeeded;

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
            int maxSeatCount = 4;
            int minSeatCount = 2;
            int found = 0;
            int randomSeatTakingTimeSpan = new Random().Next(10, 20);

            foreach(Table table in tableRepository.tables)
            {
                if (numOfPeopleToBeSeated >= maxSeatCount && table.Status == Table.FREE_STATE)
                {
                    numOfPeopleToBeSeated -= table.SeatCount;
                    table.Status = Table.TAKEN_STATE;
                    table.SeatsTaken = maxSeatCount;
                    table.TableTakingTime = table.TableLeavingTime.AddMinutes(randomSeatTakingTimeSpan);
                    found++;
                    break;
                }                    
                else if(numOfPeopleToBeSeated < maxSeatCount && numOfPeopleToBeSeated > minSeatCount)
                {
                    int restPeople = numOfPeopleToBeSeated;

                    if ( table.SeatCount == maxSeatCount && table.Status == Table.FREE_STATE)
                    {
                        SeatCustomer(numOfPeopleToBeSeated, table);
                        found++;
                        break;
                    }
                }
                else if (numOfPeopleToBeSeated <= minSeatCount)
                {
                    if(table.SeatCount == minSeatCount && table.Status == Table.FREE_STATE)
                    {
                        SeatCustomer(numOfPeopleToBeSeated, table);
                        found++;
                        break;
                    }
                }
            }

            return tableRepository.tables;
        }

        public void SeatCustomer(int numOfCustomersToBeSeated, Table table)
        {
            int randomSeatTakingTimeSpan = new Random().Next(10, 20);

            table.SeatsTaken = numOfCustomersToBeSeated;
            numOfCustomersToBeSeated -= numOfCustomersToBeSeated;
            table.Status = Table.TAKEN_STATE;
            table.TableTakingTime = table.TableLeavingTime.AddMinutes(randomSeatTakingTimeSpan);
        }

        public void TakeOrders()
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

        public void WriteRestraurantReportToFile()
        {
            string path = "C:\\Users\\Karolis\\source\\repos\\RestsurantRegistryApp\\RestaurantRegistry\\Files\\";
            string restaurantReportFile = "RestaurantReport.json";
            string customerReportFile = "CustomerReport.json";

            if(File.Exists(path += restaurantReportFile))
            {
                File.Delete(path + restaurantReportFile);
            }

            if(File.Exists(path += customerReportFile))
            {
                File.Delete(path += customerReportFile);
            }

        }

        public void SendReportToEmail()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("senderEmail");
            mail.To.Add("receiverEmail");
            mail.Subject = "Test mail";
            mail.Body = "Mail with attachment";
            mail.Attachments.Add(new Attachment("C:\\Users\\Karolis\\source\\repos\\RestsurantRegistryApp\\RestaurantRegistry\\Files\\RestaurantReport.json"));
            mail.Attachments.Add(new Attachment("C:\\Users\\Karolis\\source\\repos\\RestsurantRegistryApp\\RestaurantRegistry\\Files\\CustomerReport.json"));

            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("seenderEmail", "myPSW");
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mail);

            Console.WriteLine("Email sent");
        }
    }

}
