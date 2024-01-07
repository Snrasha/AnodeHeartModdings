using System.Collections.Generic;

namespace Universal.LanguageLib
{
    public static class LangMod
    {

        private static Dictionary<string, LanguageModDict> modLangs;


        public static void LoadLanguage(Dictionary<string, string> strings)
        {

            Language language = Singleton<Settings>.Instance.Load().Language.ToEnum<Language>();

            if (modLangs == null)
            {
                modLangs = new Dictionary<string, LanguageModDict>();
            }
            foreach (LanguageModDict languageModDict in modLangs.Values)
            {
                languageModDict.LoadLang(language,strings);
            }
        }
        public static void AddLanguageMod(string name, LanguageModDict mod)
        {
            if (modLangs == null)
            {
                modLangs = new Dictionary<string, LanguageModDict>();
            }
            modLangs.Add(name, mod);
        }

    }
}
