#region Usings

using FluentAssertions;
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
    }
}