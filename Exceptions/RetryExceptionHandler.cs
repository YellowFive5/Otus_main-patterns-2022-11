#region Usings

using System.Collections.Generic;

#endregion

namespace Exceptions
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
        }
    }
}