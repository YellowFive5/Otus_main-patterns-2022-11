#region Usings

using System;
using System.Collections.Concurrent;
using System.Threading;
using Exceptions;
using Exceptions.Commands;
using FluentAssertions;
using Moq;
using NUnit.Framework;

#endregion

namespace Multithreading.Tests
{
    public class WhenRunMultithreadedQueue : TestBase
    {
        [Test]
        public void MultithreadExecutionRunning()
        {
            var testCommand = new Mock<ICommand>();
            var mre = new ManualResetEvent(false);
            testCommand.Setup(c => c.Execute()).Callback(() => mre.Set());
            var queue = new ConcurrentQueue<ICommand>();
            queue.Enqueue(testCommand.Object);
            var server = new Server(queue, Logger.Object);

            server.RunMultithreadCommands();

            mre.WaitOne(TimeSpan.FromSeconds(1)).Should().BeTrue();
        }

        [Test]
        public void ExecutionStopsAfterHardStopCommand()
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
            var server = new Server(queue, Logger.Object);

            server.RunMultithreadCommands();

            mre.WaitOne(TimeSpan.FromSeconds(1)).Should().BeTrue();
            testCommand1.Verify(fc => fc.Execute(), Times.Once);
            testCommand2.Verify(fc => fc.Execute(), Times.Never);
            server.MultithreadCommands.Should().NotBeEmpty();
            server.HardStopped.Should().BeTrue();
        }

        [Test]
        public void ExecutionStopsAfterAllCommandsCompleteAfterSoftStopCommand()
        {
            var testCommand1 = new Mock<ICommand>();
            var mre1 = new ManualResetEvent(false);
            testCommand1.Setup(c => c.Execute()).Callback(() => mre1.Set());
            var testCommand2 = new Mock<ICommand>();
            var mre2 = new ManualResetEvent(false);
            testCommand2.Setup(c => c.Execute()).Callback(() => mre2.Set());
            var softStopCommand = new SoftStopCommand();
            var queue = new ConcurrentQueue<ICommand>();
            queue.Enqueue(testCommand1.Object);
            queue.Enqueue(softStopCommand);
            queue.Enqueue(testCommand2.Object);
            var server = new Server(queue, Logger.Object);

            server.RunMultithreadCommands();

            mre1.WaitOne(TimeSpan.FromSeconds(1)).Should().BeTrue();
            testCommand1.Verify(fc => fc.Execute(), Times.Once);
            mre2.WaitOne(TimeSpan.FromSeconds(1)).Should().BeTrue();
            testCommand2.Verify(fc => fc.Execute(), Times.Once);
            server.MultithreadCommands.Should().BeEmpty();
            server.SoftStopped.Should().BeTrue();
        }
    }
}