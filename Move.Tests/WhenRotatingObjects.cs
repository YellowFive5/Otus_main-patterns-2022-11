#region Usings

using System;
using FluentAssertions;
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
    }
}