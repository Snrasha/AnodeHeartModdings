using UnityEngine;
using HarmonyLib;
using System.Security.Policy;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace Impossible.Patches
{

    public class Patch_BattleRulesLogicExtensions
    {



        [HarmonyPatch(typeof(BattleRulesLogicExtensions), nameof(BattleRulesLogicExtensions.CanUseTech))]
        static class Patch_BattleRulesLogicExtensions_CanUseTech
        {
            [HarmonyPrefix]
            public static bool Prefix(ref bool __result, Monster monster,  Tech tech, Monster foe, bool isTurnZero = false, bool isEnemy = false)
            {
                if (!isEnemy && Plugin.ImpossibleSubMenuGUI.config.tech_enabled==0)
                {
                    __result = false;
                    return false;
                }
                return true;
            }

        }
    }
    public class Patch_BattleDirector
    {


        //[HarmonyPatch(typeof(BattleDirector), nameof(BattleDirector.SetTP))]
        //static class Patch_BattleDirector_SetTP
        //{
        //    [HarmonyPrefix]
        //    public static bool Prefix(BattleDirector __instance,int tp, bool playerTurn)
        //    {
        //        if (playerTurn && Plugin.ImpossibleSubMenuGUI.config.tp_limit>0)
        //        {
        //            __instance.BattleTimeBar.SetTP(Plugin.ImpossibleSubMenuGUI.config.tp_limit - 1, playerTurn);
        //            return false;
        //        }
        //        return true;
        //    }

        //}

        [HarmonyPatch(typeof(BattleMonster), nameof(BattleMonster.StartTP), MethodType.Getter)]
        static class Patch_BattleMonster_StartTP
        {
            [HarmonyPostfix]
            public static void PostFix(BattleMonster __instance,ref int __result)
            {
                if (!__instance.isEnemy && Plugin.ImpossibleSubMenuGUI.config.tp_limit>0)
                {
                    __result = Plugin.ImpossibleSubMenuGUI.config.tp_limit - 1;
                }
            }

        }
        //[HarmonyPatch(typeof(BattleDirector), nameof(BattleDirector.AddTP),MethodType.Enumerator)]
        //static class Patch_BattleDirector_AddTP
        //{
        //    [HarmonyPrefix]
        //    public static IEnumerator Prefix(BattleDirector __instance, int tp, bool playerTurn)
        //    {
        //        if (playerTurn && Plugin.ImpossibleSubMenuGUI.config.tp_limit > 0)
        //        {
        //            yield return __instance.BattleTimeBar.AddTP(Plugin.ImpossibleSubMenuGUI.config.tp_limit - 1, playerTurn);

        //        }
        //        yield return null;
        //    }

        //}

    }


    
}
