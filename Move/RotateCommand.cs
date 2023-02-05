#region Usings

using System;
using Command;

#endregion

namespace Move
{
    public class RotateCommand : ICommand
    {
        private readonly IRotatable toRotate;

        public RotateCommand(IRotatable toRotate)
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