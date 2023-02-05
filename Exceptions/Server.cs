#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command;
using Exceptions.Commands;
using Exceptions.Handlers;
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

        public Server(Queue<ICommand> commands, ILogger logger)
        {
            Commands = commands;
            this.logger = logger;
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
        }

        private bool HardStopped { get; set; }
        private bool SoftStopped { get; set; }

        public void RunMultithreadCommands()
        {
            foreach (var game in Games)
            {
                Task.Factory.StartNew(() =>
                                      {
                                          while (!HardStopped || (!SoftStopped && !game.Value.Any()))
                                          {
                                              try
                                              {
                                                  game.Value.TryDequeue(out var command);
                                                  command?.Execute();
                                              }
                                              catch (HardStopException)
                                              {
                                                  HardStopped = true;
                                              }
                                              catch (SoftStopException)
                                              {
                                                  SoftStopped = true;
                                              }
                                              catch (Exception)
                                              {
                                                  // continue
                                              }
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
    }
}