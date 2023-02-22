#region Usings

using System.Collections.Concurrent;
using System.Collections.Generic;
using Authorization;
using Command;
using Factory;
using MessageBroker;
using Move;

#endregion

namespace Exceptions
{
    public class InterpretAuthorizedCommand : ICommand
    {
        private readonly Message message;
        private readonly Dictionary<int, ConcurrentQueue<ICommand>> games;
        private readonly IResolvable ioc;

        public InterpretAuthorizedCommand(Message message, Dictionary<int, ConcurrentQueue<ICommand>> games, IResolvable ioc)
        {
            this.message = message;
            this.games = games;
            this.ioc = ioc;
        }

        public void Execute()
        {
            var authorizationService = ioc.Resolve<AuthorizationService>("Services.Authorization");
            if (authorizationService.CheckBattleAuthorizationTokenCorrect(message.ArgsJson))
            {
                var gameQueue = games[message.GameId];
                var gameObject = ioc.Resolve<IMovable>(message.ObjectId);
                var command = ioc.Resolve<ICommand>(message.OperationId, gameObject);
                gameQueue.Enqueue(command);
            }
        }
    }
}