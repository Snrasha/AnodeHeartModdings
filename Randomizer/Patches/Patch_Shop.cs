using UnityEngine;
using HarmonyLib;
using Randomizer;
using Randomizer.Scripts;
using System.Linq;

namespace Randomizer.Patches
{

    public class Patch_ShopItem
    {



        [HarmonyPatch(typeof(ShopItem), nameof(ShopItem.Load))]
        static class Patch_ShopItem_Load
        {
            [HarmonyPrefix]
            public static bool Prefix(ShopItem __instance, ItemType item, int quantity, bool isSelling)
            {
                if (Plugin.configManager.Rand_Shop1_DropDown.Value && Plugin.configManager.Randomizer_toggle.Value)
                {
                    // RSpawner do not work with shopitem.
                    //if (__instance.GetComponent<RSpawner>() != null)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //__instance.gameObject.AddComponent<RSpawner>();

                    // do not randomize if on the banlist.
                    if (!Plugin.randomizerItems.banList.Contains(item))
                    {
                        Plugin.randomizerShop.SetRandomizedItem(__instance, item, quantity, isSelling);
                        return false;
                    }
                    //}
                }
                return true;
            }

        }
    }
  
    
}
