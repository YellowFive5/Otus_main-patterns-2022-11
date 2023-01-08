#region Usings

using System.Collections.Generic;
using Exceptions.Commands;

#endregion

namespace Command
{
    public class RepeatableCommand : ICommand
    {
        private readonly ICommand commandToRepeat;
        private readonly Queue<ICommand> commands;

        public RepeatableCommand(ICommand commandToRepeat, Queue<ICommand> commands)
        {
            this.commandToRepeat = commandToRepeat;
            this.commands = commands;
        }

        public void Execute()
        {
            commandToRepeat.Execute();
            commands.Enqueue(this);
        }
    }
}