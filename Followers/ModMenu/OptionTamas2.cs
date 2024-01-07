
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Universal.ModMenuLib;

namespace Followers.ModMenu
{
    public class OptionTamas2 : SettinsOptionTogglable
    {
        public string[] species;
        public override void load()
        {
            GameState gameState = GameState.Instance();
            if (gameState == null || gameState.Data == null || gameState.Data.Player == null)
            {
                species= new string[] { FollowersPlugin.followersSubMenuGUI.config.option_species };
            }
            List<Monster> monsters1 = gameState.Data.Player.MainParty;
            List<Monster> monsters2 = gameState.Data.Player.BenchedParty;

            HashSet<string> strings = new HashSet<string>();

            foreach (Monster monster in monsters1)
            {
                strings.Add(monster.Species);
            }
            foreach (Monster monster in monsters2)
            {
                strings.Add(monster.Species);
            }
            strings.Add(FollowersPlugin.followersSubMenuGUI.config.option_species);
            species=strings.ToArray();
        }

        public override string[] getOptions()
        {

            return species;
        }

        public override int getStartingOption()
        {
            int i = 00;
            foreach(string specie in species)
            {
                if (specie.Equals(FollowersPlugin.followersSubMenuGUI.config.option_species))
                {
                    return i;
                }
                i++;
            }
            return i;
        }

        public override void selectOption(int option)
        {
            FollowersPlugin.followersSubMenuGUI.config.option_species = species[option];
        }
    }

}
