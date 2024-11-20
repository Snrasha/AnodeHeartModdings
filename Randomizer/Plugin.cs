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
using Randomizer.Scripts.Tama;
using Randomizer.Scripts.Techs;
using static UnityEngine.ParticleSystem.PlaybackState;
using Randomizer.Scripts.Items;
using Randomizer.Scripts.Encounter;
//using Randomizer.Config.UI;
//using ConfigManager.UI.InteractiveValues;

namespace Randomizer
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("_Universal")]
  //  [BepInDependency("UniverseLib")]
    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static RandSeed randomizerSeed;
        public static RandomizerSpawn randomizerSpawn;
        public static RandomizerShop randomizerShop;
        private RandomizerFishing randomizerFishing;
        public static RandomizerDigging randomizerDigging;
        public static RandomizerItems randomizerItems;
        public static RandomizerSpecies randomizerSpecies;



        private RandomizerTama randomizerStats;
        private RandomizerTechs randomizerTechs;
        private SaveLoad saveLoad;
        public static RandomizerConfigManager configManager;

      //  private RandomizerLang randomizerLang;

        public static int speciesLen;
        public static int techsLen;
        public static int itemLen;

        public static Plugin Instance;

      //  private bool skipLoadForNewRand = false;



        

        // Config call this method when toggle and reroll are modified, but before modifying the rest of values, strangely.
        //public void Randomize()
        //{
        //    StartCoroutine(RandomizeNextFrame());
        //}
        //public IEnumerator RandAndSave()
        //{
        //    yield return RandomizeNextFrame();
        //    SaveLoa
        //}
        //public IEnumerator RandomizeNextFrame()
        //{
        //    yield return null;

            public void RandomizeNextFrame()
            {
               // yield return null;
                //    Debug.Log("Config Rand " + configManager.Randomizer_toggle.Value + " " + configManager.Randomizer_reroll.Value);

                if (configManager.Randomizer_toggle.Value)
            {
                //  string seed = configManager.Randomizer_seed.Value;
                //  randomizerSeed.Randomize(seed);
                randomizerSeed.Set();
                randomizerSpawn.Randomize();
                randomizerStats.Randomize();
                randomizerTechs.Randomize();
                randomizerItems.Randomize();
                randomizerFishing.Randomize();
                
             //   Debug.Log("Config Save " + configManager.Rand_passive_toggle.Value + " ");
                if (GameState.instance!=null && GameState.instance.Data !=null&& GameState.instance.Data.Slot >=0)
                {
                    saveLoad.SaveFiles(GameState.instance.Data.Slot);
                }
                //else if (configManager.Randomizer_reroll.Value)
                //{
                //    skipLoadForNewRand = true;
                //}

            }
            //configManager.Randomizer_reroll.Value = false;

            //Debug.Log("Config " + Config);
            Config.Save();
        }

        private void Awake()
        {
          //  InteractiveValue.RegisterIValueType<RandInteractiveBool>();

            Instance = this;
            saveLoad = new SaveLoad();

            speciesLen = Enum.GetNames(typeof(Species)).Length;
            techsLen = Enum.GetNames(typeof(Tech)).Length;
            itemLen = Enum.GetNames(typeof(ItemType)).Length;

            
            randomizerSeed = new RandSeed();
            randomizerSpecies = new RandomizerSpecies();

            randomizerSpawn = new RandomizerSpawn();
            randomizerStats = new RandomizerTama();
            randomizerTechs = new RandomizerTechs();
            randomizerShop=new RandomizerShop();
            randomizerFishing=new RandomizerFishing();
            randomizerDigging = new RandomizerDigging();
            randomizerItems = new RandomizerItems();


            harmony.PatchAll();
           // randomizerLang = new RandomizerLang();
            Sprite ModIcon = TexturesLib.CreateSprite("Randomizer.Assets.","Icon.png");
            IconGUI.AddIcon(new Icon("Randomizer", "Randomizer", ModIcon));

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");


            configManager = new RandomizerConfigManager();
            configManager.Init(Config);
            



            //   randomizerTechs.Randomize();


            //  ModMenuGUI.AddSubMenu("Randomizer", randomizerSubMenuGUI);

        }

        


        // Used when the save is loaded.
        public void Load(int slot)
        {
            if (configManager.Randomizer_toggle.Value)
            {
                    // Load  files and set randomisation.
                saveLoad.LoadFiles(slot);


                // Randomize Load everything. If empty, will make. If reroll, will reroll.
                RandomizeNextFrame();
                // Save all of it.
                saveLoad.SaveFiles(slot);
            }
        }
        public void Save(int slot)
        {
            saveLoad.SaveFiles(slot);
        }
    }
}
