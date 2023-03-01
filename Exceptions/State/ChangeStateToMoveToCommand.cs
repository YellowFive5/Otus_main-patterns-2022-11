#region Usings

using System;
using Command;

#endregion

namespace Exceptions.State
{
    public class ChangeStateToMoveToCommand : ICommand
    {
        private readonly Server server;

        public ChangeStateToMoveToCommand(Server server)
        {
            this.server = server;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}