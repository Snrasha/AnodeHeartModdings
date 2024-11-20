using UnityEngine;
using HarmonyLib;
using Randomizer;
using Randomizer.Scripts;

namespace Randomizer.Patches
{

    public class Patch_Digging
    {



        [HarmonyPatch(typeof(ShovelingArea), nameof(ShovelingArea.OnTriggerEnter2D))]
        static class Patch_ShovelingArea_OnTriggerEnter2D
        {
            [HarmonyPrefix]
            public static void Prefix(ShovelingArea __instance, Collider2D col)
            {
                if (Plugin.configManager.Rand_Digging1_DropDown.Value && Plugin.configManager.Randomizer_toggle.Value)
                {
                    if (__instance.GetComponent<RSpawner>() != null)
                    {
                        return;
                    }
                    else
                    {
                    

                        __instance.gameObject.AddComponent<RSpawner>();
                      
                        Plugin.randomizerDigging.Randomize(__instance);

                    }

                }
            }

        }
    }
  
    
}
