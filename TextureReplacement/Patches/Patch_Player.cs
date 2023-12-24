using UnityEngine;
using HarmonyLib;
using System.Security.Policy;

namespace TextureReplacement.Patches
{



    [HarmonyPatch(typeof(GameCharacterAnimator), nameof(GameCharacterAnimator.Load))]
    static class Patch_GameCharacterAnimator_Load
    {
        static Texture2D GetTexture(GameCharacterAnimation gameCharacterAnimation)
        {


            if (TextureReplacement.SpritesAnimationPlayer.ContainsKey(gameCharacterAnimation.Type))
            {
                Sprite spriteb = TextureReplacement.SpritesAnimationPlayer[gameCharacterAnimation.Type];
                //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
                //  Sprite sprite = Sprite.Create(spriteb.texture, spriteb.rect, spriteb.pivot, 16);
                return spriteb.texture;
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
                    Texture2D texture2D= GetTexture(gameCharacterAnimation);
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
