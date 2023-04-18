using System.Collections.Generic;

namespace OneDay.Notifications
{
    public class NotificationManagerSettings
    {
        public List<(string name, INotificationService serviceInstance, NotificationReceivedHandler handler )> Services { get; }

        public NotificationManagerSettings() => Services = 
            new List<(string name, INotificationService serviceInstance, NotificationReceivedHandler handler)>();
        
        public NotificationManagerSettings AddNotificationService(
            string name, INotificationService service, NotificationReceivedHandler notificationReceivedHandler)
        {
            Services.Add(new(name, service, notificationReceivedHandler));
            return this;
        }
    }
}