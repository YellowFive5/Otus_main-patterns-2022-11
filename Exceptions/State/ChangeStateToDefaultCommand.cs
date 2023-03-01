#region Usings

using System;
using Command;

#endregion

namespace Exceptions.State
{
    public class ChangeStateToDefaultCommand : ICommand
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}