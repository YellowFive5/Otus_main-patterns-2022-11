#region Usings

using System;
using System.Numerics;
using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Move.Tests
{
    public class WhenMovingObjects : TestBase
    {
        [TestCase(12, 5, -7, 3, 5, 8)]
        public void ObjectMoves(float posX, float posY, float velX, float velY, float expectedPosX, float expectedPosY)
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(posX, posY));
            objectToMove.SetupGet(o => o.Velocity).Returns(new Vector2(velX, velY));
            var mover = new MoveCommand(objectToMove.Object);

            mover.Execute();

            objectToMove.VerifySet(o => o.Position = new Vector2(expectedPosX, expectedPosY));
        }

        [Test]
        public void ExceptionThrowsWhenMovableObjectIsNull()
        {
            IMovable objectToMove = null;
            var mover = new MoveCommand(objectToMove);

            Action act = () => mover.Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Can't move object");
        }

        [TestCase(float.NaN, 0)]
        [TestCase(float.NegativeInfinity, 0)]
        [TestCase(float.PositiveInfinity, 0)]
        [TestCase(0, float.NaN)]
        [TestCase(0, float.NegativeInfinity)]
        [TestCase(0, float.PositiveInfinity)]
        public void ExceptionThrowsWhenCantGetPosition(float posX, float posY)
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(posX, posY));

            var mover = new MoveCommand(objectToMove.Object);

            Action act = () => mover.Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Can't get object position");
        }

        [TestCase(float.NaN, 0)]
        [TestCase(float.NegativeInfinity, 0)]
        [TestCase(float.PositiveInfinity, 0)]
        [TestCase(0, float.NaN)]
        [TestCase(0, float.NegativeInfinity)]
        [TestCase(0, float.PositiveInfinity)]
        public void ExceptionThrowsWhenCantGetVelocity(float velX, float velY)
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Velocity).Returns(new Vector2(velX, velY));
            var mover = new MoveCommand(objectToMove.Object);

            Action act = () => mover.Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Can't get object velocity");
        }
    }
}