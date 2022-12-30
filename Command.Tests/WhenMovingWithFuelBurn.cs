#region Usings

using System;
using System.Numerics;
using FluentAssertions;
using Moq;
using Move;
using NUnit.Framework;

#endregion

namespace Command.Tests
{
    public class WhenMovingWithFuelBurn
    {
        [Test]
        public void ObjectMovesWithFuelBurnWhenEnoughFuel()
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(12, 5));
            objectToMove.SetupGet(o => o.Velocity).Returns(new Vector2(-7, 3));
            var objectToMoveAndBurn = objectToMove.As<IFuelBurnable>();
            objectToMoveAndBurn.SetupGet(o => o.FuelLevel).Returns(100);
            objectToMoveAndBurn.SetupGet(o => o.FuelConsumption).Returns(5);
            var checkFuelCommand = new CheckFuelCommand(objectToMoveAndBurn.Object);
            var moveCommand = new MoveCommand(objectToMove.Object);
            var burnFuelCommand = new BurnFuelCommand(objectToMoveAndBurn.Object);
            var moveAndBurnMacroCommand = new MacroCommand(checkFuelCommand, moveCommand, burnFuelCommand);

            moveAndBurnMacroCommand.Execute();

            objectToMoveAndBurn.VerifyGet(o => o.FuelLevel);
            objectToMoveAndBurn.VerifyGet(o => o.FuelConsumption);
            objectToMove.VerifySet(o => o.Position = new Vector2(5, 8));
            objectToMoveAndBurn.VerifySet(o => o.FuelLevel = 95);
        }

        [Test]
        public void ObjectNotMovesWhenNotEnoughFuel()
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(12, 5));
            objectToMove.SetupGet(o => o.Velocity).Returns(new Vector2(-7, 3));
            var objectToMoveAndBurn = objectToMove.As<IFuelBurnable>();
            objectToMoveAndBurn.SetupGet(o => o.FuelLevel).Returns(2);
            objectToMoveAndBurn.SetupGet(o => o.FuelConsumption).Returns(5);
            var checkFuelCommand = new CheckFuelCommand(objectToMoveAndBurn.Object);
            var moveCommand = new MoveCommand(objectToMove.Object);
            var burnFuelCommand = new BurnFuelCommand(objectToMoveAndBurn.Object);
            var moveAndBurnMacroCommand = new MacroCommand(checkFuelCommand, moveCommand, burnFuelCommand);

            Action act = () => moveAndBurnMacroCommand.Execute();

            act.Should().Throw<CommandException>();
            objectToMoveAndBurn.VerifyGet(o => o.FuelLevel);
            objectToMoveAndBurn.VerifyGet(o => o.FuelConsumption);
            objectToMove.VerifySet(o => o.Position = It.IsAny<Vector2>(), Times.Never);
            objectToMoveAndBurn.VerifySet(o => o.FuelLevel = It.IsAny<int>(), Times.Never);
        }
    }
}