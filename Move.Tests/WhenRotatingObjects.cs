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
            var mover = new Rotate(objectToRotate);

            Action act = () => mover.Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Can't rotate object");
        }
    }
}