#region Usings

using Command;
using Exceptions;

#endregion

namespace Multithreading
{
    public class SoftStopCommand : ICommand
    {
        public void Execute()
        {
            throw new SoftStopException();
        }
    }
}