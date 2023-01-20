#region Usings

using Exceptions.Commands;

#endregion

namespace Factory
{
    public class ScopeCurrentSetCommand : ICommand
    {
        private readonly IoC ioC;
        private readonly Scope scopeToSet;

        public ScopeCurrentSetCommand(IoC ioC, Scope scopeToSet)
        {
            this.ioC = ioC;
            this.scopeToSet = scopeToSet;
        }

        public void Execute()
        {
            ioC.CurrentScope = scopeToSet;
        }
    }
}