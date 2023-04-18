using Cysharp.Threading.Tasks;

namespace OneDay.Core
{
    public interface IProvider<T>
    {
        UniTask<T> Provide();
    }
}