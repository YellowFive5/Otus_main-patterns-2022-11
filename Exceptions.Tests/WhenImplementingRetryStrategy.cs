#region Usings

using System;
using Moq;
using NUnit.Framework;

#endregion

namespace Exceptions.Tests
{
    public class WhenImplementingRetryStrategy : TestBase
    {
        [Test]
        public void CommandsRunsWhenNoException()
        {
            var succeededCommand1 = new Mock<ICommand>();
            var succeededCommand2 = new Mock<ICommand>();
            var server = new Server();
            server.Commands.Enqueue(succeededCommand1.Object);
            server.Commands.Enqueue(succeededCommand2.Object);

            server.RunCommandsWithSingleRetryAndLog();

            succeededCommand1.Verify(c => c.Execute(), Times.Once);
            succeededCommand2.Verify(c => c.Execute(), Times.Once);
        }

        [Test]
        public void CommandsRunsWithSingleRetryAndLog()
        {
            var succeededCommand = new Mock<ICommand>();
            var failedCommand = new Mock<ICommand>();
            failedCommand.Setup(fc => fc.Execute()).Throws<Exception>();
            var server = new Server();
            server.Commands.Enqueue(succeededCommand.Object);
            server.Commands.Enqueue(failedCommand.Object);

            server.RunCommandsWithSingleRetryAndLog();

            succeededCommand.Verify(c => c.Execute(), Times.Once);
            failedCommand.Verify(c => c.Execute(), Times.Exactly(2));
        }
    }
}