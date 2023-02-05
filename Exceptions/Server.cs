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

        public ConcurrentQueue<ICommand> MultithreadCommands { get; }

        public Server(IResolvable ioc, ConcurrentQueue<ICommand> multithreadCommands, ILogger logger)
        {
            MultithreadCommands = multithreadCommands;
            this.logger = logger;
        }

        public bool HardStopped { get; private set; }
        public bool SoftStopped { get; private set; }

        public void RunMultithreadCommands()
        {
            Task.Factory.StartNew(() =>
                                  {
                                      while (!HardStopped || (!SoftStopped && !MultithreadCommands.Any()))
                                      {
                                          try
                                          {
                                              MultithreadCommands.TryDequeue(out var command);
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

        #endregion

        public void OnMessageReceived(Message message)
        {
            var command = new InterpretCommand(message);
        }
    }
}