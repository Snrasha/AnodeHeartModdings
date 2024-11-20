
using Randomizer.Scripts.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace Randomizer.Scripts.Tama
{
    // [Serializable]
    public class RandomizerTama
    {
        RandomizerType types;
        RandomizerPassive passives;
        RandomizerAffinity affinitys;
        RandomizerDrops drops;

        public void Randomize()
        {
            if(types == null)
            {
                types = new RandomizerType();
                passives = new RandomizerPassive();
                affinitys = new RandomizerAffinity();
                drops = new RandomizerDrops();

            }
            types.Randomize();
            passives.Randomize();
            affinitys.Randomize();
            drops.Randomize();

        }

    }
}
