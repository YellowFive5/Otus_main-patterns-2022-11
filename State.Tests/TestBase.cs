#region Usings

using Exceptions;
using Moq;
using NUnit.Framework;

#endregion

namespace State.Tests
{
    public class TestBase
    {
        protected Mock<ILogger> Logger { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            Logger = new Mock<ILogger>();
        }
    }
}