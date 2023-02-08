#region Usings

using System;
using System.Collections.Concurrent;
using Command;

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
            if (!scopesCollection.TryAdd(scopeName, new Scope(scopeName)))
            {
                throw new Exception($"Scope {scopeName} already registered");
            }
        }
    }
}