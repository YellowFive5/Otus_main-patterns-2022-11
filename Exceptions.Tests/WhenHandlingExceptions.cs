#region Usings

using System.Linq;
using FluentAssertions;
using NUnit.Framework;

#endregion

namespace Exceptions.Tests
{
    public class WhenHandlingExceptions : TestBase
    {
        [Test]
        public void LogExceptionHandlerEnqueuesLogCommand()
        {
            var server = new Server();
            var handler = new LogExceptionHandler(server.Commands);

            server.Commands.Should().BeEmpty();

            handler.Handle();

            server.Commands.OfType<LogCommand>().Count().Should().Be(1);
        }
    }
}