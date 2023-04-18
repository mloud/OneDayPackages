using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.TextGenerator
{
    public class TextGeneratorManager : ABaseManager, ITextGeneratorManager
    {
        private ITextGeneratorService TextGeneratorService { get; }
        public TextGeneratorManager(ITextGeneratorService textGeneratorService) =>
            TextGeneratorService = textGeneratorService;
        
        public async UniTask<DataResult<string>> Generate(string input) =>
            await TextGeneratorService.GenerateText(input);
    }
}