
using System;

using System.Linq;
namespace Randomizer.Scripts.Items
{
    public class RandomizerDrops
    {


        private void RandDrop()
        {
            for (int i = 1; i < Plugin.speciesLen; i++)
            {
                MonsterLibrary ml = MonsterLibrary.Data[(Species)i];

                int number = 0;
                foreach(MonsterDrop monsterDrop in ml.Drops)
                {
                    monsterDrop.Item = GetRandomizedItem(i, number, monsterDrop.Chance);
                    number++;
                }

            }

        }
        private ItemType GetRandomizedItem(int idx,int number, float chance)
        {
            //2000 added for decrease luck to have the same seed than fishing.
            int shift = (int)(number * 600) + (int)(chance * 600) + idx;
            // int shift = ((int)previous )+ quantity * 10 + (selling ? 42 : 0)+ shopItem.gameObject.name.GetHashCode();
            //item

            return Plugin.randomizerItems.GetRandomizedItem(8000+shift);
        }


        public void Randomize()
        {
            if (!Plugin.configManager.Rand_Drops_DropDown.Value)
            {
                return;
            }
            Plugin.randomizerSeed.RunSeeded(DelegateRand);
        }
        private bool DelegateRand()
        {

            RandDrop();

            return true;
        }
    }
}
