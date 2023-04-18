using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Notifications
{
    public interface INotificationManager : IManager
    {
        UniTask SetNotificationEnabled(bool enabled, string messageType);
        bool IsNotificationEnabled(string messageType);
    }
}