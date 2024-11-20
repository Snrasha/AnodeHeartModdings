
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Randomizer.Scripts.Encounter
{
  //  [Serializable]
    public class RandomizerSpecies
    {
    //    [NonSerialized]
        public Species[] limited_species;

      //  [NonSerialized]
        private Species[] restOfSpecies = new Species[]
        {
              Species.Cardee_2,
    Species.Cardee_3 ,
    Species.Cardee_4 ,
    Species.Cardee_5,
    Species.Cardee_6,
        };
        private Species randomizedToReturn;

        public RandomizerSpecies()
        {
            Species[] dexorder = MonsterLibrary.DexOrder;
            limited_species = dexorder.Concat(restOfSpecies).ToArray();
        }



        public Species GetRandomizedSpecies(int shift)
        {
            Plugin.randomizerSeed.RunSeeded(DelegateRand1, shift);
            return randomizedToReturn;
        }
        public bool DelegateRand1()
        {
           randomizedToReturn = limited_species[UnityEngine.Random.Range(0, limited_species.Length)];
            return true;
        }
    }
}
