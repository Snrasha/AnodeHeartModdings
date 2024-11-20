
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Randomizer.Scripts.Encounter
{
    //  [Serializable]
    public class RandomizerSpawn
    {
        //    [NonSerialized]
        public Species[] species;
        public void Randomize()
        {
            if (Plugin.configManager.Rand_Spawn_Dropdown.Value == Config.Enums.Encounters.Disabled)
            {
                return;
            }
            Plugin.randomizerSeed.RunSeeded(DelegateRand);
        }
        private bool DelegateRand()
        {
            if (Plugin.configManager.Rand_Spawn_Dropdown.Value == Config.Enums.Encounters.Limited)
            {
                RandLimited();
            }
            else
            {
                RandWild();
            }
            return true;
        }

        private void RandLimited()
        {
            Species[] choices = Plugin.randomizerSpecies.limited_species;
            List<Species> list = new List<Species>();
            List<Species> list2 = new List<Species>();

            for (int i = 0; i < choices.Length; i++)
            {
                list.Add(choices[i]);
            }
            species = new Species[Plugin.speciesLen];

            // We replace only tama from the dex order + cardee.
            for (int i = 0; i < Plugin.speciesLen; i++)
            {
                Species target = (Species)i;

                if (choices.Contains(target))
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
                    species[i] = (Species)i;

                }
            }
        }

        private void RandWild()
        {
            List<Species> list = new List<Species>();
            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                list.Add((Species)i);
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
