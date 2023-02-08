#region Usings

using System.Numerics;
using Command;
using Move;

#endregion

namespace Factory
{
    public class SetObjectPositionCommand : ICommand
    {
        private readonly IMovable objectToMove;
        private readonly Vector2 newPosition;

        public SetObjectPositionCommand(IMovable objectToMove, Vector2 newPosition)
        {
            this.objectToMove = objectToMove;
            this.newPosition = newPosition;
        }

        public void Execute()
        {
            objectToMove.Position = newPosition;
        }
    }
}