#region Usings

using System;
using Command;
using Exceptions.Commands;
using FluentAssertions;
using Moq;
using Move;
using NUnit.Framework;

#endregion

namespace Factory.Tests
{
    public class WhenResolvingIoC : TestBase
    {
        [TestCase(null)]
        [TestCase("NoOperationWithThisKey")]
        public void ErrorThrowsWhenOperationKeyNotFound(string operationName)
        {
            var ioc = new IoC();

            Action act = () => ioc.Resolve<object>(operationName);

            act.Should()
               .Throw<Exception>()
               .WithMessage($"No operation with key {operationName}");
        }

        [Test]
        public void MoveCommandRegistersAndResolves()
        {
            var ioc = new IoC();
            var objectToMove = new Mock<IMovable>();
            var objectToMoveAndBurn = objectToMove.As<IFuelBurnable>();

            ioc.Resolve<ICommand>("IoC.Register",
                                  "Move",
                                  (Func<object[], object>)(args => new MacroCommand(new CheckFuelCommand(args[0].As<IFuelBurnable>()),
                                                                                    new MoveCommand(args[0].As<IMovable>()),
                                                                                    new BurnFuelCommand(args[0].As<IFuelBurnable>())))
                                 )
               .Execute();

            var moveCommand = ioc.Resolve<ICommand>("Move", objectToMoveAndBurn);

            moveCommand.Should().BeOfType<MacroCommand>();
        }
    }
}