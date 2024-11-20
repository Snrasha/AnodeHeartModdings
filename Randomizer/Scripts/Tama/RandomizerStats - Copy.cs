
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using UnityEngine;
//using static UnityEngine.GraphicsBuffer;
//using static UnityEngine.ParticleSystem.PlaybackState;

//namespace Randomizer.Scripts
//{
//   // [Serializable]
//    public class RandomizerStats
//    {
//        public MonsterStatsAffinity[] stats;
//        public Passive[] passives;
//        public float[] baseHP;

        
//        public Element[] elements;



//        [NonSerialized]
//        private Passive[] bannedList = new Passive[]
//        {
//            Passive.Indestructible,
//            Passive.Puppeteer,
//            Passive.Ultramech,
//            Passive.Heatscales,
//            Passive.Deepdiver,
//            Passive.Aurora,
//            Passive.Reinit,
//           Passive.Virtual,

//           Passive.Absorption,
//           Passive.Rainsong,
//           Passive.SamuraiSoul,
//           Passive.Honeypot,
//                    //   Passive.RockCall,
//         //Passive.Seeding,
//        // Passive.Recurrence,
//        };
//        [NonSerialized]
//        private Element[] bannedListTypes = new Element[]
//{
//           Element.Any,
//           Element.Null,
//           Element.Virtual,
//};
//        [NonSerialized]
//        private Element[] wildBannedListTypes = new Element[]
//{
//           Element.Any,
//};
//        private void RandomizeAffinity(MonsterStatsAffinity affinity)
//        {

//            //-50 is related to the base min +10 given on stats below.
//            int sum = ((int)(affinity.Sum*100 - 50));

//            // Make weightings
//            double hpW = UnityEngine.Random.Range(0f,1f), StrengthW = UnityEngine.Random.Range(0f, 1f), MagicW = UnityEngine.Random.Range(0f, 1f);
//            double WisdomW = UnityEngine.Random.Range(0f, 1f), SpeedW = UnityEngine.Random.Range(0f, 1f);

//            double totW = hpW + StrengthW + MagicW + WisdomW + SpeedW;

//            affinity.HP = (float)((Math.Max(1, Math.Round(hpW / totW * sum)) + 10)/100);
//            affinity.Strength = (float)((Math.Max(1, Math.Round(StrengthW / totW * sum)) + 10)/100);
//            affinity.Magic = (float)((Math.Max(1, Math.Round(MagicW / totW * sum)) + 10) / 100);
//            affinity.Wisdom = (float)((Math.Max(1, Math.Round(WisdomW / totW * sum)) + 10) / 100);
//            affinity.Speed = (float)((Math.Max(1, Math.Round(SpeedW / totW * sum)) + 10) / 100);
//        }
//        private void RandomizeAffinityWild(MonsterStatsAffinity affinity)
//        {

//            //-50 is related to the base min +10 given on stats below.
//            int sum = ((int)(UnityEngine.Random.Range(3f,7f)*100 - 50));

//            // Make weightings
//            double hpW = UnityEngine.Random.Range(0f, 1f), StrengthW = UnityEngine.Random.Range(0f, 1f), MagicW = UnityEngine.Random.Range(0f, 1f);
//            double WisdomW = UnityEngine.Random.Range(0f, 1f), SpeedW = UnityEngine.Random.Range(0f, 1f);

//            double totW = hpW + StrengthW + MagicW + WisdomW + SpeedW;

//            affinity.HP = (float)((Math.Max(1, Math.Round(hpW / totW * sum)) + 10) / 100);
//            affinity.Strength = (float)((Math.Max(1, Math.Round(StrengthW / totW * sum)) + 10) / 100);
//            affinity.Magic = (float)((Math.Max(1, Math.Round(MagicW / totW * sum)) + 10) / 100);
//            affinity.Wisdom = (float)((Math.Max(1, Math.Round(WisdomW / totW * sum)) + 10) / 100);
//            affinity.Speed = (float)((Math.Max(1, Math.Round(SpeedW / totW * sum)) + 10) / 100);
//        }

//        private void RandomizeStats( )
//        {
//            if (Plugin.configManager.Rand_Stats_DropDown.Value==Config.Enums.Stats_enums.Disabled)
//            {
//                return;
//            }
//            UnityEngine.Random.InitState(seed.GetHashCode());
//            if (Plugin.configManager.Randomizer_reroll.Value || stats == null || baseHP==null)
//            {

//                stats = new MonsterStatsAffinity[Plugin.speciesLen - 1];
//                baseHP = new float[Plugin.speciesLen - 1];

//                bool balanced = Plugin.configManager.Rand_Stats_DropDown.Value == Config.Enums.Stats_enums.Balanced;


//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];

//                    if (balanced)
//                    {
//                        RandomizeAffinity(ml.Affinity);
//                    }
//                    else
//                    {
//                        RandomizeAffinityWild(ml.Affinity);
//                    }

