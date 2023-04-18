using System.Collections.Generic;

namespace OneDay.Analytics
{
    public interface IAnalyticsService
    {
        void Log(string eventName, Dictionary<string, string> eventParameters);
    }
}