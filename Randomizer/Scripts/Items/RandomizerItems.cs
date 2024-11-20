
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace Randomizer.Scripts.Items
{
    public class RandomizerItems
    {
        //  public SerializableDictionary<string, ShopStateData> ShopState = new SerializableDictionary<string, ShopStateData>();

        //ItemLibrary: UniqueArtifact

        public struct QuestItm
        {
           public ItemType item;
         //   public bool fishing;
            public bool digging;
            public LevelId level;
        }
        public ItemType[] start_questRequiredList = new ItemType[]
{
             ItemType.HashSalt,
            ItemType.Carapace,
            ItemType.Seaweed,
            ItemType.Pyrite,
            ItemType.Seashell
};
        public LevelId[] start_levels_digging = new LevelId[]
        {
            LevelId.CircuitShore,
            LevelId.MemoryLake,
            LevelId.KernelDump,
            LevelId.ProtocolTunnel

        };

        public QuestItm[] questItems { private set; get; }
        private ItemType randomizedToReturn;
        public ItemType[] banList = new ItemType[]
{
            ItemType.RedKey,
            ItemType.MetalCard,
            ItemType.SirioGoggles,
            ItemType.None,
};
        public void Randomize()
        {
            Plugin.randomizerSeed.RunSeeded(DelegateRand0);

        }
        public bool DelegateRand0()
        {
            questItems = new QuestItm[start_questRequiredList.Length];

            for (int i = 0; i < questItems.Length; i++)
            {
                questItems[i].item = start_questRequiredList[i];
                int lvl = UnityEngine.Random.Range(0, start_levels_digging.Length);
             //   questItems[i].fishing = false;
                questItems[i].digging = true;
                questItems[i].level = start_levels_digging[lvl];

                //int 

                // questItems[i].
            }
            return true;
        }

        public ItemType GetRandomizedItem(int shift)
        {
            Plugin.randomizerSeed.RunSeeded(DelegateRand1, shift);
            return randomizedToReturn;
        }
        public bool DelegateRand1()
        {
            do
            {
                randomizedToReturn = (ItemType)UnityEngine.Random.Range(1, Plugin.itemLen);

            } while (banList.Contains(randomizedToReturn));
            return true;
        }

        //NPCItem need to randomize it
    }
}
