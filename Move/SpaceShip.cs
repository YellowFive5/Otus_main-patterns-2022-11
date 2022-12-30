#region Usings

using System.Numerics;
using Command;

#endregion

namespace Move
{
    public class SpaceShip : IMovable, IRotatable, IFuelBurnable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; }

        public int Direction { get; set; }
        public int AngularVelocity { get; }
        public int Directions { get; }

        public int FuelLevel { get; set; }
        public int FuelConsumption { get; }
    }
}