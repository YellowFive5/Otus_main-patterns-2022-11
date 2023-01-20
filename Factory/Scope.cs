#region Usings

using System.Collections.Generic;

#endregion

namespace Factory
{
    public class Scope
    {
        public Scope(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public IDictionary<string, object> Dependencies { get; } = new Dictionary<string, object>();
    }
}