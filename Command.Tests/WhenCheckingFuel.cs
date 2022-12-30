#region Usings

using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Command.Tests
{
    public class WhenCheckingFuel : TestBase
    {
        [Test]
        public void FuelChecksOkWhenFuelLevelEnoughForNextMoving()
        {
            var objectForFuelCheck = new Mock<IFuelBurnable>();
            objectForFuelCheck.SetupGet(o => o.FuelLevel).Returns(100);
            objectForFuelCheck.SetupGet(o => o.FuelConsumption).Returns(5);
            var fuelChecker = new CheckFuelCommand(objectForFuelCheck.Object);

            Action act = () => fuelChecker.Execute();

            act.Should().NotThrow();
        }

        [Test]
        public void FuelCheckThrowsWhenFuelLevelNotEnoughForNextMoving()
        {
            var objectForFuelCheck = new Mock<IFuelBurnable>();
            objectForFuelCheck.SetupGet(o => o.FuelLevel).Returns(1);
            objectForFuelCheck.SetupGet(o => o.FuelConsumption).Returns(5);
            var fuelChecker = new CheckFuelCommand(objectForFuelCheck.Object);

            Action act = () => fuelChecker.Execute();

            act.Should().Throw<NotEnoughFuelException>();
        }
    }
}