using UnityEngine;
using HarmonyLib;
using Randomizer;
using Randomizer.Scripts;

namespace Randomizer.Patches
{

    public class Patch_EnemySpawner
    {



        [HarmonyPatch(typeof(EnemySpawner), nameof(EnemySpawner.spawn))]
        static class Patch_EnemySpawner_spawn
        {
            [HarmonyPrefix]
            public static void Prefix(EnemySpawner __instance)
            {
                if (Plugin.configManager.Rand_Spawn_Dropdown.Value!=Config.Enums.Spawn_enums.Disabled && Plugin.configManager.Randomizer_toggle.Value)
                {
                    if (__instance.GetComponent<RSpawner>() != null)
                    {
                        return;
                    }
                    else
                    {
                        __instance.SpeciesId = Plugin.randomizerSpawn.species[(int)__instance.SpeciesId.ToEnum<Species>()].ToString();
                        __instance.gameObject.AddComponent<RSpawner>();
                    }
                }
            }

        }
    }
  
    
}
