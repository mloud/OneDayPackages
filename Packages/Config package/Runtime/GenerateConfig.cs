namespace OneDay.Config
{
    public class NotificationConfig
    {
        public int AppStartsToDisplayNotificationRequest;
        public float MinDelayInHoursBetweenAppStarts;

    }
    public class GenerateConfig
    {
        public string DefaultPrompt;
        public int ImagesCount;
        public int CancelTimeout;
    }
    

    public class SaveConfig
    {
        public bool SaveThumbnails;
        public int ThumbnailWidth;
        public int ThumbnailHeight;
    }

    public class PremiumConfig
    {
        public bool WatermarkForNonPayers;
        public int MaxGeneratesPerDayForNonPayers;
    }
    
    public class SettingsConfig
    {
        public string MailLink;
        public string DefaultMailSubject;
        
        public string PrivatePolicyLink;
        public string FaqLink;
        public string CustomerServiceLink;
        
        public string ShareAndroidLink;
        public string ShareIosLink;
        public string ShareMessage;

        public string RateLinkAndroid;
        public string RateLinkIos;

        public string GetShareLink()
        {
#if UNITY_ANDROID
            return ShareAndroidLink;
#elif UNITY_IOS
            return ShareIosLink;
#else
            return "";
#endif
        }
        
        public string GetRateLink()
        {
            #if UNITY_ANDROID
            return RateLinkAndroid;
#elif UNITY_IOS
            return RateLinkIos;
#else
            return "";
#endif
        }
    }

    public class AdConfig
    {
        public bool AdsActive;
    }
}