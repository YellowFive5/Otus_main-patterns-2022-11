#region Usings

using Command;
using MessageBroker;

#endregion

namespace Exceptions
{
    public class InterpretCommand : ICommand
    {
        private readonly Message message;

        public InterpretCommand(Message message)
        {
            this.message = message;
        }

        public void Execute()
        {
        }
    }
}