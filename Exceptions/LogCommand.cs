#region Usings

#endregion

namespace Exceptions
{
    public class LogCommand : ICommand
    {
        public string LogMessage { get; private set; } = "No message";

        public void Execute()
        {
            LogMessage = "New logged message";
        }
    }
}