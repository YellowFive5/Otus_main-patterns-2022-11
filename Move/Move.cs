#region Usings

using System;

#endregion

namespace Move
{
    public class Move
    {
        private readonly IMovable toMove;

        public Move(IMovable toMove)
        {
            this.toMove = toMove;
        }

        public void Execute()
        {
            if (toMove == null)
            {
                throw new Exception("Can't move object");
            }

            if (float.IsInfinity(toMove.Position.X) ||
                float.IsInfinity(toMove.Position.Y) ||
                float.IsNaN(toMove.Position.X) ||
                float.IsNaN(toMove.Position.Y))
            {
                throw new Exception("Can't get object position");
            }

            if (float.IsInfinity(toMove.Velocity.X) ||
                float.IsInfinity(toMove.Velocity.Y) ||
                float.IsNaN(toMove.Velocity.X) ||
                float.IsNaN(toMove.Velocity.Y))
            {
                throw new Exception("Can't get object velocity");
            }

            toMove.Position += toMove.Velocity;
        }
    }
}