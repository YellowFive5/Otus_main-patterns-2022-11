#region Usings

using System.Numerics;
using FluentAssertions;
using NUnit.Framework;

#endregion

namespace Move.Tests
{
    public class WhenMovingObjects : TestBase
    {
        [TestCase(12, 5, -7, 3, 5, 8)]
        public void ObjectMoves(int posX, int posY, int velX, int velY, int expectedPosX, int expectedPosY)
        {
            var objectToMove = new SpaceShip
                               {
                                   Position = new Vector2(posX, posY),
                                   Velocity = new Vector2(velX, velY) // init only
                               };
            var mover = new Move(objectToMove);

            mover.Execute();

            objectToMove.Position.Should().Be(new Vector2(expectedPosX, expectedPosY));
        }
    }
}