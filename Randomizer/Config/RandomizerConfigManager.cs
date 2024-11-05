
using BepInEx;
using BepInEx.Configuration;
using Randomizer.Config.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace Randomizer.Config
{
    public class RandomizerConfigManager
    {
        internal  string CTG = "Settings";
        // Actual UE Settings
        public  ConfigEntry<bool> Randomizer_toggle;
        public  ConfigEntry<bool> Randomizer_reroll;
        public  ConfigEntry<Spawn_enums> Rand_Spawn_Dropdown;
        public  ConfigEntry<Stats_enums> Rand_Stats_DropDown;
        public  ConfigEntry<bool> Rand_Starter_toggle;
        public  ConfigEntry<Passive_enums> Rand_passive_toggle;
        //  public static ConfigElement<bool> Rand_Tech_DropDown;


        public void Init(ConfigFile config)
        {
            InitConfig(config);
        }

        private void InitConfig(ConfigFile config)
        {
            Randomizer_toggle = config.Bind(new ConfigDefinition(CTG, "Randomizer"), true, new ConfigDescription("Enable Randomizer"));
            Randomizer_reroll = config.Bind(new ConfigDefinition(CTG, "Reroll"), false, new ConfigDescription("Reroll everything, save prefs will disable it and randomize everything enabled. You may need to reload your save."));
            Rand_Spawn_Dropdown = config.Bind(new ConfigDefinition(CTG, "Wild Encounter"), Spawn_enums.Disabled, new ConfigDescription("Do encounter are randomized ? Limited is only dex order. Wild is everything. Boss included."));
            Rand_Starter_toggle = config.Bind(new ConfigDefinition(CTG, "Starters"), false, new ConfigDescription("Randomize Starters. Will reroll at each reloading"));
            Rand_Stats_DropDown = config.Bind(new ConfigDefinition(CTG, "Tama Stats"), Stats_enums.Disabled, new ConfigDescription("Do Stats are randomized from the sum of stats of the Tama or completely wild ?"));
            Rand_passive_toggle = config.Bind(new ConfigDefinition(CTG, "Tama Passives"), Passive_enums.Disabled, new ConfigDescription("Randomize passives ? If enabled, Tama with banned passive will not reroll. And Tama cannot roll banned passive."));

            Randomizer_reroll.SettingChanged += BaseSettingsChanged;
            Randomizer_toggle.SettingChanged += BaseSettingsChanged;
            
        }
        private void BaseSettingsChanged(object sender, EventArgs e)
        {
            if (Randomizer_toggle.Value && Randomizer_reroll.Value)
            {
                Plugin.Instance.Randomize();
            }
        }
    }
}
