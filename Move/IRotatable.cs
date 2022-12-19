namespace Move
{
    public interface IRotatable
    {
        public int Direction { get; set; }
        public int AngularVelocity { get; }
        public int Directions { get; }
    }
}