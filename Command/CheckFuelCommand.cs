#region Usings

using Exceptions.Commands;

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
            if (toCheck.FuelLevel < toCheck.FuelConsumption)
            {
                throw new NotEnoughFuelException();
            }
        }
    }
}