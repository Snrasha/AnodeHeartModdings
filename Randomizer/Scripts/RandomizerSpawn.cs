
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Randomizer.Scripts
{
    [Serializable]
    public class RandomizerSpawn
    {
        public Species[] species;


        [NonSerialized]
        private Species[] restOfSpecies = new Species[]
        {
              Species.Cardee_2,
    Species.Cardee_3 ,
    Species.Cardee_4 ,
    Species.Cardee_5,
    Species.Cardee_6,
        };


        public void Randomize()
        {
            if (Plugin.configManager.Rand_Spawn_Dropdown.Value == Config.Enums.Spawn_enums.Disabled)
            {
                return;
            }

            if (Plugin.configManager.Randomizer_reroll.Value || species == null)
            {
                if (Plugin.configManager.Rand_Spawn_Dropdown.Value == Config.Enums.Spawn_enums.Limited)
                {
                    RandLimited();
                }
                else
                {
                    RandWild();
                }
            }

        }
        private void RandLimited()
        {
            Species[] dexorder = MonsterLibrary.DexOrder;
            Species[] choices = dexorder.Concat(restOfSpecies).ToArray();
            List<Species> list = new List<Species>();
            List<Species> list2 = new List<Species>();

            for (int i = 0; i < choices.Length; i++)
            {
                list.Add(choices[i]);
            }
            species = new Species[Plugin.speciesLen];

            // We replace only tama from the dex order.
            for (int i = 0; i < Plugin.speciesLen; i++)
            {
                Species target = (Species)(i);

                if (dexorder.Contains(target))
                {
                    int r = UnityEngine.Random.Range(0, list.Count);
                    species[i] = list[r];
                    list.RemoveAt(r);
                    list2.Add(species[i]);
                    if (list.Count == 0)
                    {
                        list = list2;
                    }
                }
                else
                {
                    species[i] = (Species)(i);

                }
            }
        }

        private void RandWild()
        {
            List<Species> list = new List<Species>();
            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                list.Add((Species)(i));
            }
            species = new Species[list.Count];
            int inc = 0;
            while (list.Count > 0)
            {
                int r = UnityEngine.Random.Range(0, list.Count);
                species[inc] = list[r];
                list.RemoveAt(r);
                inc++;
            }
        }

    }
}
