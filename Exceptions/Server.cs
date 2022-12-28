#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Exceptions
{
    public class Server
    {
        public Queue<ICommand> Commands { get; } = new();

        public void RunCommandsWithSingleRetryAndLog()
        {
            while (Commands.Any())
            {
                var command = Commands.Dequeue();
                try
                {
                    command.Execute();
                }
                catch (Exception)
                {
                    if (command is RetryCommand)
                    {
                        new LogExceptionHandler(Commands).Handle();
                    }
                    else
                    {
                        new RetryExceptionHandler(Commands, command).Handle();
                    }
                }
            }
        }
    }
}