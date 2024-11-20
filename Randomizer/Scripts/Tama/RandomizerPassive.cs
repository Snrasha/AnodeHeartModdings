
using System;

using System.Linq;

namespace Randomizer.Scripts
{
    public class RandomizerPassive
    {
        public Passive[] passives;
        private Passive[] bannedList = new Passive[]
        {
            Passive.Indestructible,
            Passive.Puppeteer,
            Passive.Ultramech,
            Passive.Heatscales,
            Passive.Deepdiver,
            Passive.Aurora,
            Passive.Reinit,
           Passive.Virtual,

           Passive.Absorption,
           Passive.Rainsong,
           Passive.SamuraiSoul,
           Passive.Honeypot,
                    //   Passive.RockCall,
         //Passive.Seeding,
        // Passive.Recurrence,
        };
        private void RandOnePassif(int len, int i)
        {
            do
            {
                passives[i - 1] = (Passive)UnityEngine.Random.Range(1, len);
            }
            while (bannedList.Contains(passives[i - 1]));
        }
        private void RandPassifLimited()
        {
            int passivelen = Enum.GetNames(typeof(Passive)).Length;

            passives = new Passive[Plugin.speciesLen - 1];

            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];

                if (bannedList.Contains(ml.Passive))
                {
                    continue;
                }
                RandOnePassif(passivelen, i);
                ml.Passive = passives[i - 1];
            }


            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];

                // if a new passive is banned, reroll the tama passive.
                if (bannedList.Contains(passives[i - 1]))
                {
                    RandOnePassif(passivelen, i);
                }
                ml.Passive = passives[i - 1];

            }

        }
        private void RandPassifWild()
        {
            int passivelen = Enum.GetNames(typeof(Passive)).Length;

            passives = new Passive[Plugin.speciesLen - 1];

            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                passives[i - 1] = (Passive)UnityEngine.Random.Range(1, passivelen);
                ml.Passive = passives[i - 1];
            }

            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                ml.Passive = passives[i - 1];

            }

        }
        
        public void Randomize()
        {
            if (Plugin.configManager.Rand_passive_toggle.Value == Config.Enums.PassiveTama.Disabled)
            {
                return;
            }
            Plugin.randomizerSeed.RunSeeded(DelegateRand);
        }
        private bool DelegateRand()
        {
            if (Plugin.configManager.Rand_passive_toggle.Value == Config.Enums.PassiveTama.Limited)
            {
                RandPassifLimited();
            }
            else
            {
                RandPassifWild();
            }
            return true;
        }

    }
}
