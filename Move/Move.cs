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
            if (toMove == null ||
                float.IsInfinity(toMove.Position.X) ||
                float.IsInfinity(toMove.Position.Y) ||
                float.IsNaN(toMove.Position.X) ||
                float.IsNaN(toMove.Position.Y))
            {
                throw new Exception("Can't get object position");
            }

            toMove.Position += toMove.Velocity;
        }
    }
}