using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.User
{
    public class LoginInfo
    {
        public string UserId { get; }
        public LoginInfo(string userId) => UserId = userId;
    }
    public interface ILoginService
    {
        UniTask<DataResult<LoginInfo>> Login();
    }
}