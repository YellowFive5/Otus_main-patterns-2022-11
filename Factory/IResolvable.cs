#region Usings

using Exceptions.Commands;

#endregion

namespace Factory
{
    public interface IResolvable
    {
        ICommand Resolve(string key, params object[] args);
    }
}