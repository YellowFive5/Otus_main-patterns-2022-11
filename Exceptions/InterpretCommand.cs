#region Usings

using System.Collections.Concurrent;
using System.Collections.Generic;
using Command;
using Factory;
using MessageBroker;
using Move;

#endregion

namespace Exceptions
{
    public class InterpretCommand : ICommand
    {
        private readonly Message message;
        private readonly Dictionary<int, ConcurrentQueue<ICommand>> games;
        private readonly IResolvable ioc;

        public InterpretCommand(Message message, Dictionary<int, ConcurrentQueue<ICommand>> games, IResolvable ioc)
        {
            this.message = message;
            this.games = games;
            this.ioc = ioc;
        }

        public void Execute()
        {
            var gameQueue = games[message.GameId];
            var gameObject = ioc.Resolve<IMovable>(message.ObjectId);
            // todo setup velocity ?
            var command = ioc.Resolve<ICommand>(message.OperationId, gameObject);
            gameQueue.Enqueue(command);
        }
    }
}