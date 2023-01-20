#region Usings

using System.Collections.Concurrent;
using Exceptions.Commands;

#endregion

namespace Factory
{
    public class ScopeRegisterCommand : ICommand
    {
        private readonly ConcurrentDictionary<string, Scope> scopesCollection;
        private readonly string scopeName;

        public ScopeRegisterCommand(ConcurrentDictionary<string, Scope> scopesCollection, string scopeName)
        {
            this.scopesCollection = scopesCollection;
            this.scopeName = scopeName;
        }

        public void Execute()
        {
            scopesCollection.TryAdd(scopeName, new Scope(scopeName));
        }
    }
}