using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRegistry.Services
{
    public class ReceiptGenerator
    {
        TableOrderRepository tableOrderRepository;
        IFinancialService financialService;
        ReceiptRepository receiptRepository;
        TableRepository tableRepository;

        public ReceiptGenerator(TableOrderRepository tableOrderRepository, ReceiptRepository receiptRepository, TableRepository tableRepository)
        {
            this.tableOrderRepository = tableOrderRepository;
            this.receiptRepository = receiptRepository;
            this.tableRepository = tableRepository;
        }

        public async Task<CustomerReceipt> GenerateCustomerReceipt()
        {
            Table table = null;
            CustomerReceipt customerReceipt = null;

            foreach (var tableOrder in tableOrderRepository.allOrders)
            {
                if(tableOrder.IsPaid == false)
                {
                    table = tableRepository.tables.First(x => x.Number == tableOrder.TableNumber);
                    
                    customerReceipt = new CustomerReceipt()
                    {
                        TableOpenTime = table.TableTakingTime,
                        TableCloseTime = tableOrderRepository.GetTableCloseTime(table),
                        OrderNumber = tableOrderRepository.GetOrderNumber(table),
                        TableOrders = tableOrderRepository.GetTableOrders(table),
                        TotalSales = tableOrderRepository.GetTableAmountToPay(table)
                    };

                    receiptRepository.customerReceiptList.Add(customerReceipt);
                }
                else
                {
                    Console.WriteLine($"Table {table.Number} has already paid");
                }
            }

            return customerReceipt;
        }

        public RestaurantReceipt GenerateRestaurantReceipt()
        {
            financialService = new FinancialService(tableOrderRepository);
            RestaurantReceipt salesReceipt = new RestaurantReceipt()
            {
                // gauti to dienos data kol ji bega async
                TotalSales = financialService.GetTotalSales(),
                TotalProfit = financialService.GetTotalProfit()
            };
            return salesReceipt;
        }
    }
}
