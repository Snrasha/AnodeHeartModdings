using BepInEx;

using HarmonyLib;

namespace Universal
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        // public static ModMenuGUI ModMenuGUI;
        // public static IconGUI IconGUI;
        private PopupInfo popupInfo;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            harmony.PatchAll();
            popupInfo = gameObject.AddComponent<PopupInfo>();

           // ModMenuGUI = new ModMenuGUI();

        }
    }
}
