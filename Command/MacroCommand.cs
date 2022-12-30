#region Usings

using System;
using Exceptions.Commands;

#endregion

namespace Command
{
    public class MacroCommand : ICommand
    {
        private readonly ICommand[] commands;

        public MacroCommand(params ICommand[] commands)
        {
            this.commands = commands;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}