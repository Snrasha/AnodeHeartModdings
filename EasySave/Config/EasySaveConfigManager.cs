
using BepInEx.Configuration;
using UnityEngine;

namespace EasySave.Config
{
    public class EasySaveConfigManager
    {
        internal  string CTG = "Settings";

        public  ConfigEntry<KeyCode> Fast_Save_Load;
        public ConfigEntry<KeyCode> Fast_Save_Save;
        public ConfigEntry<KeyCode> Save_Menu_Toggle;

        public void Init(ConfigFile config)
        {
            InitConfig(config);
        }

        private void InitConfig(ConfigFile config)
        {
            Save_Menu_Toggle = config.Bind(new ConfigDefinition(CTG, "Save Menu"), KeyCode.F4, new ConfigDescription("Key for open the save menu. (Use carefully and not on very weird space)"));
            Fast_Save_Load = config.Bind(new ConfigDefinition(CTG, "Quick Load"), KeyCode.F9, new ConfigDescription("Key for quick load."));
            Fast_Save_Save = config.Bind(new ConfigDefinition(CTG, "Quick Save"), KeyCode.F6, new ConfigDescription("Key for quick save."));
        }
    }
}
