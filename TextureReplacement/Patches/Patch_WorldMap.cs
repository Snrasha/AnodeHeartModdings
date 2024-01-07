using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using System;

namespace TextureReplacement.Patches
{
    public class GadgetsHUD_showWorldMapEnumerator
    {
        [HarmonyPatch]
        static class Patch_GadgetsHUD_showWorldMap
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(GadgetsHUD));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("showWorldMap"))
                    {
                        if (method.ReturnType == typeof(IEnumerator))
                        {
                            if (method.GetParameters().Length == 0)
                            {
                                return method;
                            }
                        }
                    }
                }
                return AccessTools.Method(typeof(GadgetsHUD), "showWorldMap");
            }

            [HarmonyPrefix]
            static void Prefix(ref IEnumerator __result, GadgetsHUD __instance)
            {
                Sprite spritecursor = TextureReplacement.GetSprite(TextureReplacement.SpritesCharacterIcons, "PlayerCursor");

                if (spritecursor != null && __instance!=null && __instance.WorldMap!=null && __instance.WorldMap.Player != null)
                {
                    UnityEngine.UI.Image image = __instance.WorldMap.Player.GetComponent<UnityEngine.UI.Image>();
                    if (image != null)
                    {
                        image.sprite = spritecursor;
                    }
                }

            }
        }


    }
}
