using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using System;

namespace TextureReplacement.Patches
{
    public class AriboatRaceController_controlEnumerator
    {
        [HarmonyPatch]
        static class Patch_AriboatRaceController_control
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(AriboatRaceController));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("control"))
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
                return AccessTools.Method(typeof(AriboatRaceController), "control");
            }

            [HarmonyPrefix]
            static void Prefix(AriboatRaceController __instance)
            {

                //   __instance.Player.
                Sprite sprite = TextureReplacement.GetSprite(TextureReplacement.SpritesGrid, "PlayerOnAirboat");
                if (sprite != null)// && spriterenderer != null)
                {
                    SpriteAnimator[] SpriteAnimators = __instance.Player.GetComponentsInChildren<SpriteAnimator>();
                    for (int i = 0; i < SpriteAnimators.Length; i++)
                    {
                        if (SpriteAnimators[i].gameObject.name.Equals("Player"))
                        {
                            SpriteAnimators[i].Sprites.Clear();
                            int num = sprite.texture.width / 2;
                            int num2 = sprite.texture.height;
                            // spriterenderer.sprite = sprite;
                            for (int k = 0; k < 2; k++)
                            {
                                Rect rect2 = new Rect(k * num, 0, num, num2);
                                SpriteAnimators[i].Sprites.Add(Sprite.Create(sprite.texture, rect2, 0.5f * Vector2.one, 16f));
                            }
                           // SpriteAnimatorData data = Traverse.Create(SpriteAnimators[i]).Field("data").GetValue() as SpriteAnimatorData;
                           // data.Sprites=
                        }
                    }
                }

            }
        }


    }
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
