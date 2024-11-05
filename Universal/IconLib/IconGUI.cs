
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Universal.IconLib
{
    public static  class IconGUI
    {
        private static Dictionary<string, Icon> icons;

        private static string prefix = "IconModded_";

        public static void AddIcon(Icon icon)
        {
            if (icons == null)
            {
                  icons = new Dictionary<string, Icon>();
            }
            if (icons.ContainsKey(icon.NameMod))
            {
                icons[icon.NameMod] = icon;
                return;
            }
            icons.Add(icon.NameMod, icon);
        }
        public static void AddIconsToSaveSlot(SaveSlotHUD __instance)
        {
            int shift = 0;
            bool hasicon;
            if (icons == null)
            {
                icons = new Dictionary<string, Icon>();
            }
            foreach (Icon icon in icons.Values)
            {
                hasicon = false;
                GameObject moddedUI = new GameObject(prefix + icon.NameMod, typeof(RectTransform));
                foreach (Transform child in __instance.gameObject.transform)
                {
                    if (child.gameObject.name.Equals(prefix + icon.NameMod))
                    {
                        hasicon = true;
                        break;
                    }
                }
                if(hasicon)
                {
                    continue;
                }

                moddedUI.transform.SetParent(__instance.gameObject.transform, false);
                Image image = moddedUI.AddComponent<Image>();
                RectTransform rectTransform = moddedUI.GetComponent<RectTransform>();
                image.sprite = icon.sprite;
                rectTransform.pivot = new Vector2(0, 0);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 0);
                rectTransform.transform.localPosition = new Vector3(shift * 3, 0);
                rectTransform.sizeDelta = new Vector2(2, 2);
                shift++;
            }
        }
    }
}
