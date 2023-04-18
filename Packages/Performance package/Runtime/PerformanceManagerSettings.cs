namespace OneDay.Performance
{
    public class PerformanceManagerSettings
    {
        public int DefaultFramerate { get; }

        public PerformanceManagerSettings(int defaultFramerate) =>
            DefaultFramerate = defaultFramerate;
    }
}