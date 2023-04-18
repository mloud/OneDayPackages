using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Deeplink
{
    public class DeepLinkManager : ABaseManager, IDeepLinkManager
    {
        public void PerformDeepLink(string link)
        {
            Debug.Log($"DeeplinkApi received link {link}");
        }
    }
}