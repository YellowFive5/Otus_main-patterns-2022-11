#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Factory
{
    public class IoC : IResolvable
    {
        private IDictionary<string, object> dependencies { get; } = new Dictionary<string, object>();

        public T Resolve<T>(string key, params object[] args)
        {
            if (key == "IoC.Register")
            {
                var registerCommand = new IocRegisterCommand(dependencies,
                                                             args[0] as string,
                                                             args[1] as Func<object[], object>);
                return registerCommand is T command
                           ? command
                           : default;
            }

            if (key != null && dependencies.ContainsKey(key))
            {
                var function = (Func<object[], object>)dependencies[key];
                return (T)function.Invoke(args);
            }

            throw new Exception($"No operation with key {key}");
        }
    }
}