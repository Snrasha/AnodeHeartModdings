using UnityEngine;
using HarmonyLib;
using Randomizer;

namespace Randomizer.Patches
{

    public class Patch_TrainerData
    {



        [HarmonyPatch(typeof(TrainerData), nameof(TrainerData.Start))]
        static class Patch_TrainerData_Start
        {
            [HarmonyPostfix]
            public static void Postfix(TrainerData __instance)
            {
                if (Plugin.configManager.Rand_Trainers_toggle.Value && Plugin.configManager.Randomizer_toggle.Value)
                {
                    int val = 0;
                    foreach(TrainerDatum trainerDatum in __instance.Data)
                    {
                        trainerDatum.Species = Plugin.randomizerSpecies.GetRandomizedSpecies(__instance.ID.GetHashCode() + val).ToString();
                        val++;
                    }
                    
                }
            }

        }
    }
  
    
}
