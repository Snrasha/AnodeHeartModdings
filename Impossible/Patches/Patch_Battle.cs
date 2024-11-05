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
                if (!isEnemy && Plugin.impossibleConfigManager.Option_Stun_Tama.Value)
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


        [HarmonyPatch(typeof(BattleMonster), nameof(BattleMonster.StartTP), MethodType.Getter)]
        static class Patch_BattleMonster_StartTP
        {
            [HarmonyPostfix]
            public static void PostFix(BattleMonster __instance,ref int __result)
            {
                if (!__instance.isEnemy && Plugin.impossibleConfigManager.Option_TP_Limit.Value> 0)
                {
                    __result = Plugin.impossibleConfigManager.Option_TP_Limit.Value;
                }
            }

        }

    }


    
}
