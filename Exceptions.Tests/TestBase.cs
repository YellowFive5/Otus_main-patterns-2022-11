#region Usings

using Moq;
using NUnit.Framework;

#endregion

namespace Exceptions.Tests
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