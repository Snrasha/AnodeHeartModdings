
using System.Collections.Generic;

using Universal.LanguageLib;

namespace Followers.Langs
{
    public class FollowersLang:LanguageModDict
    {
        public static string Followers_option1_button_value_1 = "Followers_option1_button_value_1";
        public static string Followers_option1_button_value_2 = "Followers_option1_button_value_2";
        public static string Followers_option1_button_title = "Followers_option1_button_title";
        public static string Followers_option2_button_title = "Followers_option2_button_title";
        public static string Followers_title_plugin = "Followers_title_plugin";

        //public static string Followers_option3_button_value_1 = "Followers_option3_button_value_1";
        //public static string Followers_option3_button_value_2 = "Followers_option3_button_value_2";
        //public static string Followers_option3_button_title = "Followers_option3_button_title";



        public FollowersLang() : base()
        {

            Dictionary<Language, string> dict = new Dictionary<Language, string>
            {
                { Language.English, "Active teams" },
                { Language.French, "Team Actif" }
            };
            Add(Followers_option1_button_value_1, dict);

            dict = new Dictionary<Language, string>
            {
                { Language.English, "Favorite" },
                { Language.French, "Favori" }
            };
            Add(Followers_option1_button_value_2, dict);

            dict = new Dictionary<Language, string>
            {
                { Language.English, "Followers" },
                { Language.French, "Compagnons" }
            };
            Add(Followers_option1_button_title, dict);

            dict = new Dictionary<Language, string>
            {
                { Language.English, "Choice" },
                { Language.French, "Choix" }
            };
            Add(Followers_option2_button_title, dict);

            dict = new Dictionary<Language, string>
            {
                { Language.English, "Followers Plugin" },
                { Language.French, "Followers Plugin" }
            };
            Add(Followers_title_plugin, dict);

            //dict = new Dictionary<Language, string>
            //{
            //    { Language.English, "B" },
            //    { Language.French, "Choix" }
            //};
            //Add(Followers_option3_button_value_1, dict);


            LangMod.AddLanguageMod("Followers",this);
        }


    }
}
