using System;
using OneDay.Core;
using Cysharp.Threading.Tasks;

namespace OneDay.Ads
{
    public interface IAdService
    {
        bool RewardedVideoAvailable { get; }
        event Action<string> RewardedVideoAdRewarded;
        UniTask<DataResult<bool>> ShowVideo(string placements);
    }
}