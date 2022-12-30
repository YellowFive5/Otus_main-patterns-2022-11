#region Usings

using System;
using Exceptions.Commands;
using Move;

#endregion

namespace Command
{
    public class BurnFuelCommand : ICommand
    {
        private readonly IFuelBurnable toBurn;

        public BurnFuelCommand(IFuelBurnable toBurn)
        {
            this.toBurn = toBurn;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}