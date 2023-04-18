using UnityEngine.Device;

namespace OneDay.Device
{
    public class DeviceService : IDeviceService
    {
        public string GetUniqueDeviceIdentifier() =>
            string.IsNullOrEmpty(SystemInfo.deviceUniqueIdentifier) ? "Unknown" : SystemInfo.deviceUniqueIdentifier;
    }
}