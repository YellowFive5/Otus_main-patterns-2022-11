#region Usings

using System.Numerics;
using Moq;
using Move;
using NUnit.Framework;

#endregion

namespace MessageBroker.Tests
{
    public class WhenServerReceivesMessage : TestBase
    {
        [Test]
        public void ObjectMovesAfterServerReceivingMessage()
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(12, 5));
            objectToMove.SetupGet(o => o.Velocity).Returns(new Vector2(-7, 3));
            
            
            objectToMove.VerifySet(o => o.Position = new Vector2(5, 8));
        }
    }
}