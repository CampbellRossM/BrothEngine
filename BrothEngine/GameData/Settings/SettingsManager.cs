using System.Collections.Generic;

namespace Broth.GameData.Settings
{
    /* Getting rid of this file

    /// <summary>
    /// Config Settings are the only settings that are loaded before the game window is created.
    /// If no config file is found, the default settings will be used to create a new one.
    /// </summary>
    public class ConfigSettings
    {
        // Window Definition
        public string Title { get; set; } = "BrothEngine"; //TODO: Title can be set later. Maybe this shouldn't be in config.
        public uint Width { get; set; } = 800;
        public uint Height { get; set; } = 600;
        public uint TargetFPS { get; set; } = 60;
        public bool Fullscreen { get; set; } = false;

        public string ResourcesFolder { get; set; } = "Resources";
    }

    */

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
                ID = "ENGINE::WindowWidth",
                Name = "Window Width",
                Value = 800
            });
            RegisterSetting(new IntSetting()
            {
                ID = "ENGINE::WindowHeight",
                Name = "Window Height",
                Value = 600
            });
            RegisterSetting(new IntSetting()
            {
                ID = "ENGINE::TargetFPS",
                Name = "Target FPS",
                Value = 60
            });
            RegisterSetting(new BoolSetting()
            {
                ID = "ENGINE::Fullscreen",
                Name = "Fullscreen",
                Value = false
            });
        }
    }
}
