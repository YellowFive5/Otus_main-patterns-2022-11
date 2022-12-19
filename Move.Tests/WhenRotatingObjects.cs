#region Usings

using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Move.Tests
{
    public class WhenRotatingObjects : TestBase
    {
        [Test]
        public void ExceptionThrowsWhenRotatableObjectIsNull()
        {
            IRotatable objectToRotate = null;
            var rotator = new Rotate(objectToRotate);

            Action act = () => rotator.Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Can't rotate object");
        }

        [TestCase(0, 16, 8, 8)]
        public void ObjectRotates(int startDir, int angVel, int dir, int expectedDir)
        {
            var objectToRotate = new Mock<IRotatable>();
            objectToRotate.SetupGet(o => o.Direction).Returns(startDir);
            objectToRotate.SetupGet(o => o.AngularVelocity).Returns(angVel);
            objectToRotate.SetupGet(o => o.Direction).Returns(dir);
            var rotator = new Rotate(objectToRotate.Object);

            rotator.Execute();

            objectToRotate.VerifySet(o => o.Direction = expectedDir);
        }
    }
}