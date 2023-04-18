using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Requests
{
    public class RequestManager : ABaseManager, IRequestManager
    {
        private Dictionary<string, IRequestService> RequestNameToServiceMapping { get; }
        private Dictionary<IRequestService, List<string>> RequestServiceToRequestNameMapping { get; }

        public RequestManager(RequestManagerSettings settings)
        {
            RequestNameToServiceMapping = new Dictionary<string, IRequestService>();
            RequestServiceToRequestNameMapping = new Dictionary<IRequestService, List<string>>();

            foreach (var service in settings.Services)
            {
                RegisterRequestService(service.Key, service.Value);
            }
        }
        
        public async UniTask<DataResult<T>> SendRequest<T>(Request request)
        {
            if (RequestNameToServiceMapping.TryGetValue(request.Name, out var service))
            {
                return await service.AcceptRequest<T>(request);
            }
            Debug.LogError($"No service fot request {request.Name} found");
            return DataResult<T>.WithError("No service fot request {request.Name} found");
        }

        public void RegisterRequestService(IRequestService service, params string[] requestNames)
        {
            if (RequestServiceToRequestNameMapping.Any(x => x.Key.GetType() == service.GetType()))
            {
                throw new ArgumentException($"Service with type {service.GetType()} is already registered");
            }

            foreach (var requestName in requestNames)
            {
                if (RequestNameToServiceMapping.ContainsKey(requestName))
                {
                    throw new ArgumentException($"Request with name {requestNames} is already registered to service {RequestNameToServiceMapping[requestName].GetType()}");
                }
            }
            
            RequestServiceToRequestNameMapping.Add(service, requestNames.ToList());

            foreach (var requestName in requestNames)
            {
                RequestNameToServiceMapping.Add(requestName, service);
            }
        }
    }
}