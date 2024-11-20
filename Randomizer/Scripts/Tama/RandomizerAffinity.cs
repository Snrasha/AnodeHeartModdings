
using System;

using System.Linq;

namespace Randomizer.Scripts
{
   // [Serializable]
    public class RandomizerAffinity
    {
        public MonsterStatsAffinity[] stats;
        public float[] baseHP;
      
        private void RandomizeAffinity(MonsterStatsAffinity affinity)
        {

            //-50 is related to the base min +10 given on stats below.
            int sum = ((int)(affinity.Sum*100 - 50));

            // Make weightings
            double hpW = UnityEngine.Random.Range(0f,1f), StrengthW = UnityEngine.Random.Range(0f, 1f), MagicW = UnityEngine.Random.Range(0f, 1f);
            double WisdomW = UnityEngine.Random.Range(0f, 1f), SpeedW = UnityEngine.Random.Range(0f, 1f);

            double totW = hpW + StrengthW + MagicW + WisdomW + SpeedW;

            affinity.HP = (float)((Math.Max(1, Math.Round(hpW / totW * sum)) + 10)/100);
            affinity.Strength = (float)((Math.Max(1, Math.Round(StrengthW / totW * sum)) + 10)/100);
            affinity.Magic = (float)((Math.Max(1, Math.Round(MagicW / totW * sum)) + 10) / 100);
            affinity.Wisdom = (float)((Math.Max(1, Math.Round(WisdomW / totW * sum)) + 10) / 100);
            affinity.Speed = (float)((Math.Max(1, Math.Round(SpeedW / totW * sum)) + 10) / 100);
        }
        private void RandomizeAffinityWild(MonsterStatsAffinity affinity)
        {

            //-50 is related to the base min +10 given on stats below.
            int sum = ((int)(UnityEngine.Random.Range(3f,7f)*100 - 50));

            // Make weightings
            double hpW = UnityEngine.Random.Range(0f, 1f), StrengthW = UnityEngine.Random.Range(0f, 1f), MagicW = UnityEngine.Random.Range(0f, 1f);
            double WisdomW = UnityEngine.Random.Range(0f, 1f), SpeedW = UnityEngine.Random.Range(0f, 1f);

            double totW = hpW + StrengthW + MagicW + WisdomW + SpeedW;

            affinity.HP = (float)((Math.Max(1, Math.Round(hpW / totW * sum)) + 10) / 100);
            affinity.Strength = (float)((Math.Max(1, Math.Round(StrengthW / totW * sum)) + 10) / 100);
            affinity.Magic = (float)((Math.Max(1, Math.Round(MagicW / totW * sum)) + 10) / 100);
            affinity.Wisdom = (float)((Math.Max(1, Math.Round(WisdomW / totW * sum)) + 10) / 100);
            affinity.Speed = (float)((Math.Max(1, Math.Round(SpeedW / totW * sum)) + 10) / 100);
        }

        public void Randomize()
        {
            if (Plugin.configManager.Rand_Stats_DropDown.Value == Config.Enums.Affinities.Disabled)
            {
                return;
            }
            Plugin.randomizerSeed.RunSeeded(DelegateRand);
        }
        private bool DelegateRand()
        {
            stats = new MonsterStatsAffinity[Plugin.speciesLen - 1];
            baseHP = new float[Plugin.speciesLen - 1];

            bool balanced = Plugin.configManager.Rand_Stats_DropDown.Value == Config.Enums.Affinities.Balanced;


            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];

                if (balanced)
                {
                    RandomizeAffinity(ml.Affinity);
                }
                else
                {
                    RandomizeAffinityWild(ml.Affinity);
                }

                stats[i - 1] = ml.Affinity;
                float r = UnityEngine.Random.Range(0.1f, 1f);
                ml.BaseHP = r * (ml.BaseHP) + ml.Affinity.HP * ml.BaseHP;
                baseHP[i - 1] = ml.BaseHP;
            }
            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                ml.Affinity = stats[i - 1];
                ml.BaseHP = baseHP[i - 1];
            }
            return true;
        }

    }
}
