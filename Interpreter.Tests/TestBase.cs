#region Usings

using Exceptions;
using Factory;
using Moq;
using NUnit.Framework;

#endregion

namespace Interpreter.Tests
{
    public class TestBase
    {
        protected Mock<ILogger> Logger { get; set; }
        protected Mock<IResolvable> Ioc { get; set; }


        [SetUp]
        public virtual void Setup()
        {
            Logger = new Mock<ILogger>();
            Ioc = new Mock<IResolvable>();
        }
    }
}