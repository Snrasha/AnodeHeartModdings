using BepInEx;
using HarmonyLib;

namespace TextureReplacement
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        private void Awake()
        {

            this.gameObject.AddComponent<TextureReplacement>();
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");


            harmony.PatchAll();
        }
    }
}
