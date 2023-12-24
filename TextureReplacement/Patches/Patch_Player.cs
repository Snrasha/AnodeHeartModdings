using UnityEngine;
using HarmonyLib;
using System.Security.Policy;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace TextureReplacement.Patches
{
    //[HarmonyPatch]
    //static class Patch_BetterMonoBehaviour_AddTempComponent
    //{
    //    [HarmonyTargetMethod]
    //    static public MethodBase TargetMethod()
    //    {
    //        List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(BetterMonoBehaviour));
    //        foreach (MethodInfo method in methods)
    //        {
    //            if (method.Name.Equals("AddTempComponent"))
    //            {
    //                if (method.IsGenericMethod)
    //                {
    //                    if (method.GetParameters().Length == 0)
    //                    {
    //                        return method;
    //                    }
    //                }
    //            }
    //        }
    //        return AccessTools.Method(typeof(BetterMonoBehaviour), "AddTempComponent");
    //    }


    //    static Texture2D GetTexture(GameCharacterAnimation gameCharacterAnimation)
    //    {


    //        if (TextureReplacement.SpritesScooterAnimationPlayer.ContainsKey(gameCharacterAnimation.Type))
    //        {
    //            return TextureReplacement.SpritesScooterAnimationPlayer[gameCharacterAnimation.Type];
    //        }
    //        return null;
    //    }


    //    [HarmonyPostfix]
    //    static void Postfix<VehicleController>(BetterMonoBehaviour __instance)
    //    {


    //        if (__instance.animator.animations != null)
    //        {
    //            GameCharacterAnimation[] array = __instance.animator.animations;
    //            foreach (GameCharacterAnimation gameCharacterAnimation in array)
    //            {
    //                Texture2D texture2D = GetTexture(gameCharacterAnimation);
    //                if (texture2D == null)
    //                {
    //                    continue;
    //                }

    //                gameCharacterAnimation.Texture = texture2D;


    //                int num = gameCharacterAnimation.Texture.width / gameCharacterAnimation.Frames;
    //                int num2 = gameCharacterAnimation.Texture.height / 4;
    //                //gameCharacterAnimation.IsSimpleSheet = false;
    //                if (gameCharacterAnimation.IsSimpleSheet)
    //                {
    //                    num2 = gameCharacterAnimation.Texture.height;
    //                    gameCharacterAnimation.sprites[0] = new Sprite[gameCharacterAnimation.Frames];
    //                    for (int j = 0; j < gameCharacterAnimation.Frames; j++)
    //                    {
    //                        Rect rect = new Rect(j * num, 0f, num, num2);
    //                        gameCharacterAnimation.sprites[0][j] = Sprite.Create(gameCharacterAnimation.Texture, rect, 0.5f * Vector2.one, 16f);
    //                    }
    //                    continue;
    //                }
    //                for (int k = 0; k < 4; k++)
    //                {
    //                    gameCharacterAnimation.sprites[k] = new Sprite[gameCharacterAnimation.Frames];
    //                    for (int l = 0; l < gameCharacterAnimation.Frames; l++)
    //                    {
    //                        Rect rect2 = new Rect(l * num, (3 - k) * num2, num, num2);
    //                        gameCharacterAnimation.sprites[k][l] = Sprite.Create(gameCharacterAnimation.Texture, rect2, 0.5f * Vector2.one, 16f);
    //                    }
    //                }
    //            }






    //        }
    //    }
    ////}
    //[HarmonyPatch(typeof(SpriteLoader), nameof(SpriteLoader.LoadSprite), new[] { typeof(string) })]
    //static class Patch_SpriteLoader_LoadSprite
    //{
    //    [HarmonyPrefix]
    //    static bool Prefix(ref Sprite __result, string path)
    //    {
    //        if (path.Contains("Cursor"))
    //        {
    //            Debug.Log("Patch_SpriteLoader_LoadSprite " + path);
    //        }
    //        //if (path.EndsWith("Player_Scooter"))
    //        //{
    //        //    __result = TextureReplacement.GetSprite(TextureReplacement.SpritesScooterAnimationPlayer, GameCharacterAnimationType.Walk);
    //        //}
    //        //else if (path.EndsWith("Player_Scooter_Idle"))
    //        //{
    //        //    __result = TextureReplacement.GetSprite(TextureReplacement.SpritesScooterAnimationPlayer, GameCharacterAnimationType.Idle);
    //        //}
    //        return __result == null;
    //    }
    //}

    [HarmonyPatch(typeof(GameCharacterAnimator), nameof(GameCharacterAnimator.Load))]
    static class Patch_GameCharacterAnimator_Load
    {
        static Texture2D GetTexture(GameCharacterAnimation gameCharacterAnimation)
        {


            if (TextureReplacement.SpritesAnimationPlayer.ContainsKey(gameCharacterAnimation.Type))
            {
                return TextureReplacement.SpritesAnimationPlayer[gameCharacterAnimation.Type];
            }
            return null;
        }
        static Texture2D GetScooterTexture(GameCharacterAnimation gameCharacterAnimation)
        {


            if (TextureReplacement.SpritesScooterAnimationPlayer.ContainsKey(gameCharacterAnimation.Type))
            {
                return TextureReplacement.SpritesScooterAnimationPlayer[gameCharacterAnimation.Type];
            }
            return null;
        }

        [HarmonyPostfix]
        static void Postfix(GameCharacterAnimator __instance)
        {

            if (__instance.animations != null && __instance.gameObject.name.Equals("Player")) {


                GameCharacterAnimation[] array = __instance.animations;


                foreach (GameCharacterAnimation gameCharacterAnimation in array)
                {
                    Debug.Log("Player " + gameCharacterAnimation.Type);

                    Texture2D texture2D;

                    if (array.Length == 2)
                    {
                        texture2D = GetScooterTexture(gameCharacterAnimation);

                    }
                    else
                    {
                        texture2D = GetTexture(gameCharacterAnimation);
                    }
                    if (texture2D == null)
                    {
                        continue;
                    }

                    gameCharacterAnimation.Texture = texture2D;


                    int num = gameCharacterAnimation.Texture.width / gameCharacterAnimation.Frames;
                    int num2 = gameCharacterAnimation.Texture.height / 4;
                    //gameCharacterAnimation.IsSimpleSheet = false;
                    if (gameCharacterAnimation.IsSimpleSheet)
                    {
                        num2 = gameCharacterAnimation.Texture.height;
                        gameCharacterAnimation.sprites[0] = new Sprite[gameCharacterAnimation.Frames];
                        for (int j = 0; j < gameCharacterAnimation.Frames; j++)
                        {
                            Rect rect = new Rect(j * num, 0f, num, num2);
                            gameCharacterAnimation.sprites[0][j] = Sprite.Create(gameCharacterAnimation.Texture, rect, 0.5f * Vector2.one, 16f);
                        }
                        continue;
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        gameCharacterAnimation.sprites[k] = new Sprite[gameCharacterAnimation.Frames];
                        for (int l = 0; l < gameCharacterAnimation.Frames; l++)
                        {
                            Rect rect2 = new Rect(l * num, (3 - k) * num2, num, num2);
                            gameCharacterAnimation.sprites[k][l] = Sprite.Create(gameCharacterAnimation.Texture, rect2, 0.5f * Vector2.one, 16f);
                        }
                    }
                }






            }
        }
    }


    //public static Sprite LoadCharacterIcon(string id, string suffix = "")
    //{
    //    suffix = ((suffix == "" || suffix == null) ? "" : ("_" + suffix));
    //    return LoadAny("Characters/Icons/" + id + suffix, "Monsters/Icons/" + id + suffix);
    //}
}
