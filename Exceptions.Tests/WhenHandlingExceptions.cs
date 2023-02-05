#region Usings

using System.Collections.Generic;
using System.Linq;
using Command;
using Exceptions.Commands;
using Exceptions.Handlers;
using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Exceptions.Tests
{
    public class WhenHandlingExceptions : TestBase
    {
        [Test]
        public void LogExceptionHandlerEnqueuesLogCommand()
        {
            var server = new Server(new Queue<ICommand>(), Logger.Object);
            var handler = new LogExceptionHandler(server.Commands, Logger.Object, "Message to log");

            server.Commands.Should().BeEmpty();

            handler.Handle();

            server.Commands.OfType<LogCommand>().Count().Should().Be(1);
        }

        [Test]
        public void RetryExceptionHandlerEnqueuesRetryCommand()
        {
            var server = new Server(new Queue<ICommand>(), Logger.Object);
            var handler = new RetryExceptionHandler(server.Commands, new Mock<ICommand>().Object);

            server.Commands.Should().BeEmpty();

            handler.Handle();

            server.Commands.OfType<RetryCommand>().Count().Should().Be(1);
        }

        [Test]
        public void DoubleRetryExceptionHandlerEnqueuesDoubleRetryCommand()
        {
            var server = new Server(new Queue<ICommand>(), Logger.Object);
            var handler = new DoubleRetryExceptionHandler(server.Commands, new Mock<ICommand>().Object);

            server.Commands.Should().BeEmpty();

            handler.Handle();

            server.Commands.OfType<DoubleRetryCommand>().Count().Should().Be(1);
        }
    }
}