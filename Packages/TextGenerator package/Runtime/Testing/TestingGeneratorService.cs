using System;
using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.TextGenerator.Testing
{
    public class TestingGeneratorService : ITextGeneratorService
    {
        private TestingGeneratorConfig Config { get; }

        public TestingGeneratorService(TestingGeneratorConfig config) =>
            Config = config;
            
        public async UniTask<DataResult<string>> GenerateText(string prompt)
        {
            var rand = new Random();
            var delay = Config.MinDuration + rand.NextDouble() * (Config.MaxDuration - Config.MinDuration);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            return DataResult<string>.WithData(TestString);
        }

        private const string TestString =
            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec iaculis gravida nulla. " +
            "Praesent dapibus. Aenean fermentum risus id tortor. In laoreet, magna id viverra tincidunt, " +
            "sem odio bibendum justo, vel imperdiet sapien wisi sed libero. Praesent dapibus. Vivamus luctus " +
            "egestas leo. Aliquam ante. Nunc dapibus tortor vel mi dapibus sollicitudin. Maecenas ipsum velit, " +
            "consectetuer eu lobortis ut, dictum at dui. Aliquam erat volutpat. Curabitur bibendum justo non orci. " +
            "Vivamus luctus egestas leo. Donec vitae arcu. Nam sed tellus id magna elementum tincidunt. " +
            "Aliquam erat volutpat. Vestibulum erat nulla, ullamcorper nec, rutrum non, nonummy ac, erat. Temporibus " +
            "autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae " +
            "sint et molestiae non recusandae";
    }
}