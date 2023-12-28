using BepInEx;
using Extrama.Patches;
using HarmonyLib;
using static UnityEngine.ParticleSystem.PlaybackState;
using System.Collections.Generic;
using UnityEngine;

namespace Extrama
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        ExtramaFramework ExtramaFramework;

        private void Awake()
        {
            harmony.PatchAll();
            //  Catch StringLocalizer



            // We need to catch the Json convert, which convert string to Species




            //            So, i will write everything down before forgetting(and check if this is all, for stochastic and i forget nothing):
            //-A specific tama is saved in the save with their exact name.
            //-The name is linked to the enum list "Species" which give it a number(why? No idea)
            //- The game get the informations of the Generic Tama from MonsterLibrary.Data.Which is a static dictionary loading data from resources when needed(mostly after have loaded a game).
            //- The game load Sprites of the Generic Tama without looking at his data.

            //-> Like texture replacement, i need to load the sprites of the Tama, not an issue.
            //-> I need to load the Json of every Tama, not an issue.
            //-> 
            //  EnumPatcher.AddEnumValue<Species>(10000, "Pikachu");
            //   EnumPatcher.AddEnumValue<Species>(10001, "Raichu");
            //   EnumPatcher.
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            ExtramaFramework = new ExtramaFramework(Logger);

            ExtramaFramework.LoadAllTamas();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded2!");


        }
    }
}
