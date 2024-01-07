
using System.Collections.Generic;
using Universal.LanguageLib;

namespace EasySave.Langs
{
    public class EasySaveLang:LanguageModDict
    {

        public static string EasySave_desc_plugin = "EasySave_desc_plugin";
        public static string EasySave_title_plugin = "EasySave_title_plugin";



        public EasySaveLang() : base()
        {

            Dictionary<Language, string> dict = new Dictionary<Language, string>
            {
                { Language.English, "F4 for save menu, F5/F9 for quick" },
                { Language.French, "F4 pour sauv., F5/F9 pour instant" }
            };
            Add(EasySave_desc_plugin, dict);

            dict = new Dictionary<Language, string>
            {
                { Language.English, "EasySave Plugin" },
                { Language.French, "EasySave Plugin" }
            };
            Add(EasySave_title_plugin, dict);
           

            LangMod.AddLanguageMod("EasySave", this);
        }


    }
}
