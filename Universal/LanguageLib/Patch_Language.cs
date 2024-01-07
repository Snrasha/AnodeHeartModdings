
using HarmonyLib;

namespace Universal.LanguageLib
{


    [HarmonyPatch(typeof(StringLocalizer), nameof(StringLocalizer.Load))]
    static class Patch_StringLocalizer_Load
    {
        [HarmonyPostfix]
        static void Postfix(StringLocalizer __instance)
        {
            LangMod.LoadLanguage(__instance.strings);
        }
    }
}
