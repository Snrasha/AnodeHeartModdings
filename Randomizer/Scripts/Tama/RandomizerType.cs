
using System;
using System.Linq;

namespace Randomizer.Scripts.Items
{
    public class RandomizerType
    {
        public Element[] elements;

        private Element[] bannedListTypes = new Element[]
        {
           Element.Any,
           Element.Null,
           Element.Virtual,
        };
        private Element[] wildBannedListTypes = new Element[]
        {
           Element.Any,
        };
        private void RandOneElement(Element[] banned, int len, int i)
        {
            do
            {
                elements[i - 1] = (Element)UnityEngine.Random.Range(0, len);
            }
            while (bannedListTypes.Contains(elements[i - 1]));
        }
        private void RandElementWild()
        {
            int elemlen = Enum.GetNames(typeof(Element)).Length;

            elements = new Element[Plugin.speciesLen - 1];

            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                RandOneElement(wildBannedListTypes, elemlen, i);
                if (wildBannedListTypes.Contains(ml.Element))
                {
                    continue;
                }
                ml.Element = elements[i - 1];
            }

            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                if (wildBannedListTypes.Contains(ml.Element))
                {
                    continue;
                }
                ml.Element = elements[i - 1];

            }
        }
        private void RandElementLimited()
        {
            int elemlen = Enum.GetNames(typeof(Element)).Length;

            elements = new Element[Plugin.speciesLen - 1];

            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                RandOneElement(bannedListTypes, elemlen, i);
                if (bannedListTypes.Contains(ml.Element))
                {
                    continue;
                }
                ml.Element = elements[i - 1];
            }

            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];
                if (bannedListTypes.Contains(ml.Element))
                {
                    continue;
                }
                ml.Element = elements[i - 1];

            }

        }
        public void Randomize()
        {
            if (Plugin.configManager.Rand_Type_DropDown.Value == Config.Enums.ElementType.Disabled)
            {
                return;
            }
            Plugin.randomizerSeed.RunSeeded(DelegateRand);
        }
        private bool DelegateRand()
        {
            if (Plugin.configManager.Rand_Type_DropDown.Value == Config.Enums.ElementType.Limited)
            {
                RandElementLimited();
            }
            else
            {
                RandElementWild();
            }
            return true;
        }
    }
}