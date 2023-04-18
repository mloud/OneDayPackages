using OneDay.Core;

namespace OneDay.User
{
    public interface IUserManager : IManager
    {
        public IUser User { get; }
    }
}