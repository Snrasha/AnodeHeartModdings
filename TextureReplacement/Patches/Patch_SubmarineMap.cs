using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using System;

namespace TextureReplacement.Patches
{
    public class SubmarineMapHUD_Load
    {
        [HarmonyPatch]
        static class Patch_SubmarineMapHUD_Load
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(SubmarineMapHUD));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("Load"))
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
                return AccessTools.Method(typeof(SubmarineMapHUD), "Load");
            }

            [HarmonyPrefix]
            static void Prefix(ref IEnumerator __result, SubmarineMapHUD __instance)
            {
                Sprite spritecursor = TextureReplacement.GetSprite(TextureReplacement.SpritesCharacterIcons, "PlayerCursor");

                if (spritecursor != null && __instance!=null  && __instance.Player != null)
                {
                    UnityEngine.UI.Image image = __instance.Player.GetComponent<UnityEngine.UI.Image>();
                    if (image != null)
                    {
                        image.sprite = spritecursor;
                    }
                }

            }
        }


    }
}
