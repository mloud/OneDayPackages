using OneDay.Core;

namespace OneDay.Deeplink
{
    public interface IDeepLinkManager : IManager
    {
        void PerformDeepLink(string link);
    }
}