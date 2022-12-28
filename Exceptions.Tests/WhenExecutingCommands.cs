#region Usings

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
            var logCommand = new LogCommand(Logger.Object, "Message to log");

            logCommand.Execute();

            Logger.Verify(l => l.Log("Message to log"), Times.Once);
        }

        [Test]
        public void RetryCommandExecutes()
        {
            var failedCommand = new Mock<ICommand>();
            var retryCommand = new RetryCommand(failedCommand.Object);

            retryCommand.Execute();

            failedCommand.Verify(fc => fc.Execute(), Times.Once);
        }
    }
}