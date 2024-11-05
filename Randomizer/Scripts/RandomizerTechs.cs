
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Randomizer.Scripts
{
    [Serializable]
    public class RandomizerTechs
    {
        public List<LevelUpTech>[] techs;

        private void RandomizeTechs(List<LevelUpTech> levels)
        {
            int size = levels.Count;
            int r=UnityEngine.Random.Range(0, 3);
            int[] steps= new int[size+r];

            int i = 0;
            foreach(LevelUpTech t in levels)
            {
                steps[i] = t.Level;
                i++;
            }


            


        }

        public void Randomize()
        {


            //if (Plugin.configManager.t.config.reroll)
            //{
            //    techs = new List<LevelUpTech>[Plugin.speciesLen-1];

            //    for (int i = 1; i < Plugin.speciesLen; i++)
            //    {
            //        MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
            //        techs[i-1] = ml.Techs;
            //        RandomizeTechs(techs[i - 1]);

            //    }
            //}
            //else if (techs != null)
            //{
            //    for (int i = 1; i < Plugin.speciesLen; i++)
            //    {
            //        MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
            //        ml.Techs = techs[i - 1];
            //    }
            //}

        }

    }
}
