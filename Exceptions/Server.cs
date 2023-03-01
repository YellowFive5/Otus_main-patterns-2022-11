#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command;
using Exceptions.Commands;
using Exceptions.Handlers;
using Exceptions.State;
using Factory;
using MessageBroker;

#endregion

namespace Exceptions
{
    public class Server
    {
        #region Simple

        public Queue<ICommand> Commands { get; }
        private readonly ILogger logger;
        private readonly ServerState state;

        public Server(Queue<ICommand> commands, ILogger logger)
        {
            Commands = commands;
            this.logger = logger;
            state = new DefaultState(this);
        }

        public void RunCommandsTillFirstException()
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
                    return;
                }
            }
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
                try
                {
                    command.Execute();
                }
                catch (Exception e)
                {
                    switch (command)
                    {
                        case DoubleRetryCommand:
                            new LogExceptionHandler(Commands, logger, e.Message).Handle();
                            break;
                        case RetryCommand:
                            new DoubleRetryExceptionHandler(Commands, command).Handle();
                            break;
                        default:
                            new RetryExceptionHandler(Commands, command).Handle();
                            break;
                    }
                }
            }
        }

        #endregion

        #region Multithreading

        public Dictionary<int, ConcurrentQueue<ICommand>> Games { get; }
        private readonly IResolvable ioc;

        public Server(IResolvable ioc, ConcurrentQueue<ICommand> gameCommands, ILogger logger)
        {
            Games = new Dictionary<int, ConcurrentQueue<ICommand>> { { 1, gameCommands } };
            this.ioc = ioc;
            this.logger = logger;
            state = new DefaultState(this);
        }

        public bool HardStopped { get; set; }
        public bool SoftStopped { get; set; }

        public void RunMultithreadCommands()
        {
            foreach (var game in Games)
            {
                Task.Factory.StartNew(() =>
                                      {
                                          while (!HardStopped || (!SoftStopped && !game.Value.Any()))
                                          {
                                              state.Handle(game);
                                          }
                                      });
            }
        }

        #endregion

        public void OnMessageReceived(Message message)
        {
            new InterpretCommand(message, Games, ioc)
                .Execute();
        }

        public void OnAuthorizedMessageReceived(Message message)
        {
            new InterpretAuthorizedCommand(message, Games, ioc)
                .Execute();
        }
    }
}