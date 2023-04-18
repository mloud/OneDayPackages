namespace OneDay.Config
{
    public class ConfigManagerSettings
    {
        public string[] ConfigKeys { get; }

        public ConfigManagerSettings(params string[] keys) =>
            ConfigKeys = keys;
    }
}