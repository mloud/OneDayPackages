using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OneDay.Core;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace OneDay.Performance
{
    public class PerformanceManager : ABaseManager, IPerformanceManager
    {
        private List<Stopwatch> AvailableWacthes { get; } = new();
        private Dictionary<string, Stopwatch> StopWatches { get; } = new();
        private Dictionary<string, long>  ExecutionTimeHistory { get; }= new();
        
        private PerformanceManagerSettings Settings { get; }

        public PerformanceManager(PerformanceManagerSettings settings)
        {
            Settings = settings;
            SetFrameRate(settings.DefaultFramerate);
        }

        public void SetFrameRate(int framerate) =>
            Application.targetFrameRate = framerate;
        
        public void PrintHistory()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Execution time history");
            stringBuilder.AppendLine();
            foreach (var measuredBlock in ExecutionTimeHistory)
            {
                stringBuilder.Append("Block Name: ").Append(measuredBlock.Key).Append(" ").Append(measuredBlock.Value)
                    .Append(" ms");
                stringBuilder.AppendLine();
            }
            Debug.Log(stringBuilder.ToString());
        }
        
        public bool StartMeasureBlock(string blockName)
        {
            if (StopWatches.ContainsKey(blockName))
            {
                Debug.LogError($"Already measuring block {blockName}");
                return false;
            }
            Debug.Log($"Start measuring execution time of block {blockName} ms");
            var sw = GetStopWatch();
            StopWatches.Add(blockName, sw);
            sw.Reset();
            sw.Start();
            return true;
        }

        public long StopMeasureBlock(string blockName)
        {
            if (StopWatches.TryGetValue(blockName, out var sw))
            {
                sw.Stop();
                AvailableWacthes.Add(sw);
                Debug.Log($"Execution of block {blockName} took {sw.ElapsedMilliseconds} ms");
                if (ExecutionTimeHistory.ContainsKey(blockName))
                {
                    ExecutionTimeHistory[blockName] = sw.ElapsedMilliseconds;
                }
                else
                {
                    ExecutionTimeHistory.Add(blockName, sw.ElapsedMilliseconds);
                }
                return sw.ElapsedMilliseconds;
            }

            Debug.LogError($"No such  measuring block exists {blockName}");
            return -1;
        }

        private Stopwatch GetStopWatch()
        {
            if (AvailableWacthes.Count <= 0) 
                return new Stopwatch();
            
            var sw = AvailableWacthes[^1];
            AvailableWacthes.RemoveAt(AvailableWacthes.Count - 1);
            return sw;
        }
    }
}