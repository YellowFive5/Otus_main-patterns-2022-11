#region Usings

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
            server.State = new DefaultState(server);
        }
    }
}