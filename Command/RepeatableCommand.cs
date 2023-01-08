#region Usings

using System;
using Exceptions.Commands;

#endregion

namespace Command
{
    public class RepeatableCommand : ICommand
    {
        private readonly ICommand commandToRepeat;

        public RepeatableCommand(ICommand commandToRepeat)
        {
            this.commandToRepeat = commandToRepeat;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}