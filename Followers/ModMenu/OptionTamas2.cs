
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Followers.ModMenu
{
    public class OptionTamas2 : SettingsOption
    {
        public string[] species;
        public override void load()
        {
            GameState gameState = GameState.Instance();
            if (gameState == null || gameState.Data == null || gameState.Data.Player == null)
            {
                species= new string[] { FollowersBehaviour.followersSubMenuGUI.config.option_species };
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
            strings.Add(FollowersBehaviour.followersSubMenuGUI.config.option_species);
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
                if (specie.Equals(FollowersBehaviour.followersSubMenuGUI.config.option_species))
                {
                    return i;
                }
                i++;
            }
            return i;
        }

        public override void selectOption(int option)
        {
            FollowersBehaviour.followersSubMenuGUI.config.option_species = species[option];
        }
    }

}
