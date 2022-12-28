#region Usings

using System.Collections.Generic;
using Exceptions.Commands;

#endregion

namespace Exceptions.Handlers
{
    public class DoubleRetryExceptionHandler : IExceptionHandler
    {
        private readonly Queue<ICommand> commands;
        private readonly ICommand commandToRetry;

        public DoubleRetryExceptionHandler(Queue<ICommand> commands, ICommand commandToRetry)
        {
            this.commands = commands;
            this.commandToRetry = commandToRetry;
        }

        public void Handle()
        {
            commands.Enqueue(new DoubleRetryCommand(commandToRetry));
        }
    }
}