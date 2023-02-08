#region Usings

using System.Numerics;
using Command;

#endregion

namespace Move
{
    public class MoveWithVelocityCommand : ICommand
    {
        private readonly IMovable toMove;
        private readonly Vector2 velocity;

        public MoveWithVelocityCommand(IMovable toMove, Vector2 velocity)
        {
            this.toMove = toMove;
            this.velocity = velocity;
        }

        public void Execute()
        {
            toMove.Position += velocity;
        }
    }
}