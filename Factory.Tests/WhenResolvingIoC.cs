#region Usings

using System;
using FluentAssertions;
using NUnit.Framework;

#endregion

namespace Factory.Tests
{
    public class WhenResolvingIoC : TestBase
    {
        [TestCase(null)]
        [TestCase("NoOperationWithThisKey")]
        public void ErrorThrowsWhenOperationKeyNotFound(string operationName)
        {
            var ioc = new IoC();

            Action act = () => ioc.Resolve<object>(operationName);

            act.Should()
               .Throw<Exception>()
               .WithMessage($"No operation with key {operationName}");
        }
    }
}