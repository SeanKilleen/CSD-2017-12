using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework.Internal;

namespace Excella.Vending.Tests
{
    public class MoqGuidance
    {
        public MoqGuidance()
        {
            // Instead of passing in a class, you can Mock it using its interface
            var mock = new Mock<IPaymentProcessor>();

            // You then access the object behind the mock by using mock.Object
            var theActualObject = mock.Object;
            // or: new MyClass(mock.Object);

            // You can set it to always return something
            mock.Setup(x => x.ReturnChange()).Returns(25);
            
            // You can set it to always throw an error
            mock.Setup(x => x.ProcessPayment()).Throws<InvalidOperationException>();

            // You can verify that certain methods were called or not called
            mock.Verify(x=>x.MakePayment(25), Times.Once);

            // You dont have to pass in a value -- you can use It.IsAny
            mock.Verify(x=>x.MakePayment(It.IsAny<int>()), Times.Never);

            // And you can get pretty complex if you only want to match certain cases.
            mock.Verify(x=>x.MakePayment(It.Is<int>(val => val > 5 && val < 10)), Times.AtLeastOnce);
        }
    }

    public interface IInventorySystem { }
}
