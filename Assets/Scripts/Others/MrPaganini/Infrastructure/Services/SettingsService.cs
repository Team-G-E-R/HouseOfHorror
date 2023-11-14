using GameResources.SO;

namespace MrPaganini.UI.Windows
{
    public class SettingsService : ISettingsService
    {
        public SettingsConfig SettingsConfig { get; }

        public SettingsService(SettingsConfig settingsConfig)
        {
            SettingsConfig = settingsConfig;
        }
    }
}