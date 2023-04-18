using OneDay.Core;
using Cysharp.Threading.Tasks;

namespace OneDay.Ads
{
    public class AdManager : ABaseManager, IAdManager
    {
        public bool IsRewardedVideoAvailable => AdService.RewardedVideoAvailable;
       
        public AdVideoWasRewarded RewardedVideoAdRewarded { get; set; }
        
        private IAdService AdService { get; }

        public AdManager(IAdService adService)
        {
            AdService = adService;
            AdService.RewardedVideoAdRewarded += (placement) =>
                RewardedVideoAdRewarded?.Invoke(placement);
        }
        protected override UniTask OnInitialize() => UniTask.CompletedTask;
        
        public async UniTask<DataResult<bool>> ShowVideo(string placement) =>
            await AdService.ShowVideo(placement);
    }
}