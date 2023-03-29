#region Usings

using System;
using System.Collections.Concurrent;
using System.Numerics;
using Command;
using Exceptions;
using Factory;
using FluentAssertions;
using MessageBroker;
using Moq;
using Move;
using NUnit.Framework;

#endregion

namespace Interpreter.Tests
{
    public class WhenInterpretingOrder : TestBase
    {
        [Test]
        public void ObjectMovesIfInScopeAfterOrderInterpreted()
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(10, 5));

            var ioc = new IoC();

            ioc.Resolve<ICommand>("Scopes.New", "Object_1")
               .Execute();
            ioc.Resolve<ICommand>("Scopes.Current", "Object_1")
               .Execute();
            ioc.Resolve<ICommand>("IoC.Register",
                                  "Objects.Movable_1",
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
                              ObjectId = "Objects.Movable_1",
                              OperationId = "Commands.MoveWithVelocity",
                              ArgsJson = "5"
                          };

            server.OnMessageReceived(message);

            objectToMove.VerifySet(o => o.Position = new Vector2(15, 10));
        }

        [Test]
        public void ObjectNotMovesIfNotInScopeAfterOrderInterpreted()
        {
            var objectToMove = new Mock<IMovable>();
            objectToMove.SetupGet(o => o.Position).Returns(new Vector2(10, 5));

            var ioc = new IoC();

            ioc.Resolve<ICommand>("Scopes.New", "Object_1")
               .Execute();
            ioc.Resolve<ICommand>("Scopes.Current", "Object_1")
               .Execute();
            ioc.Resolve<ICommand>("IoC.Register",
                                  "Objects.Movable_1",
                                  (Func<object[], object>)(_ => objectToMove.Object)
                                 ).Execute();
            ioc.Resolve<ICommand>("IoC.Register",
                                  "Commands.MoveWithVelocity",
                                  (Func<object[], object>)(args => new MoveWithVelocityCommand((IMovable)args[0], (Vector2)args[1]))
                                 ).Execute();
            ioc.Resolve<ICommand>("Scopes.New", "Object_2")
               .Execute();
            ioc.Resolve<ICommand>("Scopes.Current", "Object_2")
               .Execute();

            var server = new Server(ioc, new ConcurrentQueue<ICommand>(), Logger.Object);
            server.RunMultithreadCommands();

            var message = new Message
                          {
                              GameId = 1,
                              ObjectId = "Objects.Movable_1",
                              OperationId = "Commands.MoveWithVelocity",
                              ArgsJson = "5"
                          };

            Action act = () => server.OnMessageReceived(message);

            act.Should()
               .Throw<Exception>()
               .WithMessage("No operation with key Objects.Movable_1");
            objectToMove.VerifySet(o => o.Position = new Vector2(15, 10), Times.Never);
        }
    }
}