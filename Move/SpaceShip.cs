#region Usings

using System.Numerics;

#endregion

namespace Move
{
    public class SpaceShip : IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; }
    }
}