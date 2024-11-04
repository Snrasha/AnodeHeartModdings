

using Impossible;
using Impossible.Langs;
using System.Collections.Generic;

namespace Followers.ModMenu
{
    public class OptionTPLimit : SettingsOption
    {
        public string[] tplimits;
        public override void load()
        {
            tplimits = new string[11];

            tplimits[0] = StringLocalizer.t(ImpossibleLang.Impossible_disable_plugin);
            for(int i=1;i<tplimits.Length;i++) {
                tplimits[i] = ""+(i-1);
            }
        }

        public override string[] getOptions()
        {
            return tplimits;
        }
        public override int getStartingOption()
        {
            return Plugin.ImpossibleSubMenuGUI.config.tp_limit;
        }

        public override void selectOption(int option)
        {
            Plugin.ImpossibleSubMenuGUI.config.tp_limit = option;

        }
    }

}
