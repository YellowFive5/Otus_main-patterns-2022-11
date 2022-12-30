#region Usings

using Exceptions.Commands;
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
    }
}