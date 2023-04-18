using Cysharp.Threading.Tasks;

namespace OneDay.Core
{
    public interface IManager
    {
        UniTask Initialize();
        UniTask Release();
    }
}