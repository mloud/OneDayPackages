using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.TextGenerator
{
    public interface ITextGeneratorManager : IManager
    {
        UniTask<DataResult<string>> Generate(string input);
    }
}