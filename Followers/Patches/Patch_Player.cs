using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using Followers.Scripts;

namespace Followers.Patches
{
    public class LevelLoader_getAnchorPosition
    {
        [HarmonyPatch]
        static class Patch_LevelLoader_getAnchorPosition
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(LevelLoader));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("getAnchorPosition"))
                    {
                        if (method.ReturnType == typeof(Vector3))
                        {
                            if (method.GetParameters().Length == 1)
                            {
                                return method;
                            }
                        }
                    }
                }
                return AccessTools.Method(typeof(LevelLoader), "getAnchorPosition");
            }

            [HarmonyPrefix]
            static void Postfix(string anchorId)
            {
                GameObject orb = GameObject.FindGameObjectWithTag("Floaty");
                if (orb != null)
                {
                    FollowerGroup followerGroup = orb.transform.parent.GetComponentInChildren<FollowerGroup>();
                    if (followerGroup != null)
                    {
                        followerGroup.UnlockMovement();
                    }
                }
            }
        }


    }

    public class DaycareHUD_reload
    {
        [HarmonyPatch]
        static class Patch_DaycareHUD_reload
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(DaycareHUD));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("reload"))
                    {
                        if (method.ReturnType == typeof(void))
                        {
                            if (method.GetParameters().Length == 0)
                            {
                                return method;
                            }
                        }
                    }
                }
                return AccessTools.Method(typeof(DaycareHUD), "reload");
            }

            [HarmonyPostfix]
            static void Postfix()
            {
                FollowersPlugin.UpdateFollowersGroup();
            }
        }
    }
    public class DialogueCommander_takeTama
    {
        [HarmonyPatch]
        static class Patch_DialogueCommander_takeTama
        {
            [HarmonyTargetMethod]
            static public MethodBase TargetMethod()
            {
                List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(DialogueCommander));
                foreach (MethodInfo method in methods)
                {
                    if (method.Name.Equals("takeTama"))
                    {
                        if (method.ReturnType == typeof(bool))
                        {
                            if (method.GetParameters().Length == 1)
                            {
                                return method;
                            }
                        }
                    }
                }
                return AccessTools.Method(typeof(DialogueCommander), "takeTama");
            }

            [HarmonyPostfix]
            static void Postfix(string id)
            {
                FollowersPlugin.UpdateFollowersGroup();
            }
        }
    }

    [HarmonyPatch(typeof(PartySelectorHUD), nameof(PartySelectorHUD.ConfirmMove))]
    static class Patch_PartySelectorHUD_ConfirmMove
    {

        [HarmonyPostfix]
        static void Postfix()
        {
            FollowersPlugin.UpdateFollowersGroup();

        }
    }




    


    [HarmonyPatch(typeof(Player), nameof(Player.SwitchParty))]
    static class Patch_Player_SwitchParty
    {

        [HarmonyPostfix]
        static void Postfix()
        {
            FollowersPlugin.UpdateFollowersGroup();

        }
    }



    

    [HarmonyPatch(typeof(Player), nameof(Player.AddTama), new[] { typeof(Monster) })]
    static class Patch_Player_AddTama
    {

        [HarmonyPostfix]
        static void Postfix(Monster monster)
        {
            FollowersPlugin.UpdateFollowersGroup();

        }
    }
    [HarmonyPatch(typeof(Player), nameof(Player.AddTamaToBox), new[] { typeof(Monster) })]
    static class Patch_Player_AddTamaToBox
    {

        [HarmonyPostfix]
        static void Postfix(Monster monster)
        {
            FollowersPlugin.UpdateFollowersGroup();

        }
    }

    

    [HarmonyPatch(typeof(GameCharacterAnimator), nameof(GameCharacterAnimator.Load))]
    static class Patch_GameCharacterAnimator_Load
    {

        [HarmonyPostfix]
        static void Postfix(GameCharacterAnimator __instance)
        {
            if (__instance.animations != null && __instance.gameObject.name.Equals("Player"))
            {
                FollowersPlugin.UpdateFollowersGroup();
            }
        }
    }
}
