#region Usings

using System;
using Exceptions.Commands;
using Move;

#endregion

namespace Command
{
    public class CheckFuelCommand : ICommand
    {
        private readonly IFuelBurnable toCheck;

        public CheckFuelCommand(IFuelBurnable toCheck)
        {
            this.toCheck = toCheck;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}