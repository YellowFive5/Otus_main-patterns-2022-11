#region Usings

using System;
using System.Collections.Concurrent;
using Exceptions.Commands;

#endregion

namespace Factory
{
    public class ScopeCurrentSetCommand : ICommand
    {
        private readonly IoC ioC;
        private readonly ConcurrentDictionary<string, Scope> scopes;
        private readonly string scopeIdToSet;

        public ScopeCurrentSetCommand(IoC ioC, ConcurrentDictionary<string, Scope> scopes, string scopeIdToSet)
        {
            this.ioC = ioC;
            this.scopes = scopes;
            this.scopeIdToSet = scopeIdToSet;
        }

        public void Execute()
        {
            if (!scopes.TryGetValue(scopeIdToSet, out var scopeToSet))
            {
                throw new Exception($"Scope {scopeIdToSet} not registered");
            }

            ioC.CurrentScope.Value = scopeToSet;
        }
    }
}