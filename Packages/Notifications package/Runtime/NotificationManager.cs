using System.Collections.Generic;
using OneDay.Core;
using Cysharp.Threading.Tasks;
using OneDay.Permissions;
using UnityEngine;

namespace OneDay.Notifications
{
    public delegate void NotificationReceivedHandler(string notificationType, INotificationData data);
   
    public class NotificationManager : ABaseManager, INotificationManager
    {
        private IPermissionService PermissionService { get; }
        private IStorageService<int> StateStorage { get; }
        private Dictionary<string, InternalService> Services { get; }

        public NotificationManager(
            NotificationManagerSettings settings, 
            IStorageService<int> stateStorage,
            IPermissionService permissionService)
        {
            StateStorage = stateStorage;
            Services = new Dictionary<string, InternalService>();
            PermissionService = permissionService;
            foreach (var servicePair in settings.Services)
            {
                Services.Add(
                    servicePair.name,
                    new InternalService(servicePair.serviceInstance, servicePair.name, servicePair.handler));
            }
        }

        protected override async UniTask OnInitialize()
        {
            await UniTask.WhenAll(Services.Values.Select(x => x.Service.Initialize()));
            var setStatesTasks = new List<UniTask>();
            
            foreach (var internalService in Services)
            {
                internalService.Value.Service.SubscribeForMessages(internalService.Value.NotificationDataReceived);
                bool isServiceEnabled = (await StateStorage.Load(internalService.Value.Name)) == 1;
                setStatesTasks.Add(internalService.Value.Service.SetNotificationsEnabled(isServiceEnabled));
            }
            await UniTask.WhenAll(setStatesTasks);
        }

        public async UniTask SetNotificationEnabled(bool enabled, string messagesType)
        {
            if (Services.TryGetValue(messagesType, out var internalService))
            {
                await internalService.Service.SetNotificationsEnabled(enabled);
                await StateStorage.Save(messagesType, enabled ? 1 : 0);
            }
            else
            {
                Debug.LogError($"No notification service exist for messageType {messagesType}");    
            }
        }
        public bool IsNotificationEnabled(string messageType)
        {
            if (Services.ContainsKey(messageType))
            {
                return Services[messageType].Service.IsNotificationEnabled;
            }
            Debug.LogError($"No such message type {messageType} is registered");
            return false;
        }
     
        private class InternalService
        {
            public INotificationService Service { get; }
            public string Name { get; }
            private NotificationReceivedHandler Handler { get; }
            public InternalService(INotificationService service, string name, NotificationReceivedHandler handler)
            {
                Service = service;
                Name = name;
                Handler = handler;
            }

            public void NotificationDataReceived(INotificationData notificationData) =>
                Handler(Name, notificationData);
        }
    }
}