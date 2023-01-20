#region Usings

using System.Collections.Generic;

#endregion

namespace Factory
{
    public class Scope
    {
        public IDictionary<string, object> Dependencies { get; } = new Dictionary<string, object>();
    }
}