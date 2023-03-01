#region Usings

using System.Collections.Concurrent;
using System.Collections.Generic;
using Command;

#endregion

namespace Exceptions.State
{
    public abstract class ServerState
    {
        protected readonly Server server;

        protected ServerState(Server server)
        {
            this.server = server;
        }

        public abstract void Handle(KeyValuePair<int, ConcurrentQueue<ICommand>> game);
    }
}