using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace OneDay.Notifications
{
    public interface INotificationData
    {
        IDictionary<string, string> DataDictionary { get; }
    }
 
    public delegate void NotificationDataReceived(INotificationData data); 
    
    public interface INotificationService
    {
        bool IsNotificationEnabled { get; }
        string PermissionName { get; }
        
        UniTask Initialize();
        UniTask SetNotificationsEnabled(bool enabled);
        UniTask SubscribeForMessages(NotificationDataReceived callback);
        UniTask UnsubscribeForMessages(NotificationDataReceived callback);
    }
}