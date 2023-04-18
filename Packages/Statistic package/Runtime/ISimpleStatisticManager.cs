using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Statistic
{
    public interface ISimpleStatisticManager : IManager
    {
        bool IsInitialized { get; }
        UniTask FetchAllStatistic();
        UniTask SaveAllStatistic();
        UniTask<int> GetStatistic(string key);
        UniTask SetStatistic(string key, int value);
        UniTask IncrementStatistic(string key, int byValue = 1);
    }
}