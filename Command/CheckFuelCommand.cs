#region Usings

using System;

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
            if (toCheck == null)
            {
                throw new Exception("Can't check object fuel");
            }

            if (toCheck.FuelLevel < toCheck.FuelConsumption)
            {
                throw new NotEnoughFuelException();
            }
        }
    }
}