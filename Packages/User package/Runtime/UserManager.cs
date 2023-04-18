using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine;

namespace OneDay.User
{
    public class UserManager : ABaseManager, IUserManager
    {
        public IUser User { get; private set; }
        private ILoginService LoginServiceService { get; }

        protected override async UniTask OnInitialize() =>
            await Login();
    
        public UserManager(ILoginService loginServiceService) =>
            LoginServiceService = loginServiceService;
        
        private async UniTask<DataResult<IUser>> Login()
        {
            var loginInfo = await LoginServiceService.Login();
            if (!loginInfo.HasError())
            {
                User = new User(loginInfo.Data.UserId);
                Debug.Log($"User was locked with userId {User.Id}");
                return DataResult<IUser>.WithData(User);
            }
        
            Debug.LogError($"Error during login {loginInfo.Error}");
            return DataResult<IUser>.WithError(loginInfo.Error);
        }
    }
}