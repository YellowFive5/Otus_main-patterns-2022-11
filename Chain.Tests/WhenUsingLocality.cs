#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using Command;
using Exceptions;
using FluentAssertions;
using Moq;
using Move;
using NUnit.Framework;

#endregion

namespace Chain.Tests
{
    public class WhenUsingLocality : TestBase
    {
        [Test]
        public void TwoNearObjectsCollisions()
        {
            var object1 = new Mock<IMovable>();
            object1.SetupGet(o => o.Position).Returns(new Vector2(1, 1));
            var object2 = new Mock<IMovable>();
            object2.SetupGet(o => o.Position).Returns(new Vector2(2, 2));
            var object3 = new Mock<IMovable>();
            object3.SetupGet(o => o.Position).Returns(new Vector2(10, 10));
            var gameObjects = new List<IMovable>
                              {
                                  object1.Object,
                                  object2.Object,
                                  object3.Object
                              };
            var queue = new ConcurrentQueue<ICommand>();
            var server = new Server(Ioc.Object, queue, Logger.Object);
            var stopCommand = new Mock<ICommand>();
            var mre = new ManualResetEvent(false);
            stopCommand.Setup(c => c.Execute()).Callback(() => mre.Set());

            var defineCollisionsCommand = new DefineCollisionsCommand(server, gameObjects);
            queue.Enqueue(defineCollisionsCommand);
            queue.Enqueue(stopCommand.Object);

            server.RunMultithreadCommands();

            mre.WaitOne(TimeSpan.FromSeconds(5)).Should().BeTrue();
            server.CollisionObjects.Count.Should().Be(2);
            server.CollisionObjects.ElementAt(0).Position.X.Should().Be(1);
            server.CollisionObjects.ElementAt(0).Position.Y.Should().Be(1);
            server.CollisionObjects.ElementAt(1).Position.X.Should().Be(2);
            server.CollisionObjects.ElementAt(1).Position.Y.Should().Be(2);
        }

        [Test]
        public void TwoFarObjectsNotCollisions()
        {
            var object1 = new Mock<IMovable>();
            object1.SetupGet(o => o.Position).Returns(new Vector2(1, 1));
            var object2 = new Mock<IMovable>();
            object2.SetupGet(o => o.Position).Returns(new Vector2(11, 11));
            var gameObjects = new List<IMovable>
                              {
                                  object1.Object,
                                  object2.Object
                              };
            var queue = new ConcurrentQueue<ICommand>();
            var server = new Server(Ioc.Object, queue, Logger.Object);
            var stopCommand = new Mock<ICommand>();
            var mre = new ManualResetEvent(false);
            stopCommand.Setup(c => c.Execute()).Callback(() => mre.Set());

            var defineCollisionsCommand = new DefineCollisionsCommand(server, gameObjects);
            queue.Enqueue(defineCollisionsCommand);
            queue.Enqueue(stopCommand.Object);

            server.RunMultithreadCommands();

            mre.WaitOne(TimeSpan.FromSeconds(5)).Should().BeTrue();
            server.CollisionObjects.Count.Should().Be(0);
        }

        [Test]
        public void TwoNearObjectsCollisionsInMultipleLocalities()
        {
            var object1 = new Mock<IMovable>();
            object1.SetupGet(o => o.Position).Returns(new Vector2(9, 9));
            var object2 = new Mock<IMovable>();
            object2.SetupGet(o => o.Position).Returns(new Vector2(11, 11));
            var gameObjects = new List<IMovable>
                              {
                                  object1.Object,
                                  object2.Object,
                              };
            var queue = new ConcurrentQueue<ICommand>();
            var server = new Server(Ioc.Object, queue, Logger.Object);
            var stopCommand = new Mock<ICommand>();
            var mre = new ManualResetEvent(false);
            stopCommand.Setup(c => c.Execute()).Callback(() => mre.Set());

            var localitiesNumber = 3;
            var defineCollisionsCommand = new DefineCollisionsCommand(server, gameObjects, localitiesNumber);
            queue.Enqueue(defineCollisionsCommand);
            queue.Enqueue(stopCommand.Object);

            server.RunMultithreadCommands();

            mre.WaitOne(TimeSpan.FromSeconds(5)).Should().BeTrue();
            server.CollisionObjects.Count.Should().Be(2);
            server.CollisionObjects.ElementAt(0).Position.X.Should().Be(9);
            server.CollisionObjects.ElementAt(0).Position.Y.Should().Be(9);
            server.CollisionObjects.ElementAt(1).Position.X.Should().Be(11);
            server.CollisionObjects.ElementAt(1).Position.Y.Should().Be(11);
        }
    }
}