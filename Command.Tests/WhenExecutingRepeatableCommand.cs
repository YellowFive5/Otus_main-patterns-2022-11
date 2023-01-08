#region Usings

using Exceptions.Commands;
using Moq;
using NUnit.Framework;

#endregion

namespace Command.Tests
{
    public class WhenExecutingRepeatableCommand : TestBase
    {
        [Test]
        public void RepeatableCommandRepeatExecuting()
        {
            var commandToRepeat = new Mock<ICommand>();
            var repeatableCommand = new RepeatableCommand(commandToRepeat.Object);

            repeatableCommand.Execute();

            commandToRepeat.Verify(fc => fc.Execute(), Times.AtLeast(2));
        }
    }
}