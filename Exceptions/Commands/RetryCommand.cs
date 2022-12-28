#region Usings

#endregion

namespace Exceptions.Commands
{
    public class RetryCommand : ICommand
    {
        private readonly ICommand commandToRetry;

        public RetryCommand(ICommand commandToRetry)
        {
            this.commandToRetry = commandToRetry;
        }

        public void Execute()
        {
            commandToRetry.Execute();
        }
    }
}