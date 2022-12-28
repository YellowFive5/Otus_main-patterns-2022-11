#region Usings

using System.Collections.Generic;

#endregion

namespace Exceptions
{
    public class Server
    {
        public Queue<ICommand> Commands { get; } = new();
    }
}