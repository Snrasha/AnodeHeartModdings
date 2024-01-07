﻿using BepInEx;
using Followers.ModMenu;
using HarmonyLib;

namespace Followers
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("_Universal")]
    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        FollowersPlugin followersPlugin;

        private void Awake()
        {




            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            followersPlugin = new FollowersPlugin(Logger);
            FollowersPlugin.followersSubMenuGUI = new FollowersSubMenuGUI();

            followersPlugin.LoadAllTextures();

            harmony.PatchAll();
        }
    }
}
