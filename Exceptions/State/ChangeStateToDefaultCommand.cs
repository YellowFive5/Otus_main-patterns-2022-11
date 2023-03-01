#region Usings

using System;
using Command;

#endregion

namespace Exceptions.State
{
    public class ChangeStateToDefaultCommand : ICommand
    {
        private readonly Server server;

        public ChangeStateToDefaultCommand(Server server)
        {
            this.server = server;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}