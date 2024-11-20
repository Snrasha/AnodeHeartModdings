
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Randomizer.Scripts.Items
{
    //  [Serializable]
    public class RandomizerFishing
    {

        public void Randomize()
        {
        
            Plugin.randomizerSeed.RunSeeded(DelegateRand);
        }
        private bool DelegateRand()
        {
            if (Plugin.configManager.Rand_Fishing1_DropDown.Value)
            {
                if (Plugin.configManager.Rand_Spawn_Dropdown.Value != Config.Enums.Encounters.Disabled)
                {
                    RandTama();
                }

                RandItems();
            }
            


            return true;
        }

      

        private void RandTama()
        {
         //   Plugin.randomizerSpawn.species;

            foreach(List<FishingData> value in FishingLibrary.Data.Values)
            {
                foreach(FishingData data in value)
                {
                    if (data.Item != ItemType.None)
                    {
                        data.Tama = Plugin.randomizerSpawn.species[(int)data.Tama];
                    }
                }
            }
        }
        private void RandItems()
        {
            //   Plugin.randomizerSpawn.species;

            foreach (List<FishingData> value in FishingLibrary.Data.Values)
            {
                foreach (FishingData data in value)
                {
                    if (data.Tama != Species.None)
                    {
                        data.Item = GetRandomizedItem(data);
                    }
                }
            }
        }
        private ItemType GetRandomizedItem(FishingData fishingdata)
        {
            int shift = fishingdata.Difficulty * 42 + (int)(fishingdata.Chance * 14) + (int)fishingdata.Item;
            // int shift = ((int)previous )+ quantity * 10 + (selling ? 42 : 0)+ shopItem.gameObject.name.GetHashCode();
            //item

            return Plugin.randomizerItems.GetRandomizedItem(shift);
        }

    }
}
