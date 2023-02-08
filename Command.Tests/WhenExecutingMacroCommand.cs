#region Usings

using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Command.Tests
{
    public class WhenExecutingMacroCommand : TestBase
    {
        [Test]
        public void AllCommandsExecutes()
        {
            var command1 = new Mock<ICommand>();
            var command2 = new Mock<ICommand>();
            var macroCommand = new MacroCommand(command1.Object, command2.Object);

            macroCommand.Execute();

            command1.Verify(fc => fc.Execute(), Times.Once);
            command2.Verify(fc => fc.Execute(), Times.Once);
        }

        [Test]
        public void ExecutingStopsOnFailedCommand()
        {
            var command1 = new Mock<ICommand>();
            var failedCommand = new Mock<ICommand>();
            failedCommand.Setup(c => c.Execute()).Throws<Exception>();
            var command3 = new Mock<ICommand>();
            var macroCommand = new MacroCommand(command1.Object, failedCommand.Object, command3.Object);

            Action act = () => macroCommand.Execute();

            act.Should().Throw<CommandException>();
            command1.Verify(fc => fc.Execute(), Times.Once);
            failedCommand.Verify(fc => fc.Execute(), Times.Once);
            command3.Verify(fc => fc.Execute(), Times.Never);
        }
    }
}