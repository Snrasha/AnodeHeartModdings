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


        [HarmonyPatch(typeof(BattleDirector), nameof(BattleDirector.SetTP))]
        static class Patch_BattleDirector_SetTP
        {
            [HarmonyPrefix]
            public static bool Prefix(BattleDirector __instance,int tp, bool playerTurn)
            {
                if (playerTurn && Plugin.ImpossibleSubMenuGUI.config.tp_limit>0)
                {
                    __instance.BattleTimeBar.SetTP(Plugin.ImpossibleSubMenuGUI.config.tp_limit - 1, playerTurn);
                    return false;
                }
                return true;
            }

        }
    }


    
}
