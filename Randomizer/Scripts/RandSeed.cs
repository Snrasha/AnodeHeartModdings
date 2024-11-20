using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.Random;

namespace Randomizer.Scripts
{
    [Serializable]
    public class RandSeed
    {
        public string seed;
        public State stateSaved;

        //[NonSerialized]
        //public bool spawn = false;
        //[NonSerialized]
        //public bool stats = false;
        //[NonSerialized]
        //public bool passif = false;
        //[NonSerialized]
        //public bool typetama = false;


        // Set the seed if empty, if reroll enable, set it. At the end, display the seed of the save on the config.
        public void Set()
        {
            string seed2 = Plugin.configManager.Randomizer_seed.Value;
           // this.seed = seed;
            if (this.seed == null || Plugin.configManager.Randomizer_reroll.Value)
            {
                this.seed = seed2;
                Plugin.configManager.Randomizer_reroll.Value = false;
                //    //typetama = false;
                //    //spawn = false;
                //    //stats = false;
                //    //passif = false;
            }
            Plugin.configManager.Randomizer_seed.Value = this.seed;

        }
        public void RunSeeded(Func<bool> call, int shiftseed=0)
        {
            Init(shiftseed);
            call();
            ResetToGameSeed();
        }

        public void Init(int shiftseed=0)
        {
            stateSaved = UnityEngine.Random.state;
            // UnityEngine.Random.
            UnityEngine.Random.InitState(seed.GetHashCode()+ shiftseed);
        }
        public void ResetToGameSeed()
        {
            UnityEngine.Random.state = stateSaved;
        }
    }
}
