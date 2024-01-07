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



    [HarmonyPatch(typeof(EventBus), nameof(EventBus.Initialize))]
    static class Patch_EventBus_Initialize
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            UniEventBus.__AddOptionsToEventBus();
        }
    }
    [HarmonyPatch(typeof(EventBus), nameof(EventBus.Register), new[] { typeof(IEventReceiverBase) })]
    static class Patch_EventBus_Register
    {
        [HarmonyPrefix]
        static void Prefix(IEventReceiverBase target)
        {
            UniEventBus.__AddOptionsToEventBus();
        }
    }
    [HarmonyPatch(typeof(EventBus), nameof(EventBus.UnRegister), new[] { typeof(IEventReceiverBase) })]
    static class Patch_EventBus_UnRegister
    {
        [HarmonyPrefix]
        static void Prefix(IEventReceiverBase target)
        {
            UniEventBus.__AddOptionsToEventBus();
        }
    }
    [HarmonyPatch(typeof(EventBus), MethodType.StaticConstructor)]
    static class Patch_EventBus_Constructor
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            UniEventBus.__AddOptionsToEventBus();
        }
    }

}
