using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrama.Json
{
    [Serializable]
    public class Others
    {
        [Serializable]
        public class NameClass
        {
            public string en;
            public string fr;
        }
        [Serializable]
        public class DexClass
        {
            public string en;
            public string fr;
        }
        [Serializable]

        public class EditSpeciesClass
        {
            public Species Species;
            public List<Evolution> Evolutions = new List<Evolution>();
            public bool SwitchAncestorToNew;
        }
        public NameClass Name;
        public DexClass Dex;
        public EditSpeciesClass EditSpecie;



    }
}
