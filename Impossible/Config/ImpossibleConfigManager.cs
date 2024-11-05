
using BepInEx.Configuration;

namespace Impossible.Config
{
    public class ImpossibleConfigManager
    {
        internal  string CTG = "Settings";

        public  ConfigEntry<bool> Option_Stun_Tama;
        public ConfigEntry<int> Option_TP_Limit;

        public void Init(ConfigFile config)
        {
            InitConfig(config);
        }

        private void InitConfig(ConfigFile config)
        {
            Option_Stun_Tama = config.Bind(new ConfigDefinition(CTG, "Stun Tama"), false, new ConfigDescription("You will not be enable to use any Techs in battle."));
            Option_TP_Limit= config.Bind(new ConfigDefinition(CTG, "TP Limit"), 0, new ConfigDescription("Disabled if 0, otherwise set tp limit to the number set",
        new AcceptableValueRange<int>(0, 10)));
        }
    }
}
