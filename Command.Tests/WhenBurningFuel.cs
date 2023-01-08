#region Usings

using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Command.Tests
{
    public class WhenBurningFuel : TestBase
    {
        [Test]
        public void ExceptionThrowsWhenFuelBurnableObjectIsNull()
        {
            IFuelBurnable objectForFuelBurn = null;
            var mover = new BurnFuelCommand(objectForFuelBurn);

            Action act = () => mover.Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Can't burn object fuel");
        }

        [Test]
        public void FuelBurns()
        {
            var objectForFuelBurn = new Mock<IFuelBurnable>();
            objectForFuelBurn.SetupGet(o => o.FuelLevel).Returns(100);
            objectForFuelBurn.SetupGet(o => o.FuelConsumption).Returns(5);
            var fuelBurner = new BurnFuelCommand(objectForFuelBurn.Object);

            fuelBurner.Execute();

            objectForFuelBurn.VerifySet(o => o.FuelLevel = 95);
        }
    }
}