#region Usings

using System.Numerics;

#endregion

namespace Move
{
    public class SpaceShip : IMovable, IRotatable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; }

        public int Direction { get; set; }
        public int AngularVelocity { get; }
        public int Directions { get; }
    }
}