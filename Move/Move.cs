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
            toMove.Position += toMove.Velocity;
        }
    }
}