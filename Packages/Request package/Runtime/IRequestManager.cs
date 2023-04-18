using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Requests
{
    public class Request
    {
        public string Name { get; }
        public Dictionary<string, object> Parameters { get; }
        public Request(string name, Dictionary<string, object> parameters)
        {
            Name = name;
            Parameters = parameters;
        }
    }

    public interface IRequestManager : IManager
    {
        UniTask<DataResult<T>> SendRequest<T>(Request requests);
        void RegisterRequestService(IRequestService service, string[] requestNames);
    }
}