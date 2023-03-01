﻿#region Usings

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Command;
using Exceptions;
using FluentAssertions;
using Moq;
using Multithreading;
using NUnit.Framework;

#endregion

namespace State.Tests
{
    public class WhenChangingServerState : TestBase
    {
        [Test]
        public void CommandsExecutionStopsAfterHardStopCommand()
        {
            var testCommand1 = new Mock<ICommand>();
            var testCommand2 = new Mock<ICommand>();
            var hardStopCommand = new HardStopCommand();
            var mre = new ManualResetEvent(false);
            testCommand1.Setup(c => c.Execute()).Callback(() => mre.Set());
            var queue = new ConcurrentQueue<ICommand>();
            queue.Enqueue(testCommand1.Object);
            queue.Enqueue(hardStopCommand);
            queue.Enqueue(testCommand2.Object);
            var server = new Server(Ioc.Object, queue, Logger.Object);

            server.RunMultithreadCommands();

            mre.WaitOne(TimeSpan.FromSeconds(2)).Should().BeTrue();
            testCommand1.Verify(fc => fc.Execute(), Times.Once);
            testCommand2.Verify(fc => fc.Execute(), Times.Never);
            server.Games.First().Value.Should().NotBeEmpty();
        }
    }
}