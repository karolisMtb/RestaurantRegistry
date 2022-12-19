using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;
using System.Linq;

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

        public CustomerReceipt GenerateCustomerReceipt()
        {
            Table table = null;
            CustomerReceipt customerReceipt = null;

            foreach (var tableOrder in tableOrderRepository.allOrders)
            {
                if(tableOrder.IsPaid == false)
                {
                    table = tableRepository.tables.First(x => x.Number == tableOrder.TableNumber);
                    table.TableLeavingTime = table.TableTakingTime.AddMinutes(new Random().Next(15, 30));

                    customerReceipt = new CustomerReceipt()
                    {
                        TableOpenTime = table.TableTakingTime,
                        TableCloseTime = tableOrderRepository.GetTableCloseTime(table),
                        OrderNumber = tableOrderRepository.GetOrderNumber(table),
                        TableOrders = tableOrderRepository.GetTableOrders(table),
                        TotalSales = tableOrderRepository.GetTableSales(table)             
                    };

                    receiptRepository.customerReceiptList.Add(customerReceipt);
                    table.Status = Table.FREE_STATE;
                    table.SeatsTaken = 0;
                    tableOrder.IsPaid = true;
                    table.OrdersCount = 0;
                }
            }

            return customerReceipt;
        }

        public RestaurantReceipt GenerateRestaurantReceipt()
        {
            financialService = new FinancialService(tableOrderRepository);
            RestaurantReceipt salesReceipt = new RestaurantReceipt()
            {
                TotalSales = financialService.GetTotalSales(),
                TotalProfit = financialService.GetTotalProfit(),
                TableAmountToPay = tableOrderRepository.GetTablesAmountToPay()
            };

            receiptRepository.RestaurantReceipt = salesReceipt;
            return salesReceipt;
        }
    }
}
