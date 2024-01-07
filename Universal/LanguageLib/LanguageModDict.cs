
using System.Collections.Generic;


namespace Universal.LanguageLib
{
    public abstract class LanguageModDict
    {

        private Dictionary<string, Dictionary<Language, string>> dict;

        public LanguageModDict() {

            dict = new Dictionary<string, Dictionary<Language, string>>();
        }

        public void Add(string key, Dictionary<Language, string> value)
        {
            dict.Add(key, value);
        }

        //public Dictionary<string, Dictionary<Language, string>>.KeyCollection GetKeys()
        //{
        //    return dict.Keys;
        //}
        //public Dictionary<Language, string> Get(string key)
        //{
        //    return dict[key];
        //}

        public void LoadLang(Language language, Dictionary<string, string> strings)
        {
            foreach (string key in dict.Keys)
            {
                var value = dict[key];
                if (value == null) continue;
                if (value.ContainsKey(language))
                {
                    strings.Add(key, value[language]);
                }
                else if (value.ContainsKey(Language.English))
                {
                    strings.Add(key, value[language]);

                }
            }
            
        }
        //public void LoadConfigFile();
        //public void SaveConfigFile();

        //public void LoadOptions();

        //public bool CheckIfExist(OptionsController optionsController);

        //public void CreateModMenu(OptionsController optionsController);

        //public void OnExitOptionsMenu();
        //public void OnEnterOptionsMenu();
    }
}
