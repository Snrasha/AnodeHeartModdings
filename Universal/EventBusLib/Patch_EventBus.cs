
using HarmonyLib;

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
