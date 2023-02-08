#region Usings

using System;
using System.Collections.Concurrent;
using System.Numerics;
using Command;
using Exceptions;
using Factory;
using Moq;
using Move;
using NUnit.Framework;

#endregion

namespace MessageBroker.Tests
{
    public class WhenServerReceivesMessage : TestBase
    {
        [Test]
        public void ObjectMovesAfterServerReceivedMessage()
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(12, 5));

            var ioc = new IoC();
            ioc.Resolve<ICommand>("IoC.Register",
                                  "Objects.Movable_548",
                                  (Func<object[], object>)(_ => objectToMove.Object)
                                 ).Execute();
            ioc.Resolve<ICommand>("IoC.Register",
                                  "Commands.MoveWithVelocity",
                                  (Func<object[], object>)(args => new MoveWithVelocityCommand((IMovable)args[0], (Vector2)args[1]))
                                 ).Execute();

            var server = new Server(ioc, new ConcurrentQueue<ICommand>(), Logger.Object);
            server.RunMultithreadCommands();

            var message = new Message
                          {
                              GameId = 1,
                              ObjectId = "Objects.Movable_548",
                              OperationId = "Commands.MoveWithVelocity",
                              ArgsJson = "2"
                          };

            server.OnMessageReceived(message);

            objectToMove.VerifySet(o => o.Position = new Vector2(14, 7));
        }
    }
}