using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Excella.Vending.Tests
{
    [Binding]
    public class ReleaseChangeSteps
    {
        private VendingMachine _vendingMachine;
        private int _releasedChange;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _vendingMachine = new VendingMachine(new PaymentProcessor(new PaymentDao()));
        }

        [Given("I have inserted a quarter")]
        [When("I insert a quarter")]
        [When("I insert another quarter")]
        public void IHaveInsertedAQuarter()
        {
            _vendingMachine.InsertQuarter();
        }

        [When("I release the change")]
        public void ReleaseTheChange()
        {
            _vendingMachine.ReleaseChange();
            _releasedChange = _vendingMachine.GetChangeFromDispenser();
        }

        [Then("I should receive (.*) cents")]
        public void IShouldReceive(int cents)
        {
            Assert.That(_releasedChange, Is.EqualTo(cents));
        }
        

    }
}
