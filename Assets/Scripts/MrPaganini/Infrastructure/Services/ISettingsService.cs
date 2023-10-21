using GameResources.SO;

namespace MrPaganini.UI.Windows
{
    public interface ISettingsService : IService
    {
        SettingsConfig SettingsConfig { get; }
    }
}