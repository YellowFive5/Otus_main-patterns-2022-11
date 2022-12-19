#region Usings

using System.Numerics;

#endregion

namespace Move
{
    public class FotonBullet : IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; init; }
    }
}