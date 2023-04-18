using OneDay.Core;

namespace OneDay.Performance
{
    public interface IPerformanceManager : IManager
    {
        bool StartMeasureBlock(string blockName);
        long StopMeasureBlock(string blockName);
        void PrintHistory();
        
        void SetFrameRate(int framerate);
    }
}