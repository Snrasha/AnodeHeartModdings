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

namespace Followers.Patches
{

    [HarmonyPatch(typeof(SaveSlotHUD), nameof(SaveSlotHUD.Load), new[] { typeof(int), typeof(Action<int>), typeof(Action<int>) })]
    static class Patch_SaveSlotHUD_Load
    {
        [HarmonyPostfix]
        static void Postfix(SaveSlotHUD __instance, int slot, Action<int> onSelect, Action<int> onHover)
        {


            GameObject moddedUI = new GameObject("BitModdingGUI_Followers", typeof(RectTransform));
            int shift = 0;
            foreach(Transform child in __instance.gameObject.transform)
            {
                if (child.gameObject.name.StartsWith("BitModdingGUI"))
                {
                    shift++;
                }
                if (child.gameObject.name.Equals("BitModdingGUI_Followers")){
                    return;
                }
            }
           

            moddedUI.transform.SetParent(__instance.gameObject.transform, false);
            Image image = moddedUI.AddComponent<Image>();
            RectTransform rectTransform = moddedUI.GetComponent<RectTransform>();
            image.sprite = FollowersBehaviour.ModIcon;
            rectTransform.pivot = new Vector2(0, 0);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.transform.localPosition = new Vector3(shift * 3, 0);
            rectTransform.sizeDelta = new Vector2(2, 2);

        }



    }

    [HarmonyPatch(typeof(EventBus), nameof(EventBus.Initialize))]
    static class Patch_EventBus_Initialize
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            FollowersSubMenuGUI.AddOptionsToEventBus();
        }
    }
    [HarmonyPatch(typeof(EventBus), nameof(EventBus.Register), new[] { typeof(IEventReceiverBase) })]
    static class Patch_EventBus_Register
    {
        [HarmonyPrefix]
        static void Prefix(IEventReceiverBase target)
        {
            FollowersSubMenuGUI.AddOptionsToEventBus();
        }
    }
    [HarmonyPatch(typeof(EventBus), MethodType.StaticConstructor )]
    static class Patch_EventBus_Constructor
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            FollowersSubMenuGUI.AddOptionsToEventBus();
        }
    }


            [HarmonyPatch(typeof(OptionsController), nameof(OptionsController.Load), new[] { typeof(int) })]
    static class Patch_OptionsController_Load
    {
        [HarmonyPrefix]
        static bool Prefix(OptionsController __instance, int page,ref int ___page, ref SettingsOption[] ___OptionButtons,ref int ___selectedOption,ref bool ___focus)
        {
            Debug.Log("Patch_OptionsController_Load " + page);


            if (page == 0) {
                if (FollowersBehaviour.modMenuGUI.CheckIfExist(__instance)){
                    FollowersBehaviour.modMenuGUI.CreateModMenu(__instance);
                }
                if (!FollowersBehaviour.followersSubMenuGUI.CheckIfExist(__instance))
                {
                    FollowersBehaviour.followersSubMenuGUI.CreateModMenu(__instance);
                }

            }
            FollowersBehaviour.modMenuGUI.ModMenu.SetActive(page == 2);

            if (page == 2 && ___page !=2)
            {
                ___page = page;
                ___OptionButtons = FollowersBehaviour.modMenuGUI.ModMenu.GetComponentsInChildren<SettingsOption>();
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
                AccessTools.Method(typeof(OptionsController), "UpdateLock").Invoke(__instance,null);
                ___focus = true;

                return false;
            }



            return true;
        }
        public delegate void DoDelegate();
    }
    [HarmonyPatch(typeof(OptionsController), nameof(OptionsController.Exit))]
    static class Patch_OptionsController_Exit
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            FollowersBehaviour.followersSubMenuGUI.SaveConfigFile();
        }
    }
    
}
