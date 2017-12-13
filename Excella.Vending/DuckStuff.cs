using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excella.Vending
{
    public abstract class Duck
    {
        public abstract void Fly();
        public abstract void Swim();
        public abstract void Float();
        public abstract void Eat();
    }

    public class RubberDuck : Duck
    {
        public override void Fly()
        {
            throw new NotImplementedException();
        }

        public override void Swim()
        {
        }

        public override void Float()
        {
        }

        public override void Eat()
        {
            throw new NotImplementedException();
        }
    }
    public class Mallard : Duck
    {
        public override void Fly() {}

        public override void Swim() { }

        public override void Float() { }

        public override void Eat() { }
    }
}
