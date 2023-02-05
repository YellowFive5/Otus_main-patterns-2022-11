#region Usings

using System;
using System.Linq;
using System.Threading.Tasks;
using Command;
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

        [Test]
        public void RegisteredScopeSetsAsCurrent()
        {
            var ioc = new IoC();

            ioc.CurrentScope.Value?.Name.Should().Be("DefaultScope");

            ioc.Resolve<ICommand>("Scopes.New", "id_1")
               .Execute();
            ioc.Resolve<ICommand>("Scopes.Current", "id_1")
               .Execute();

            ioc.CurrentScope.Value?.Name.Should().Be("id_1");
        }

        [Test]
        public void ErrorThrowsWhenTryingSetNotRegisteredScope()
        {
            var ioc = new IoC();

            Action act = () => ioc.Resolve<ICommand>("Scopes.Current", "id_1")
                                  .Execute();

            act.Should()
               .Throw<Exception>()
               .WithMessage("Scope id_1 not registered");
        }

        [Test]
        public void CurrentScopeSetsForEachThread()
        {
            var ioc = new IoC();

            ioc.Resolve<ICommand>("Scopes.New", "id_1")
               .Execute();
            ioc.Resolve<ICommand>("Scopes.New", "id_2")
               .Execute();

            Task.Factory.StartNew(() =>
                                  {
                                      ioc.Resolve<ICommand>("Scopes.Current", "id_1")
                                         .Execute();

                                      ioc.CurrentScope.Value?.Name.Should().Be("id_1");
                                  });
            Task.Factory.StartNew(() =>
                                  {
                                      ioc.Resolve<ICommand>("Scopes.Current", "id_2")
                                         .Execute();

                                      ioc.CurrentScope.Value?.Name.Should().Be("id_2");
                                  });
            ioc.CurrentScope.Value?.Name.Should().Be("DefaultScope");
        }
    }
}