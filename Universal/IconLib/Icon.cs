using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Universal.IconLib
{
    public class Icon
    {
        public string NameMod { get; set; }
        public string TrueNameMod { get; set; }
        public Sprite sprite { get; set; }
        public Icon() { }
        public Icon(string nameMod,string truenameMod,Sprite iconsprite)
        {
            NameMod = nameMod;

            TrueNameMod= truenameMod;
            sprite=iconsprite;
        }


    }
}