//                    stats[i - 1] = ml.Affinity;
//                    float r = UnityEngine.Random.Range(0.1f, 1f);
//                    ml.BaseHP = r * (ml.BaseHP) + ml.Affinity.HP * ml.BaseHP;
//                    baseHP[i - 1] = ml.BaseHP;
//                }
//            }
//            else if (stats != null && baseHP != null)
//            {
//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
//                    ml.Affinity = stats[i - 1];
//                    ml.BaseHP = baseHP[i - 1];
//                }
//            }
//        }
//        private void RandOnePassif(int len, int i)
//        {
//            do
//            {
//                passives[i - 1] = (Passive)UnityEngine.Random.Range(1, len);
//            }
//            while (bannedList.Contains(passives[i - 1]));
//        }
//        private void RandPassifLimited()
//        {
//            int passivelen = Enum.GetNames(typeof(Passive)).Length;

            
//            if (!Plugin.randomizerSeed.passif)
//            {
//                Plugin.randomizerSeed.passif = true;
//                Plugin.randomizerSeed.Init();
//                passives = new Passive[Plugin.speciesLen - 1];

//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];

//                    if (bannedList.Contains(ml.Passive))
//                    {
//                        continue;
//                    }
//                    RandOnePassif(passivelen, i);
//                    ml.Passive = passives[i - 1];
//                }
//            }
//            else if (passives != null)
//            {
//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];

//                    // if a new passive is banned, reroll the tama passive.
//                    if (bannedList.Contains(passives[i - 1]))
//                    {
//                        RandOnePassif(passivelen, i);
//                    }
//                    ml.Passive = passives[i - 1];

//                }
//            }
//        }
//        private void RandPassifWild()
//        {
//            int passivelen = Enum.GetNames(typeof(Passive)).Length;
//            if (!Plugin.randomizerSeed.passif)
//            {
//                Plugin.randomizerSeed.passif = true;
//                Plugin.randomizerSeed.Init();
//                passives = new Passive[Plugin.speciesLen - 1];

//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
//                    passives[i - 1] = (Passive)UnityEngine.Random.Range(1, passivelen);
//                    ml.Passive = passives[i - 1];
//                }
//            }
//            else if (passives != null)
//            {
//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
//                    ml.Passive = passives[i - 1];

//                }
//            }
//        }
//        public void RandomizePassif( )
//        {
//            // Need to add a blacklist, like ignore indestructible. Also, do not reroll if the tama has this passive.

//            if (Plugin.configManager.Rand_passive_toggle.Value==Config.Enums.Passive_enums.Disabled)
//            {
//                return;
//            }
//            this.seed = seed;
//            UnityEngine.Random.InitState(seed.GetHashCode());
//            if (Plugin.configManager.Rand_passive_toggle.Value == Config.Enums.Passive_enums.Limited)
//            {
//                RandPassifLimited();
//            }
//            else
//            {
//                RandPassifWild();
//            }

//        }
//        private void RandOneElement(Element[] banned,int len, int i)
//        {
//            do
//            {
//                elements[i - 1] = (Element)UnityEngine.Random.Range(0, len);
//            }
//            while (bannedList.Contains(passives[i - 1]));
//        }
//        private void RandElementWild()
//        {
//            int elemlen = Enum.GetNames(typeof(Element)).Length;
//            if (Plugin.configManager.Randomizer_reroll.Value || elements == null)
//            {
//                elements = new Element[Plugin.speciesLen - 1];

//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
//                    RandOneElement(wildBannedListTypes, elemlen, i);
//                    if (wildBannedListTypes.Contains(ml.Element))
//                    {
//                        continue;
//                    }
//                    ml.Element = elements[i - 1];
//                }
//            }
//            else if (elements != null)
//            {
//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
//                    if (wildBannedListTypes.Contains(ml.Element))
//                    {
//                        continue;
//                    }
//                    ml.Element = elements[i - 1];

//                }
//            }
//        }
//        private void RandElementLimited()
//        {
//            int elemlen = Enum.GetNames(typeof(Element)).Length;
//            if (Plugin.configManager.Randomizer_reroll.Value || elements == null)
//            {
//                elements = new Element[Plugin.speciesLen - 1];

//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
//                    RandOneElement(bannedListTypes, elemlen, i);
//                    if (bannedListTypes.Contains(ml.Element))
//                    {
//                        continue;
//                    }
//                    ml.Element = elements[i - 1];
//                }
//            }
//            else if (elements != null)
//            {
//                for (int i = 1; i < Plugin.speciesLen; i++)
//                {
//                    MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
//                    if (bannedListTypes.Contains(ml.Element))
//                    {
//                        continue;
//                    }
//                    ml.Element = elements[i - 1];

//                }
//            }
//        }
//        public void RandomizeTypes( )
//        {
//            // Need to add a blacklist, like ignore indestructible. Also, do not reroll if the tama has this passive.

//            if (Plugin.configManager.Rand_Type_DropDown.Value == Config.Enums.Type_enums.Disabled)
//            {
//                return;
//            }
//            this.seed = seed;
//            UnityEngine.Random.InitState(seed.GetHashCode());
//            if (Plugin.configManager.Rand_Type_DropDown.Value == Config.Enums.Type_enums.Limited)
//            {
//                RandElementLimited();
//            }
//            else
//            {
//                RandElementWild();
//            }

//        }
//        public void Randomize( )
//        {

//            RandomizeStats();
//            RandomizePassif(seed);
//            RandomizeTypes(seed);
//        }

//    }
//}
