#region Usings

using System;
using System.Collections.Concurrent;

#endregion

namespace Factory
{
    public class IoC : IResolvable
    {
        public ConcurrentDictionary<string, Scope> Scopes { get; } = new();
        private static Scope CurrentScope { get; } = new();

        public T Resolve<T>(string key, params object[] args)
        {
            if (key == "IoC.Register")
            {
                var registerCommand = new IocRegisterCommand(CurrentScope.Dependencies,
                                                             args[0] as string,
                                                             args[1] as Func<object[], object>);
                return registerCommand is T command
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