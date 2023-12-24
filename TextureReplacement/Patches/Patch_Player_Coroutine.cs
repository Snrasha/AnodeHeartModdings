//using UnityEngine;
//using HarmonyLib;
//using System.Reflection;
//using System.Collections.Generic;
//using System.Collections;

//namespace TextureReplacement.Patches
//{



//    public class startMountAnimationEnumerator : IEnumerable
//    {
//        static Texture2D GetTexture(GameCharacterAnimation gameCharacterAnimation)
//        {


//            if (TextureReplacement.SpritesScooterAnimationPlayer.ContainsKey(gameCharacterAnimation.Type))
//            {
//                return TextureReplacement.SpritesScooterAnimationPlayer[gameCharacterAnimation.Type];
//            }
//            return null;
//        }


//        public IEnumerator enumerator;
//        public VehicleSwitcher __instance;
//        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
//        public IEnumerator GetEnumerator()
//        {

//            // We call the method IPlayerTactics() now.
//            while (enumerator.MoveNext())
//            {
//                var item = enumerator.Current;
//                yield return item;
//            }
//            Debug.Log("Patch_BattlePlayerSkillStateIPlayerTactics GetEnumerator");
//            if (__instance == null)
//            {
//                yield break;
//            }


//            // (IEnumerator)ITacticNameShow.Invoke(__instance)
//            Debug.Log("Patch_BattlePlayerSkillStateIPlayerTactics " + commander.tactics);
//            __instance.
//            if (__instance.get.animations != null)
//            {
//                GameCharacterAnimation[] array = __instance.animator.animations;
//                foreach (GameCharacterAnimation gameCharacterAnimation in array)
//                {
//                    Texture2D texture2D = GetTexture(gameCharacterAnimation);
//                    if (texture2D == null)
//                    {
//                        continue;
//                    }

//                    gameCharacterAnimation.Texture = texture2D;


//                    int num = gameCharacterAnimation.Texture.width / gameCharacterAnimation.Frames;
//                    int num2 = gameCharacterAnimation.Texture.height / 4;
//                    //gameCharacterAnimation.IsSimpleSheet = false;
//                    if (gameCharacterAnimation.IsSimpleSheet)
//                    {
//                        num2 = gameCharacterAnimation.Texture.height;
//                        gameCharacterAnimation.sprites[0] = new Sprite[gameCharacterAnimation.Frames];
//                        for (int j = 0; j < gameCharacterAnimation.Frames; j++)
//                        {
//                            Rect rect = new Rect(j * num, 0f, num, num2);
//                            gameCharacterAnimation.sprites[0][j] = Sprite.Create(gameCharacterAnimation.Texture, rect, 0.5f * Vector2.one, 16f);
//                        }
//                        continue;
//                    }
//                    for (int k = 0; k < 4; k++)
//                    {
//                        gameCharacterAnimation.sprites[k] = new Sprite[gameCharacterAnimation.Frames];
//                        for (int l = 0; l < gameCharacterAnimation.Frames; l++)
//                        {
//                            Rect rect2 = new Rect(l * num, (3 - k) * num2, num, num2);
//                            gameCharacterAnimation.sprites[k][l] = Sprite.Create(gameCharacterAnimation.Texture, rect2, 0.5f * Vector2.one, 16f);
//                        }
//                    }
//                }



//                __instance = null;
//        }
//    }


//    [HarmonyPatch]
//    static class Patch_VehicleSwitcher_startMountAnimation
//        {
//        [HarmonyTargetMethod]
//        static public MethodBase TargetMethod()
//        {
//            List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(VehicleSwitcher));
//            foreach (MethodInfo method in methods)
//            {
//                if (method.Name.Equals("startMountAnimation"))
//                {
//                    if (method.ReturnType == typeof(IEnumerator))
//                    {
//                        if (method.GetParameters().Length == 1)
//                        {
//                            return method;
//                        }
//                    }
//                }
//            }
//            return AccessTools.Method(typeof(VehicleSwitcher), "startMountAnimation");
//        }


//        static void Postfix(ref IEnumerator __result, VehicleSwitcher __instance)
//        {

//            Debug.Log("Patch_BattlePlayerSkillStateIPlayerTactics");
//            var myEnumerator = new startMountAnimationEnumerator() { enumerator = __result, __instance = __instance };
//            __result = myEnumerator.GetEnumerator();
//            Debug.Log("Patch_BattlePlayerSkillStateIPlayerTactics3");
//        }
//    }


//}
