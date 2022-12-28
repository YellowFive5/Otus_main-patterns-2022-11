#region Usings

using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Exceptions.Tests
{
    public class WhenExecutingCommands : TestBase
    {
        [Test]
        public void LogCommandExecutes()
        {
            var logCommand = new LogCommand();

            logCommand.LogMessage.Should().Be("No message");

            logCommand.Execute();

            logCommand.LogMessage.Should().Be("New logged message");
        }

        [Test]
        public void RetryCommandExecutes()
        {
            var failedCommand = new Mock<ICommand>();
            var retryCommand = new RetryCommand(failedCommand.Object);

            retryCommand.Execute();

            failedCommand.Verify(fc => fc.Execute(), Times.Exactly(1));
        }
    }
}