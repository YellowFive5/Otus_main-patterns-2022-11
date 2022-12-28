#region Usings

using System;
using System.Collections.Generic;
using Exceptions.Commands;
using Moq;
using NUnit.Framework;

#endregion

namespace Exceptions.Tests
{
    public class WhenImplementingRetryStrategy : TestBase
    {
        [Test]
        public void CommandsRunsWhenNoExceptionOnSingleRetry()
        {
            var succeededCommand1 = new Mock<ICommand>();
            var succeededCommand2 = new Mock<ICommand>();
            var commands = new Queue<ICommand>(new[] { succeededCommand1.Object, succeededCommand2.Object });
            var server = new Server(commands, Logger.Object);

            server.RunCommandsWithSingleRetryAndLog();

            succeededCommand1.Verify(c => c.Execute(), Times.Once);
            succeededCommand2.Verify(c => c.Execute(), Times.Once);
        }

        [Test]
        public void CommandsRunsWithSingleRetryAndLog()
        {
            var succeededCommand = new Mock<ICommand>();
            var failedCommand = new Mock<ICommand>();
            failedCommand.Setup(fc => fc.Execute()).Throws(new Exception("Fails exception"));
            var commands = new Queue<ICommand>(new[] { succeededCommand.Object, failedCommand.Object });
            var server = new Server(commands, Logger.Object);

            server.RunCommandsWithSingleRetryAndLog();

            succeededCommand.Verify(c => c.Execute(), Times.Once);
            failedCommand.Verify(c => c.Execute(), Times.Exactly(2));
            Logger.Verify(l => l.Log("Fails exception"));
        }

        [Test]
        public void CommandsRunsWhenNoExceptionOnDoubleRetry()
        {
            var succeededCommand1 = new Mock<ICommand>();
            var succeededCommand2 = new Mock<ICommand>();
            var commands = new Queue<ICommand>(new[] { succeededCommand1.Object, succeededCommand2.Object });
            var server = new Server(commands, Logger.Object);

            server.RunCommandsWithDoubleRetryAndLog();

            succeededCommand1.Verify(c => c.Execute(), Times.Once);
            succeededCommand2.Verify(c => c.Execute(), Times.Once);
        }

        [Test]
        public void CommandsRunsWithDoubleRetryAndLog()
        {
            var succeededCommand = new Mock<ICommand>();
            var failedCommand = new Mock<ICommand>();
            failedCommand.Setup(fc => fc.Execute()).Throws(new Exception("Fails exception"));
            var commands = new Queue<ICommand>(new[] { succeededCommand.Object, failedCommand.Object });
            var server = new Server(commands, Logger.Object);

            server.RunCommandsWithDoubleRetryAndLog();

            succeededCommand.Verify(c => c.Execute(), Times.Once);
            failedCommand.Verify(c => c.Execute(), Times.Exactly(3));
            Logger.Verify(l => l.Log("Fails exception"));
        }
    }
}