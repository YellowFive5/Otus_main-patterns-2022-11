#region Usings

using Command;
using Exceptions;

#endregion

namespace Multithreading
{
    public class HardStopCommand : ICommand
    {
        public void Execute()
        {
            throw new HardStopException();
        }
    }
}