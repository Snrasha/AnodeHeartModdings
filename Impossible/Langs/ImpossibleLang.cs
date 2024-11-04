
using System.Collections.Generic;
using Universal.LanguageLib;

namespace Impossible.Langs
{
    public class ImpossibleLang : LanguageModDict
    {

        public static string Impossible_enable_plugin = "Impossible_enable_plugin";
        public static string Impossible_disable_plugin = "Impossible_disable_plugin";

        public static string Impossible_title_plugin = "Impossible_title_plugin";



        public ImpossibleLang() : base()
        {

            Dictionary<Language, string> dict = new Dictionary<Language, string>
            {
                { Language.English, "Enable" },
                { Language.French, "Activé" }
            };
            Add(Impossible_enable_plugin, dict);

            dict = new Dictionary<Language, string>
            {
                { Language.English, "Disable" },
                { Language.French, "Désactive" }
            };
            Add(Impossible_disable_plugin, dict);
            dict = new Dictionary<Language, string>
            {
                { Language.English, "Impossible Plugin" },
                { Language.French, "Impossible Plugin" }
            };
            Add(Impossible_title_plugin, dict);

            LangMod.AddLanguageMod("Impossible", this);
        }


    }
}
