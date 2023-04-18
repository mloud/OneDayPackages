using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Requests
{
    public interface IRequestService
    {
        UniTask<DataResult<T>> AcceptRequest<T>(Request requests);
    }
}