using UnityEngine;
using HarmonyLib;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
using Followers.ModMenu;
using static Followers.ModMenu.ModMenuGUI;
using System.Linq;
using System.Runtime.CompilerServices;
using MonoMod.Utils;
using Universal.EventBusLib;

namespace Universal.ModMenuLib
{


    [HarmonyPatch(typeof(OptionsController), nameof(OptionsController.Load), new[] { typeof(int) })]
    static class Patch_OptionsController_Load
    {
        [HarmonyPrefix]
        static bool Prefix(OptionsController __instance, int page, ref int ___page, ref SettingsOption[] ___OptionButtons, ref int ___selectedOption, ref bool ___focus)
        {

            if (page == 0)
            {
                if (ModMenuGUI.__CheckIfNotExist(__instance))
                {
                    ModMenuGUI.__CreateModMenu(__instance);
                  
                }
                ModMenuGUI.__CreateSubModMenu(__instance);



            }
            ModMenuGUI.ModMenu.SetActive(page == 2);

            if (page == 2 && ___page != 2)
            {
                ___page = page;
                ___OptionButtons = ModMenuGUI.ModMenu.GetComponentsInChildren<SettingsOption>();
                __instance.SettingsRowButtonOverlay.SetActive(page == 1);
                __instance.GameModesRowButtonOverlay.SetActive(page == 0);
                __instance.SettingsOptionsContainer.SetActive(page == 0);


                bool flag = BuildOptions.IsDemo && __instance.GameModesUnavailableTab != null;
                __instance.GameModesUnavailableTab.SetActive(flag && page == 1);
                __instance.GameModesTab.SetActive(!flag && page == 1);

                //   Type internalclass = AccessTools.TypeByName("OptionsController.c__DisplayClass21_0");

                for (int i = 0; i < ___OptionButtons.Length; i++)
                {

                    ListenerOption listenerOption = new ListenerOption();
                    listenerOption.OptionsController = __instance;
                    //  object instancedisplay=  AccessTools.CreateInstance(internalclass);
                    //  AccessTools.Field(typeof(OptionsController), "4__this").SetValue(instancedisplay, __instance);
                    SettingsOption obj = ___OptionButtons[i];
                    obj.Unselect();
                    obj.enabled = true;
                    obj.Load();

                    MouseSupport component = obj.GetComponent<MouseSupport>();
                    listenerOption.j = i;
                    component.OnHover = listenerOption.OnHover;
                    component.OnAction1 = listenerOption.OnAction1;
                    component.OnAction2 = listenerOption.OnAction2;

                    //   AccessTools.Field(typeof(int), "j").SetValue(instancedisplay, i);
                    //MethodInfo method = AccessTools.Method(typeof(void), "__0");
                    //Action someMethodDelegate = (Action)Delegate.CreateDelegate(typeof(Action), method);
                    //component.OnHover = someMethodDelegate;
                    // method = AccessTools.Method(typeof(void), "__1");
                    // someMethodDelegate = (Action)Delegate.CreateDelegate(typeof(Action), method);
                    //component.OnAction1 = someMethodDelegate;
                    //method = AccessTools.Method(typeof(void), "__2");
                    //someMethodDelegate = (Action)Delegate.CreateDelegate(typeof(Action), method);
                    //component.OnAction2 = someMethodDelegate;
                }

                ___OptionButtons[0].Select();
                ___selectedOption = 0;
                __instance.UpdateDescription();
                AccessTools.Method(typeof(OptionsController), "UpdateLock").Invoke(__instance, null);
                ___focus = true;

                ModMenuGUI.__OnEnterModMenuAll();

                return false;
            }



            return true;
        }
        public delegate void DoDelegate();
    }
    [HarmonyPatch(typeof(OptionsController), nameof(OptionsController.Exit))]
    static class Patch_OptionsController_Exit
    {
        [HarmonyPrefix]
        static void Prefix()
        {
            if (ModMenuGUI.__IsOptionMenuOpen())
            {
                ModMenuGUI.__SaveConfigFileAll();
                ModMenuGUI.__OnExitOptionsMenuAll();
            }
        }
    }

    //[HarmonyPatch(typeof(MainMenuHUD), nameof(MainMenuHUD.showOptions))]
    //static class Patch_MainMenuHUD_showOptions
    //{
    //    [HarmonyPostfix]
    //    static void Postfix()
    //    {
    //        ModMenuGUI.__OnEnterModMenuAll();
    //    }
    //}

    [HarmonyPatch(typeof(MainMenuHUD), nameof(MainMenuHUD.Click_Exit))]
    static class Patch_MainMenuHUD_Click_Exit
    {
        [HarmonyPrefix]
        static void Prefix()
        {
            if (ModMenuGUI.__IsOptionMenuOpen())
            {
                ModMenuGUI.__SaveConfigFileAll();
                ModMenuGUI.__OnExitOptionsMenuAll();
            }
        }
    }
    [HarmonyPatch(typeof(MainMenuHUD), nameof(MainMenuHUD.Update))]
    static class Patch_MainMenuHUD_Update
    {
        [HarmonyPrefix]
        static void Prefix(MainMenuHUD __instance)
        {
            if (__instance.optionsShown && GameInput.Cancel())
            {
                ModMenuGUI.__SaveConfigFileAll();
                ModMenuGUI.__OnExitOptionsMenuAll();
            }
        }
    }

   



}
