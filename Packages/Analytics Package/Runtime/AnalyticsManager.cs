using System.Collections.Generic;
using System.Linq;
using OneDay.Core;

namespace OneDay.Analytics
{
    public class AnalyticsManager : ABaseManager, IAnalyticsManager
    {
        private IAnalyticsService AnalyticsService { get; }

        public AnalyticsManager(IAnalyticsService analyticsService) =>
            AnalyticsService = analyticsService;

        public void LogEvent(string eventName, Dictionary<string, string> eventParameters) =>
            AnalyticsService.Log(eventName, eventParameters);
        
        public void LogEvent(string eventName, params (string key, string value)[] eventParameters) =>
            AnalyticsService.Log(eventName, eventParameters.ToDictionary(x=>x.key, x=>x.value));
    }
}