#region Usings

using Exceptions;
using Exceptions.Commands;

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