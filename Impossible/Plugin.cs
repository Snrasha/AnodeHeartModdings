using BepInEx;

using UnityEngine;
using HarmonyLib;
using Universal.IconLib;
using Universal.TexturesLib;
using Impossible.Config;

namespace Impossible
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("_Universal")]

    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        //   public static ImpossibleSubMenuGUI ImpossibleSubMenuGUI;

        //   private ImpossibleLang ImpossibleLang;
        public static ImpossibleConfigManager impossibleConfigManager;



        private void Awake()
        {
            harmony.PatchAll();
         //   ImpossibleLang = new ImpossibleLang();
            Sprite ModIcon =TexturesLib.CreateSprite("Impossible.Assets.", "Icon.png");
            IconGUI.AddIcon(new Icon("Impossible", "Impossible", ModIcon));

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            // ImpossibleSubMenuGUI = new ImpossibleSubMenuGUI();

            //  ModMenuGUI.AddSubMenu("Impossible", ImpossibleSubMenuGUI);
            impossibleConfigManager = new ImpossibleConfigManager();
            impossibleConfigManager.Init(Config);
        }

 
    }
}
