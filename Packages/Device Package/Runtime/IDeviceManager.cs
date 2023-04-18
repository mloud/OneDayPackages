using OneDay.Core;

namespace OneDay.Device
{
    public interface IDeviceManager : IManager
    {
        string GetUniqueDeviceIdentifier();
    }
}