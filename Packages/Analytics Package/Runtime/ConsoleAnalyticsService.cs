using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OneDay.Analytics
{
    public class ConsoleAnalyticsService :  IAnalyticsService
    {
        private StringBuilder StrBuilder { get; }

        public ConsoleAnalyticsService() =>
            StrBuilder = new StringBuilder();
        
        public void Log(string eventName, Dictionary<string, string> eventParameters)
        {
            StrBuilder.Clear();
            StrBuilder.AppendLine($"ConsoleAnalyticsService received event: {eventName}");
            foreach (var keyValue in eventParameters)
            {
                StrBuilder.AppendLine($"   with parameter: {keyValue.Key} : {keyValue.Value}");
            }
            Debug.Log(StrBuilder.ToString());
        }
    }
}