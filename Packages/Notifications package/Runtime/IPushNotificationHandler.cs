using OneDay.Core;
using UnityEngine;

namespace OneDay.Notifications
{
    public interface IPushNotificationHandler
    {
        void OnPushNotificationReceived(string notificationType, INotificationData data);
    }
}