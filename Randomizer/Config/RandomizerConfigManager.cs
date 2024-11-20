
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
        public ConfigEntry<string> Randomizer_seed;

        public ConfigEntry<Encounters> Rand_Spawn_Dropdown;
        public ConfigEntry<bool> Rand_Trainers_toggle;
        public ConfigEntry<bool> Rand_Starter_toggle;

        public ConfigEntry<Affinities> Rand_Stats_DropDown;
        public  ConfigEntry<PassiveTama> Rand_passive_toggle;
        public ConfigEntry<ElementType> Rand_Type_DropDown;
        public ConfigEntry<LearnSet> Rand_Tech_DropDown;

        public ConfigEntry<bool> Rand_Shop1_DropDown;
        public ConfigEntry<bool> Rand_Fishing1_DropDown;
        public ConfigEntry<bool> Rand_Digging1_DropDown;
        public ConfigEntry<bool> Rand_Drops_DropDown;
        public ConfigEntry<bool> Rand_GroundItems_Toggle;

        


        //  public static ConfigElement<bool> Rand_Tech_DropDown;


        public void Init(ConfigFile config)
        {
            InitConfig(config);
        }

        private void InitConfig(ConfigFile config)
        {
            string test=(""+UnityEngine.Random.Range(0, 100000)).GetHashCode()+"";

            Randomizer_toggle = config.Bind(new ConfigDefinition(CTG, "Randomizer"), true, new ConfigDescription("Enable Randomizer"));
            Randomizer_reroll = config.Bind(new ConfigDefinition(CTG, "Reroll"), false, new ConfigDescription("When enabled. Will auto-disable on next save loaded and will set the below seed."));
            Randomizer_seed = config.Bind(new ConfigDefinition(CTG, "Seed"), test, new ConfigDescription("Random will be set per this seed. The seed of a save will replace this field when loaded."));

            Rand_Spawn_Dropdown = config.Bind(new ConfigDefinition(CTG, "Wild Encounter"), Encounters.Limited, new ConfigDescription("Do encounter are randomized ? Limited is only dex order. Wild is everything. Boss included."));
            Rand_Trainers_toggle = config.Bind(new ConfigDefinition(CTG, "Trainers"), true, new ConfigDescription("Randomize Enemi team tama ?"));
            Rand_Starter_toggle = config.Bind(new ConfigDefinition(CTG, "Starters"), true, new ConfigDescription("Randomize Starters. Will reroll at each reloading"));
            Rand_Stats_DropDown = config.Bind(new ConfigDefinition(CTG, "Tama Stats"), Affinities.Balanced, new ConfigDescription("Do Stats are randomized from the sum of stats of the Tama or completely wild ?"));
            Rand_passive_toggle = config.Bind(new ConfigDefinition(CTG, "Tama Passives"), PassiveTama.Limited, new ConfigDescription("Randomize passives ? If enabled, Tama with banned passive will not reroll. And Tama cannot roll banned passive."));
            Rand_Type_DropDown = config.Bind(new ConfigDefinition(CTG, "Tama Types"), ElementType.Disabled, new ConfigDescription("Randomize Tama element ? Limited do not include Null & Virtual (Tama of these types are unmodified) "));
            Rand_Tech_DropDown = config.Bind(new ConfigDefinition(CTG, "Tama Learn Sets"), LearnSet.Disabled, new ConfigDescription("Randomize what Tama learns  ? Balanced will put tech depending of your type/stats. Wild is wild. WildCrazy enable boss tech."));
            Rand_Drops_DropDown = config.Bind(new ConfigDefinition(CTG, "Tama Drops"), true, new ConfigDescription("Randomize drops ?"));

            Rand_Shop1_DropDown = config.Bind(new ConfigDefinition(CTG, "Shop"), true, new ConfigDescription("Randomize shop ?"));
            Rand_Fishing1_DropDown = config.Bind(new ConfigDefinition(CTG, "Fishing"), true, new ConfigDescription("Randomize fishing ? Require Wild Encounter enabled for Tama."));
            Rand_Digging1_DropDown = config.Bind(new ConfigDefinition(CTG, "Digging"), true, new ConfigDescription("Randomize digging ? Require Wild Encounter enabled for Tama."));
            Rand_GroundItems_Toggle = config.Bind(new ConfigDefinition(CTG, "Ground Items"), true, new ConfigDescription("Randomize ground items ? Exclude one-time item"));



            //   Randomizer_toggle.SettingType
            //   Randomizer_toggle = config.Bind(new ConfigDefinition(CTG, "Randomizer"), true, new ConfigDescription("Enable Randomizer"));


            //Randomizer_toggle.SettingChanged

            //Randomizer_reroll.SettingChanged += BaseSettingsChanged;
            //Randomizer_toggle.SettingChanged += BaseSettingsChanged;

        }
        private void BaseSettingsChanged(object sender, EventArgs e)
        {
            //if (Randomizer_toggle.Value && Randomizer_reroll.Value)
            //{
            //    Plugin.Instance.Randomize();
            //}
        }
    }
}
