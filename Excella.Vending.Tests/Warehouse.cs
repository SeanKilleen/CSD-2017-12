using System;

namespace Excella.Vending.Tests
{

    public class Warehouse
    {
        private readonly ILogger _logger;
        private readonly IInventoryService _inventoryService;

        public Warehouse(IInventoryService invService, ILogger logger)
        {
            _logger = logger;
            _inventoryService = invService;
        }
        public void PlaceOrder(string sku)
        {
            if (_inventoryService.ItemInStock(sku))
            {
                _logger.Log($"Purchasing {sku}");
            }
            else
            {
                _logger.Log($"Uh oh -- {sku} is out of stock!");
                throw new Exception("Not in stock!");
            }

        }
    }

    public interface ILogger
    {
        void Log(string message);
    }
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            // this can do anything, doesn't matter
            Console.WriteLine(message);
        }
    }

    public interface IInventoryService
    {
        bool ItemInStock(string SKU);
    }
    public class InventoryService : IInventoryService
    {
        public bool ItemInStock(string SKU)
        {
            // this can be any code, doesn't matter.
            return SKU.StartsWith("1");
        }
    }
}
