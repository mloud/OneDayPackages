using System.Collections.Generic;
using OneDay.Core;

namespace OneDay.Analytics
{
    public interface IAnalyticsManager : IManager
    {
        void LogEvent(string eventName, Dictionary<string, string> eventParameters);
        void LogEvent(string eventName, params (string key, string value)[] eventParameters);
    }
}