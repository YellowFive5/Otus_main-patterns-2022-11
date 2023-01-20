#region Usings

using System;
using System.Collections.Generic;
using Exceptions.Commands;

#endregion

namespace Factory
{
    public class IocRegisterCommand : ICommand
    {
        private readonly IDictionary<string, object> dependencies;
        private readonly string key;
        private readonly Func<object[], object> action;

        public IocRegisterCommand(IDictionary<string, object> dependencies,
                                  string key,
                                  Func<object[], object> action)
        {
            this.dependencies = dependencies;
            this.key = key;
            this.action = action;
        }

        public void Execute()
        {
            dependencies.Add(key, action);
        }
    }
}