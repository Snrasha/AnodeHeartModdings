
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Randomizer.Scripts.Items
{
    //  [Serializable]
    public class RandomizerShop
    {
        //    [NonSerialized]
       // public ItemType[] itemTypes;

        //  [NonSerialized]


      
        public void Load(ShopItem shopItem, ItemType item, int quantity, bool isSelling)
        {
            shopItem.item = item;
            shopItem.quantity = quantity;
            shopItem.Icon.sprite = SpriteLoader.LoadItemIcon(item);
            if (item == ItemType.Money)
            {
                shopItem.Name.SetText(string.Format("{0} B$", quantity));
                shopItem.Quantity.SetText("");
                shopItem.QuantityShadow.SetText("");
            }
            else
            {
                shopItem.Quantity.SetText(quantity);
                shopItem.QuantityShadow.SetText(quantity);
                shopItem.Name.SetText(ItemLibrary.Data[item].Name);
            }
            if (shopItem.Price != null)
            {
                shopItem.Price.SetText(ItemLibrary.Data[item].Cost / ((!isSelling) ? 1 : 2));
            }
            if (shopItem.CartSelection != null)
            {
                shopItem.CartUnselect();
            }
        }

        public void SetRandomizedItem(ShopItem shopItem,ItemType previous, int quantity, bool selling)
        {
            //4000 added for decrease luck to have the same seed than fishing & digging.
            int shift = (int)shopItem.transform.localPosition.y + quantity * 10 + (selling ? 42 : 0) + shopItem.gameObject.name.GetHashCode();
           // int shift = ((int)previous )+ quantity * 10 + (selling ? 42 : 0)+ shopItem.gameObject.name.GetHashCode();
            //item
       
            ;
            Load(shopItem, Plugin.randomizerItems.GetRandomizedItem(4000 + shift), quantity,selling);
            //return randomizedToReturn;
        }
    
        //public void Randomize()
        //{
        //    if (Plugin.configManager.Rand_Shop_DropDown.Value == Config.Enums.Shop.Disabled)
        //    {
        //        return;
        //    }
        //    Plugin.randomizerSeed.RunSeeded(DelegateRand);
        //}



        //private bool DelegateRand()
        //{
        //    //if (Plugin.configManager.Rand_Shop_DropDown.Value == Config.Enums.Shop.Enabled)
        //    //{
        //    //    Rand();
        //    //}
        //    return true;
        //}
    }

    //    private void Rand()
    //    {



    //       // ItemType[] dexorder = MonsterLibrary.DexOrder;
    //        List<Species> list = new List<Species>();
    //        List<Species> list2 = new List<Species>();

    //        for (int i = 0; i < choices.Length; i++)
    //        {
    //            list.Add(choices[i]);
    //        }
    //        species = new Species[Plugin.speciesLen];

    //        // We replace only tama from the dex order.
    //        for (int i = 0; i < Plugin.speciesLen; i++)
    //        {
    //            Species target = (Species)i;

    //            if (dexorder.Contains(target))
    //            {
    //                int r = UnityEngine.Random.Range(0, list.Count);
    //                species[i] = list[r];
    //                list.RemoveAt(r);
    //                list2.Add(species[i]);
    //                if (list.Count == 0)
    //                {
    //                    list = list2;
    //                }
    //            }
    //            else
    //            {
    //                species[i] = (Species)i;

    //            }
    //        }
    //    }
    //}
}
