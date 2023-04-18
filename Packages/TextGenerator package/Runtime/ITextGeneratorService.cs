using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.TextGenerator
{
    public interface ITextGeneratorService
    {
        UniTask<DataResult<string>> GenerateText(string prompt);
    }
}