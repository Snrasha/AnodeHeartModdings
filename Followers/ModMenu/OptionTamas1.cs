
using Followers.Langs;
using UnityEngine;
using UnityEngine.UI;

namespace Followers.ModMenu
{
    public class OptionTamas1 : SettingsOption
    {
        public override string[] getOptions()
        {
            return new string[2]
            {
            StringLocalizer.t(FollowersLang.Followers_option1_button_value_1),
            StringLocalizer.t(FollowersLang.Followers_option1_button_value_2)
            };
        }
        //public override void load()
        //{
        //    getStartingOption();
        //}

        public override int getStartingOption()
        {
            if (FollowersPlugin.followersSubMenuGUI.config.option_followers==0)
            {
                FollowersPlugin.followersSubMenuGUI.optionTamas2.DisableButton();

                return 0;
            }
            FollowersPlugin.followersSubMenuGUI.optionTamas2.EnableButton();

            return 1;
        }
      //  public override 


        public override void selectOption(int option)
        {
            FollowersPlugin.followersSubMenuGUI.config.option_followers = option;
            if(option == 1)
            {
                FollowersPlugin.followersSubMenuGUI.optionTamas2.EnableButton();

            }
            else
            {
                FollowersPlugin.followersSubMenuGUI.optionTamas2.DisableButton();
            }

            //      FollowersBehaviour.followersSubMenuGUI.optionTamas2.GetComponent<Button>().enabled = false;
            // FollowersBehaviour.followersSubMenuGUI.optionTamas2 -> Disable it
        }
    }

}
