#region Usings

using System.Collections.Generic;
using Exceptions.Commands;

#endregion

namespace Exceptions.Handlers
{
    public class LogExceptionHandler : IExceptionHandler
    {
        private readonly Queue<ICommand> commands;
        private readonly ILogger logger;
        private readonly string message;

        public LogExceptionHandler(Queue<ICommand> commands, ILogger logger, string message)
        {
            this.commands = commands;
            this.logger = logger;
            this.message = message;
        }

        public void Handle()
        {
            commands.Enqueue(new LogCommand(logger, message));
        }
    }
}