using BepInEx;
using System.IO;
using System.Reflection;
using System;
using UnityEngine;
using HarmonyLib;
using Universal.IconLib;
using Randomizer.Scripts;
using Randomizer.Config;
using System.Collections;
using Universal.TexturesLib;

namespace Randomizer
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("_Universal")]
  //  [BepInDependency("UniverseLib")]
    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static RandomizerSpawn randomizerSpawn;
        public static RandomizerStats randomizerStats;
        public static RandomizerTechs randomizerTechs;
        private SaveLoad saveLoad;
        public static RandomizerConfigManager configManager;

      //  private RandomizerLang randomizerLang;

        public static int speciesLen;

        public static Plugin Instance;



        

        // Config call this method when toggle and reroll are modified, but before modifying the rest of values, strangely.
        public void Randomize()
        {
            StartCoroutine(RandomizeNextFrame());
        }
        public IEnumerator RandomizeNextFrame()
        {
            yield return null;
            Debug.Log("Config Rand " + configManager.Randomizer_toggle.Value + " " + configManager.Randomizer_reroll.Value);
            //Debug.Log("Config1 " + configManager.Randomizer_toggle);

            if (configManager.Randomizer_toggle.Value)
            {
                randomizerSpawn.Randomize();
                randomizerStats.Randomize();
                Debug.Log("Config Save " + configManager.Rand_passive_toggle.Value + " " + randomizerStats.passives);
                saveLoad.SaveFiles();
            }
            configManager.Randomizer_reroll.Value = false;

            //Debug.Log("Config " + Config);
            Config.Save();
        }
        

        private void Awake()
        {
            Instance = this;
            saveLoad = new SaveLoad();

            speciesLen = Enum.GetNames(typeof(Species)).Length;
            randomizerSpawn = new RandomizerSpawn();
            randomizerStats = new RandomizerStats();
            

            harmony.PatchAll();
           // randomizerLang = new RandomizerLang();
            Sprite ModIcon = TexturesLib.CreateSprite("Randomizer.Assets.","Icon.png");
            IconGUI.AddIcon(new Icon("Randomizer", "Randomizer", ModIcon));

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");


            configManager = new RandomizerConfigManager();
            configManager.Init(Config);


            saveLoad.LoadFiles();
            Randomize();

            //   randomizerTechs.Randomize();


            //  ModMenuGUI.AddSubMenu("Randomizer", randomizerSubMenuGUI);

        }
    }
}
