#region Usings

using System;

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
            if (toBurn == null)
            {
                throw new Exception("Can't burn object fuel");
            }

            toBurn.FuelLevel -= toBurn.FuelConsumption;
        }
    }
}