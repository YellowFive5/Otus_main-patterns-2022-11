#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Command;

#endregion

namespace Exceptions.State
{
    public class DefaultState : ServerState
    {
        public DefaultState(Server server) : base(server)
        {
        }

        public override void Handle(KeyValuePair<int, ConcurrentQueue<ICommand>> game)
        {
            try
            {
                game.Value.TryDequeue(out var command);
                command?.Execute();
            }
            catch (HardStopException)
            {
                server.HardStopped = true;
            }
            catch (SoftStopException)
            {
                server.SoftStopped = true;
            }
            catch (Exception)
            {
                // continue
            }
        }
    }
}