#region Usings

#endregion

namespace Exceptions.Commands
{
    public class DoubleRetryCommand : ICommand
    {
        private readonly ICommand commandToSecondRetry;

        public DoubleRetryCommand(ICommand commandToSecondRetry)
        {
            this.commandToSecondRetry = commandToSecondRetry;
        }

        public void Execute()
        {
            commandToSecondRetry.Execute();
        }
    }
}