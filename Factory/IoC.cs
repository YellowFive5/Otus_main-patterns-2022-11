#region Usings

using System;
using System.Collections.Concurrent;

#endregion

namespace Factory
{
    public class IoC : IResolvable
    {
        public ConcurrentDictionary<string, Scope> Scopes { get; } = new();
        public Scope CurrentScope { get; set; } = new("DefaultScope");

        public T Resolve<T>(string key, params object[] args)
        {
            if (key == "Scopes.New")
            {
                return new ScopeRegisterCommand(Scopes, args[0].ToString()) is T command
                           ? command
                           : default;
            }

            if (key == "Scopes.Current")
            {
                return new ScopeCurrentSetCommand(this, Scopes, args[0].ToString()) is T command
                           ? command
                           : default;
            }

            if (key == "IoC.Register")
            {
                return new IocRegisterCommand(CurrentScope.Dependencies,
                                              args[0] as string,
                                              args[1] as Func<object[], object>) is T command
                           ? command
                           : default;
            }

            if (key != null && CurrentScope.Dependencies.ContainsKey(key))
            {
                var function = (Func<object[], object>)CurrentScope.Dependencies[key];
                return (T)function.Invoke(args);
            }

            throw new Exception($"No operation with key {key}");
        }
    }
}