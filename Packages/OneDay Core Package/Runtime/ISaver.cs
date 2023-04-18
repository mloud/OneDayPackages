using Cysharp.Threading.Tasks;

namespace OneDay.Core
{
    public interface ISaver<in T>
    {
        UniTask<Result> Save(T obj);
    }
}