using BepInEx;
using System.IO;
using System.Reflection;
using System;
using UnityEngine;
using HarmonyLib;
using System.Collections;
using Universal.IconLib;
using Universal;
using Universal.ModMenu;
using Impossible.ModMenu;
using Impossible.Langs;

namespace Impossible
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("_Universal")]

    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static ImpossibleSubMenuGUI ImpossibleSubMenuGUI;

        private ImpossibleLang ImpossibleLang;

        

        private void Awake()
        {
            harmony.PatchAll();
            ImpossibleLang = new ImpossibleLang();
            Sprite ModIcon = CreateSprite("Icon.png");
            IconGUI.AddIcon(new Icon("Impossible", "Impossible", ModIcon));

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            ImpossibleSubMenuGUI = new ImpossibleSubMenuGUI();

            ModMenuGUI.AddSubMenu("Impossible", ImpossibleSubMenuGUI);

        }

    


            public static Sprite CreateSprite(string path)
        {

            Texture2D tex = new Texture2D(1, 1);
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("Impossible.Assets." + path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    tex.filterMode = FilterMode.Point;  // Thought maybe this would help 
                    tex.LoadImage(bytes);
                }
            }
            catch (Exception e)
            {
            }

            tex.filterMode = FilterMode.Point;
            tex.anisoLevel = 0;
            tex.wrapMode = TextureWrapMode.Clamp;

            tex.Apply();
            Vector2 standardPivot = new Vector2(tex.width / 2f, tex.height / 2f);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            return sprite;
        }
    }
}
