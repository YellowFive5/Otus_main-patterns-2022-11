#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Exceptions
{
    public class Server
    {
        public Queue<ICommand> Commands { get; }
        private readonly ILogger logger;

        public Server(Queue<ICommand> commands, ILogger logger)
        {
            Commands = commands;
            this.logger = logger;
        }

        public void RunCommandsWithSingleRetryAndLog()
        {
            while (Commands.Any())
            {
                var command = Commands.Dequeue();
                try
                {
                    command.Execute();
                }
                catch (Exception e)
                {
                    if (command is RetryCommand)
                    {
                        new LogExceptionHandler(Commands, logger, e.Message).Handle();
                    }
                    else
                    {
                        new RetryExceptionHandler(Commands, command).Handle();
                    }
                }
            }
        }

        public void RunCommandsWithDoubleRetryAndLog()
        {
            while (Commands.Any())
            {
                var command = Commands.Dequeue();
                command.Execute();
            }
        }
    }
}