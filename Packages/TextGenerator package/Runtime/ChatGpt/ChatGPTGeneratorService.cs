using Cysharp.Threading.Tasks;
using OneDay.Core;
using OneDay.OpenAi;

namespace OneDay.TextGenerator.ChatGpt
{
    public class ChatGPTGeneratorService : ITextGeneratorService
    {
        private string Model { get; } 
        private OpenAiApi OpenAi { get; }
        
        public ChatGPTGeneratorService(OpenAiConfig config, string model)
        {
            Model = model;
            OpenAi = new OpenAiApi(config);
        }
        public async UniTask<DataResult<string>> GenerateText(string prompt)
        {
            var result = await OpenAi.ChatGpt.Generate(prompt, Model);
            return result.Error == null
                ? DataResult<string>.WithData(result.Data.choices[0].message.content)
                : DataResult<string>.WithError(result.Error);
        }
    }
}