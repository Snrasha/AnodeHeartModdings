

using Impossible;
using Impossible.Langs;

namespace Followers.ModMenu
{
    public class OptionEnable : SettingsOption
    {
        public override string[] getOptions()
        {
            return new string[2]
            {
            StringLocalizer.t(ImpossibleLang.Impossible_enable_plugin),
            StringLocalizer.t(ImpossibleLang.Impossible_disable_plugin)
            };
        }
        //public override void load()
        //{
        //    getStartingOption();
        //}

        public override int getStartingOption()
        {
            if (Plugin.ImpossibleSubMenuGUI.config.enabled==0)
            {
                return 0;
            }
            return 1;
        }
      //  public override 


        public override void selectOption(int option)
        {
            Plugin.ImpossibleSubMenuGUI.config.enabled = option;

        }
    }

}
