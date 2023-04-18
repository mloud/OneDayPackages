using OneDay.Core;

namespace OneDay.Device
{
    public class DeviceManager : ABaseManager, IDeviceManager
    {
        private IDeviceService DeviceService { get; }
        
        public DeviceManager(IDeviceService deviceService) =>
            DeviceService = deviceService;

        public string GetUniqueDeviceIdentifier() => DeviceService.GetUniqueDeviceIdentifier();
    }
}