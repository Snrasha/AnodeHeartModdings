using UnityEngine;
using HarmonyLib;
using Randomizer;
using Randomizer.Scripts;
using System.Linq;

namespace Randomizer.Patches
{

    public class Patch_GroundItems
    {



        [HarmonyPatch(typeof(NPCItem), nameof(NPCItem.Start))]
        static class Patch_NPCItem_Start
        {
            [HarmonyPrefix]
            public static void Prefix(NPCItem __instance)
            {
                if (Plugin.configManager.Rand_GroundItems_Toggle.Value && Plugin.configManager.Randomizer_toggle.Value)
                {
                    if (__instance.GetComponent<RSpawner>() != null)
                    {
                        return;
                    }
                    else
                    {
                        ItemType type = (__instance.ItemId.ToEnum<ItemType>());
                        if (!Plugin.randomizerItems.banList.Contains(type))
                        {
                            if (!__instance.OneTimeItem)
                            {

                                __instance.ItemId = Plugin.randomizerItems.GetRandomizedItem(10000 + __instance.ID.GetHashCode()).ToString();
                                __instance.gameObject.AddComponent<RSpawner>();

                            }
                            else
                            {
                                Debug.Log("Unique item to ban: " + __instance.ItemId);
                            }
                        }

                    }
                }
            }

        }
    }
  
    
}
