using UnityEngine;
using HarmonyLib;
using System.Security.Policy;
using System;
using UnityEngine.UI;

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
}
