#region Usings

using System;
using System.Collections.Generic;
using Exceptions;
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
            commandToRepeat.SetupSequence(c => c.Execute())
                           .Pass()
                           .Pass()
                           .Throws(new Exception("Some exception"));
            var commands = new Queue<ICommand>();
            var repeatableCommand = new RepeatableCommand(commandToRepeat.Object, commands);
            commands.Enqueue(repeatableCommand);
            var server = new Server(commands, new Mock<ILogger>().Object);

            server.RunCommandsTillFirstException();

            commandToRepeat.Verify(fc => fc.Execute(), Times.Exactly(3));
        }
    }
}