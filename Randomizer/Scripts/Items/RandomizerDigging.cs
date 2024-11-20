
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using static Randomizer.Scripts.Items.RandomizerItems;

namespace Randomizer.Scripts.Items
{
    //  [Serializable]
    public class RandomizerDigging
    {

        public void Randomize(ShovelingArea shovelingArea)
        {
            shovelingArea.Tama = Plugin.randomizerSpawn.species[(int)shovelingArea.Tama.ToEnum<Species>()].ToString();
            List<ShovelLoot> shovelLoots = shovelingArea.Loot;
            foreach (ShovelLoot loot in shovelLoots)
            {
                loot.Item = GetRandomizedItem(shovelingArea, loot).ToString();
            }
            foreach (QuestItm qitem in Plugin.randomizerItems.questItems)
            {
                if (qitem.digging && LevelLoader.CurrentLevel == qitem.level)
                {
                    ShovelLoot it=new ShovelLoot();
                    it.Chance = 0.1f;
                    shovelLoots.Add(it);
                }
            }

        }

        private ItemType GetRandomizedItem(ShovelingArea shovelingArea, ShovelLoot shovelLoot)
        {
            //2000 added for decrease luck to have the same seed than fishing.
            int shift = (int)(shovelLoot.Chance * 42) + (int)(shovelingArea.TamaSpawnChance * 12) + 5 * shovelingArea.Loot.Count + shovelingArea.MinLevel + shovelingArea.MaxLevel + shovelingArea.MaxExcavations;
            // int shift = ((int)previous )+ quantity * 10 + (selling ? 42 : 0)+ shopItem.gameObject.name.GetHashCode();
            //item

            return Plugin.randomizerItems.GetRandomizedItem(2000 + shift);
        }
    }
}
