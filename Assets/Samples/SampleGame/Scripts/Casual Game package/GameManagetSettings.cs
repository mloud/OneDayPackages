using OneDay.Core;

namespace OneDay.CasualGame
{
    public class GameManagerSettings
    {
        public string DataStorageType { get; }

        public GameManagerSettings(string dataStorageType)
        {
            DataStorageType = dataStorageType;
        }
    }
}