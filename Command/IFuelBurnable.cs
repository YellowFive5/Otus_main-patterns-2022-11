﻿namespace Command
{
    public interface IFuelBurnable
    {
        public int FuelLevel { get; set; }
        public int FuelConsumption { get; }
    }
}