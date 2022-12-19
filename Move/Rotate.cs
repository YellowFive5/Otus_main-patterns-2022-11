#region Usings

using System;

#endregion

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
            if (toRotate == null)
            {
                throw new Exception("Can't rotate object");
            }

            toRotate.Direction += toRotate.AngularVelocity % toRotate.Direction;
        }
    }
}