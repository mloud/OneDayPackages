using OneDay.Core;
using Cysharp.Threading.Tasks;

namespace OneDay.Ads
{
    public delegate void AdVideoWasRewarded(string placement);
    public interface IAdManager : IManager
    {
        bool IsRewardedVideoAvailable { get; }
        AdVideoWasRewarded RewardedVideoAdRewarded { get; set; }
        UniTask<DataResult<bool>> ShowVideo(string placements);
    }
}