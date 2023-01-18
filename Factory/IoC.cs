#region Usings

using System;

#endregion

namespace Factory
{
    public class IoC : IResolvable
    {
        public T Resolve<T>(string key, params object[] args)
        {
            throw new Exception($"No operation with key {key}");
        }
    }
}