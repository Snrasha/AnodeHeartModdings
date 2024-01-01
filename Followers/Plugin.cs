using BepInEx;
using HarmonyLib;

namespace Followers
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        FollowersBehaviour textureReplacement;

        private void Awake()
        {




            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            textureReplacement = new FollowersBehaviour(Logger);

            textureReplacement.LoadAllTextures();

            harmony.PatchAll();
        }
    }
}
