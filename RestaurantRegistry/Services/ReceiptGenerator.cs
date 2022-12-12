using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Services
{
    public class ReceiptGenerator
    {
        TableOrderRepository tableOrderRepository;

        public ReceiptGenerator()
        {

        }

        public Receipt[] GenerateReceipt(int tableNumber)
        {
            Receipt[] receipts = new Receipt[2];
            
            RestaurantReceipt receipt = new RestaurantReceipt();
            CustomerReceipt customerReceipt = new CustomerReceipt();

            return receipts;
        }
    }
}
