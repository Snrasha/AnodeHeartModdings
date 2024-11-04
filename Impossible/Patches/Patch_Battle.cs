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
                if (!isEnemy)
                {
                    __result = false;
                    return false;
                }
                return true;
            }

        }


    
    }

}
