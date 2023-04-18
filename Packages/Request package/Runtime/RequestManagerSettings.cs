using System.Collections.Generic;

namespace OneDay.Requests
{
    public class RequestManagerSettings
    {
        public Dictionary<IRequestService, string[]> Services { get; } = new();

        public RequestManagerSettings AddService(IRequestService service, params string[] requestNames)
        {
            Services.Add(service, requestNames);
            return this;
        }
    }
}