
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
            StringLocalizer.t("Active teams"),
            StringLocalizer.t("Favorite")
            };
        }

        public override int getStartingOption()
        {
            if (FollowersBehaviour.followersSubMenuGUI.config.option_followers==0)
            {
                return 0;
            }
            return 1;
        }

        public override void selectOption(int option)
        {
            FollowersBehaviour.followersSubMenuGUI.config.option_followers = option;
     //      FollowersBehaviour.followersSubMenuGUI.optionTamas2.GetComponent<Button>().enabled = false;
           // FollowersBehaviour.followersSubMenuGUI.optionTamas2 -> Disable it
        }
    }

}
