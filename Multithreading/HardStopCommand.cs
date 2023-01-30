
using Exceptions;
using Exceptions.Commands;

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