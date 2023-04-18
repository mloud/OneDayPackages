namespace OneDay.Notifications
{
    public static class NotificationConsts
    {
        public const string  PushNotificationType = "push_notification";
        public static string[] GetAll() => new[]
        {
            PushNotificationType
        };
    }
}