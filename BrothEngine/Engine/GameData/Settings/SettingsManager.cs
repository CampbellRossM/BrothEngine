using System.Collections.Generic;

namespace Broth.Engine.GameData.Settings
{
    /// <summary>
    /// Contains references to all settings that can be manipulated by a user both in game and in files.
    /// </summary>
    public class SettingsManager
    {
        private readonly Dictionary<string, Setting> settings = new Dictionary<string, Setting>();

        public void RegisterSetting(Setting setting)
        {
            settings.Add(setting.ID, setting);
        }

        public Setting GetSetting(string id)
        {
            settings.TryGetValue(id, out Setting setting);
            return setting;
        }

        // TODO: Read the common settings from JSON?
        internal void RegisterCommonSettings()
        {
            RegisterSetting(new IntSetting()
            {
                ID = "ENGINE.WindowWidth",
                Name = "Window Width",
                Default = 800
            });
            RegisterSetting(new IntSetting()
            {
                ID = "ENGINE.WindowHeight",
                Name = "Window Height",
                Default = 600
            });
            RegisterSetting(new IntSetting()
            {
                ID = "ENGINE.TargetFPS",
                Name = "Target FPS",
                Default = 60
            });
            RegisterSetting(new BoolSetting()
            {
                ID = "ENGINE.Fullscreen",
                Name = "Fullscreen",
                Default = false
            });
        }
    }
}
