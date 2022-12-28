#region Usings

using System.Collections.Generic;

#endregion

namespace Exceptions
{
    public class LogExceptionHandler : IExceptionHandler
    {
        private readonly Queue<ICommand> commands;

        public LogExceptionHandler(Queue<ICommand> commands)
        {
            this.commands = commands;
        }

        public void Handle()
        {
            commands.Enqueue(new LogCommand());
        }
    }
}