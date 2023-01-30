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
            var queue = new ConcurrentQueue<ICommand>();
            queue.Enqueue(testCommand1.Object);
            queue.Enqueue(hardStopCommand);
            queue.Enqueue(testCommand2.Object);
            var server = new Server(queue, Logger.Object);
            
            server.RunMultithreadCommands();
            
            testCommand1.Verify(fc => fc.Execute(), Times.Once);
            testCommand2.Verify(fc => fc.Execute(), Times.Never);
            server.StopMultithread.Should().BeTrue();
        }
    }
}