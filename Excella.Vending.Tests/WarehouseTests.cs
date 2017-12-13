using System;
using Moq;
using NUnit.Framework;

namespace Excella.Vending.Tests
{
    public class WarehouseTests
    {
        private Warehouse _warehouse;
        private Mock<IInventoryService> _mockInventoryService;
        private Mock<ILogger> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockInventoryService = new Mock<IInventoryService>();
            _mockLogger = new Mock<ILogger>();
            _warehouse = new Warehouse(_mockInventoryService.Object, _mockLogger.Object);
        }

        [Test]
        public void PlaceOrder_SKUInStock_LogsWithSKUNumber()
        {
            _mockInventoryService.Setup(x => x.ItemInStock(It.IsAny<string>())).Returns(true);
            _warehouse.PlaceOrder("1234");

            _mockLogger.Verify(x=>x.Log("Purchasing 1234"), Times.Once);
        }

        [Test]
        public void PlaceOrder_SKUOutofStock_ExpectException()
        {
            _mockInventoryService.Setup(x => x.ItemInStock(It.IsAny<string>())).Returns(false);

            Assert.Throws<Exception>(() => _warehouse.PlaceOrder("blahblah"));

        }

        // TODO: PlaceOrder_WhenItemOutOfStock_LogsOutOfStockMessageWithSKU()
        // TODO: PlaceOrder_WhenItemOutOfStock_DoesntLogPurchasingMessage()
        // TODO: PlaceOrder_WhenItemInStock_LogsInStockMessageWithSKU()
    }
}