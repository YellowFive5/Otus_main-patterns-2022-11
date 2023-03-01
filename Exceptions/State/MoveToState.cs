#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Command;

#endregion

namespace Exceptions.State
{
    public class MoveToState : ServerState
    {
        public MoveToState(Server server) : base(server)
        {
        }

        public override void Handle(KeyValuePair<int, ConcurrentQueue<ICommand>> game)
        {
            throw new NotImplementedException();
        }
    }
}