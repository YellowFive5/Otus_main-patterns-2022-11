#region Usings

using System.Collections.Generic;
using Command;
using Exceptions.Commands;

#endregion

namespace Exceptions.Handlers
{
    public class RetryExceptionHandler : IExceptionHandler
    {
        private readonly Queue<ICommand> commands;
        private readonly ICommand commandToRetry;

        public RetryExceptionHandler(Queue<ICommand> commands, ICommand commandToRetry)
        {
            this.commands = commands;
            this.commandToRetry = commandToRetry;
        }

        public void Handle()
        {
            commands.Enqueue(new RetryCommand(commandToRetry));
        }
    }
}