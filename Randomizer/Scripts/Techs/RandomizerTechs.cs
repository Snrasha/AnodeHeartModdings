
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace Randomizer.Scripts.Techs
{
    public class RandomizerTechs
    {
        public List<Tech> techsLearnable;

        private void RandomizeTechs(List<LevelUpTech> levels)
        {
            int size = levels.Count;
            int r = UnityEngine.Random.Range(0, 3);
            int[] steps = new int[size + r];

            int i = 0;
            foreach (LevelUpTech t in levels)
            {
                steps[i] = t.Level;
                i++;
            }
        }
        private void RandTechCrazyWild(MonsterLibrary ml)
        {
            List<LevelUpTech> levelUpTeches = ml.Techs;
            HashSet<int> currentTechs = new HashSet<int>();

            levelUpTeches.Clear();

            int nb = UnityEngine.Random.Range(8, 12);

            int level = 1;
            int previousincrease = 0;

            for (int i = 0; i < nb; i++)
            {
                int tindex;
                do
                {
                    tindex = UnityEngine.Random.Range(1, Plugin.techsLen);
                } while (currentTechs.Contains(tindex));

                levelUpTeches.Add(new LevelUpTech((Tech)tindex, level));
                currentTechs.Add(tindex);
                previousincrease = previousincrease + UnityEngine.Random.Range(1, 3);
                if (previousincrease > 5)
                {
                    previousincrease = 5;
                }
                level = level + previousincrease;

            }
        }

        private void RandTechCrazyWild()
        {
            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                RandTechCrazyWild(ml);
            }
        }

        private void RandTechWild(MonsterLibrary ml)
        {
            List<LevelUpTech> levelUpTeches = ml.Techs;

            HashSet<int> currentTechs = new HashSet<int>();

            levelUpTeches.Clear();

            int nb=UnityEngine.Random.Range(8, 12);

            int level = 1;
            int previousincrease = 0;

            for (int i = 0; i < nb; i++)
            {
                int tindex;
                do
                {
                    tindex = UnityEngine.Random.Range(0, techsLearnable.Count);
                } while (currentTechs.Contains(tindex));


                    //  Tech tech=
                levelUpTeches.Add(new LevelUpTech(techsLearnable[tindex], level));
                currentTechs.Add(tindex);
                previousincrease = previousincrease + UnityEngine.Random.Range(1, 3);
                if (previousincrease > 5)
                {
                    previousincrease = 5;
                }
                level = level + previousincrease;

            }
        }
        

        private void RandTechWild()
        {
            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                RandTechWild(ml);
            }
        }
        private void RandTechBalanced(Species specie,MonsterLibrary ml)
        {
            List<LevelUpTech> levelUpTeches = ml.Techs;
            HashSet<int> currentTechs = new HashSet<int>();

            DamageType DamageTypetama;
            if( ml.Affinity.Magic * 0.7f > ml.Affinity.Strength)
            {
                DamageTypetama = DamageType.Magical;
            }
            else if (ml.Affinity.Strength * 0.7f > ml.Affinity.Magic)
            {
                DamageTypetama = DamageType.Brutal;
            }
            else
            {
                DamageTypetama = DamageType.Mixed;
            }


            levelUpTeches.Clear();

            int nb = UnityEngine.Random.Range(8, 12);

            int level = 1;
            int previousincrease = 0;

            for (int i = 0; i < nb; i++)
            {
                int tindex;
                do
                {

                    //50% to prefer a type.
                    int typepref = UnityEngine.Random.Range(0, 2);
                    int focusdamagepref = UnityEngine.Random.Range(0, 3);

                    tindex = UnityEngine.Random.Range(0, techsLearnable.Count);
                    int test = 0;
                    if (typepref == 0)
                    {
                        while (TechLibrary.Data[techsLearnable[tindex]].Element!= ml.Element && test<10)
                        {
                            tindex = UnityEngine.Random.Range(0, techsLearnable.Count);
                            test++;
                        }
                    }
                    test = 0;
                    if (focusdamagepref == 0)
                    {
                        while (TechLibrary.Data[techsLearnable[tindex]].DamageType != DamageTypetama && test < 10)
                        {
                            tindex = UnityEngine.Random.Range(0, techsLearnable.Count);
                            test++;
                        }
                    }

                } while (currentTechs.Contains(tindex));


                //if (specie == Species.Saplee) {
                //    Debug.Log("Saplee: lv" + level + ", " + techsLearnable[tindex]);
                //}

                levelUpTeches.Add(new LevelUpTech(techsLearnable[tindex], level));
                currentTechs.Add(tindex);
                previousincrease = previousincrease + UnityEngine.Random.Range(1, 3);
                if (previousincrease > 5)
                {
                    previousincrease = 5;
                }
                level = level + previousincrease;

            }
        }

        private void RandTechBalanced()
        {
            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                RandTechBalanced((Species)i,ml);
            }
        }
        private void EveryTechAreUsable()
        {
            for (int i = 1; i < Plugin.techsLen; i++)
            {
                TechLibrary tec = TechLibrary.Data[(Tech)i];
                tec.SpeciesExclusive = Species.None;
            }
        }

        public void Randomize()
        {
            if (Plugin.configManager.Rand_Tech_DropDown.Value == Config.Enums.LearnSet.Disabled)
            {
                return;
            }
            Plugin.randomizerSeed.RunSeeded(DelegateRand);
        }
        private bool DelegateRand()
        {
            if (techsLearnable == null)
            {
                techsLearnable = new List<Tech>();
                for (int i = 1; i < Plugin.speciesLen; i++)
                {
                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                    List<LevelUpTech> levelUpTeches = ml.Techs;
                    foreach (LevelUpTech t in levelUpTeches)
                    {
                        techsLearnable.Add(t.Tech);
                    }

                    techsLearnable = techsLearnable.Distinct().ToList();
                }
            }
            EveryTechAreUsable();
            if (Plugin.configManager.Rand_Tech_DropDown.Value == Config.Enums.LearnSet.Balanced)
            {
                RandTechBalanced();
            }
            else if (Plugin.configManager.Rand_Tech_DropDown.Value == Config.Enums.LearnSet.WildCrazy)
            {
                RandTechCrazyWild();
            }
            else
            {
                RandTechWild();
            }
            return true;
        }


        //public void Randomize()
        //{


        //    //if (Plugin.configManager.t.config.reroll)
        //    //{
        //    //    techs = new List<LevelUpTech>[Plugin.speciesLen-1];

        //    //    for (int i = 1; i < Plugin.speciesLen; i++)
        //    //    {
        //    //        MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
        //    //        techs[i-1] = ml.Techs;
        //    //        RandomizeTechs(techs[i - 1]);

        //    //    }
        //    //}
        //    //else if (techs != null)
        //    //{
        //    //    for (int i = 1; i < Plugin.speciesLen; i++)
        //    //    {
        //    //        MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
        //    //        ml.Techs = techs[i - 1];
        //    //    }
        //    //}

        //}

    }
}
