using UnityEngine;
using HarmonyLib;
using Randomizer;

namespace Randomizer.Patches
{

    public class Patch_GameModeRules
    {



        [HarmonyPatch(typeof(GameModeRules), nameof(GameModeRules.GetStarters))]
        static class Patch_GameModeRules_GetStarters
        {
            [HarmonyPrefix]
            public static bool Prefix(ref Species[] __result)
            {
                if (Plugin.configManager.Rand_Starter_toggle.Value && Plugin.configManager.Randomizer_toggle.Value)
                {
                    __result = new Species[3];

                    int f1 = Random.Range(1, Plugin.speciesLen);
                    int f2;
                    int f3;
                    do
                    {
                        f2 = Random.Range(1, Plugin.speciesLen);
                    }
                    while (f2 == f1);
                    do
                    {
                        f3 = Random.Range(1, Plugin.speciesLen);
                    }
                    while (f2 == f3 || f3==f1);
                    __result[0] =(Species)f1;
                    __result[1] = (Species)f2;
                    __result[2] = (Species)f3;
                    return false;  
                }
                return true;
            }

        }
    }
  
    
}
