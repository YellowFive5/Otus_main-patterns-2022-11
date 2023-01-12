#region Usings

using System;
using Exceptions.Commands;

#endregion

namespace Fabric
{
    public class IoC : IResolvable
    {
        public ICommand Resolve(string key, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}