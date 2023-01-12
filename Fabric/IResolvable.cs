#region Usings

using Exceptions.Commands;

#endregion

namespace Fabric
{
    public interface IResolvable
    {
        ICommand Resolve(string key, params object[] args);
    }
}