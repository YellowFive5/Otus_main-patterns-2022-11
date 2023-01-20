#region Usings

using System;
using System.Linq;
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
                                  (Func<object[], object>)(args => new MacroCommand(new CheckFuelCommand((IFuelBurnable)args[0]),
                                                                                    new MoveCommand((IMovable)args[0]),
                                                                                    new BurnFuelCommand((IFuelBurnable)args[0])))
                                 )
               .Execute();

            var moveCommand = ioc.Resolve<ICommand>("Move", objectToMoveAndBurn.Object);

            moveCommand.Should().BeOfType<MacroCommand>();
        }

        [Test]
        public void ScopeRegisters()
        {
            var ioc = new IoC();

            ioc.Scopes.Count.Should().Be(0);

            ioc.Resolve<ICommand>("Scopes.New", "id_1")
               .Execute();

            ioc.Scopes.Count.Should().Be(1);
            ioc.Scopes.First().Value.Name.Should().Be("id_1");
        }

        [Test]
        public void ErrorThrowsWhenTryToRegisterDuplicatedScope()
        {
            var ioc = new IoC();

            ioc.Resolve<ICommand>("Scopes.New", "id_1")
               .Execute();
            Action act = () => ioc.Resolve<ICommand>("Scopes.New", "id_1")
                                  .Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Scope id_1 already registered");
        }
    }
}