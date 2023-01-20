#region Usings

#endregion

namespace Factory
{
    public interface IResolvable
    {
        T Resolve<T>(string key, params object[] args);
    }
}