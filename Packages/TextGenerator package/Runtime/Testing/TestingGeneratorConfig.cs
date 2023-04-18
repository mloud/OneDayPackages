namespace OneDay.TextGenerator.Testing
{
    public class TestingGeneratorConfig
    {
        public float MinDuration { get; }
        public float MaxDuration { get; }

        public TestingGeneratorConfig(float minDuration, float maxDuration)
        {
            MinDuration = minDuration;
            MaxDuration = maxDuration;
        }
    }
}