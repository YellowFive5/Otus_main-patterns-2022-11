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
            try
            {
                game.Value.TryDequeue(out var command);
                // move to another queue or some another logic 
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