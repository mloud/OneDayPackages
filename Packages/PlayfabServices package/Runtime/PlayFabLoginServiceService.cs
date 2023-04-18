using Cysharp.Threading.Tasks;
using OneDay.Core;
using OneDay.User;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace OneDay.PlayfabServices
{
    public class PlayFabLoginServiceService : ILoginService
    {
        private string TitleId { get; }

        public PlayFabLoginServiceService(string titleId) =>
            TitleId = titleId;
        public async UniTask<DataResult<LoginInfo>> Login()
        {
            PlayFabSettings.TitleId = TitleId;
            string requestError = null;
            bool loggedIn = false;
            string userId = null;
            PlayFabClientAPI.LoginWithCustomID(
                new LoginWithCustomIDRequest()
                    {
                        CreateAccount = true,
                        TitleId = PlayFabSettings.TitleId,
                        CustomId = SystemInfo.deviceUniqueIdentifier
                    },
                (loginResult) =>
                {
                    userId = loginResult.PlayFabId;
                    loggedIn = true;
                },
                (error) =>
                {
                    requestError = error.ErrorMessage;
                });

            await UniTask.WaitUntil(() => requestError != null || loggedIn);

            return requestError == null 
                ? DataResult<LoginInfo>.WithData(new LoginInfo(userId)) 
                : DataResult<LoginInfo>.WithError(requestError);
        }
    }
}