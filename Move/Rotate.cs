namespace Move
{
    public class Rotate
    {
        private readonly IRotatable toRotate;

        public Rotate(IRotatable toRotate)
        {
            this.toRotate = toRotate;
        }

        public void Execute()
        {
        }
    }
}