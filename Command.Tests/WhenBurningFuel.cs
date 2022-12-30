#region Usings

using Moq;
using NUnit.Framework;

#endregion

namespace Command.Tests
{
    public class WhenBurningFuel : TestBase
    {
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