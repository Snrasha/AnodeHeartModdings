
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Universal.ModMenuLib;

namespace Universal.ModMenu
{
    public static class ModMenuGUI
    {

        private static Dictionary<string, SubModMenuInterface> subModMenus;


        private static OptionsController optionsController;
        public static GameObject ModMenu;
        private static GameObject Settings;

        public static Color overlaydefault = new Color(0.5377f, 0.5377f, 0.5377f);
        public static Color overlaytitle = new Color(0.8377f, 0.5377f, 0.5377f);
        public static Color overlayDesc = new Color(0.2377f, 0.2377f, 0.2377f);

        public static string name { get { return "ModMenu"; } } 

    public class ListenerOption
        {
            public OptionsController OptionsController;
            public int j;
            internal void OnHover()
            {
               // AccessTools.Method(typeof(OptionsController), "setSelected").Invoke(OptionsController, new object[] { j });

                OptionsController.setSelected(j);
            }

            internal void OnAction1()
            {
                OptionsController.OptionButtons[j].MoveLeft();
               // OptionsController.OptionButtons
              //  SettingsOption[] settingsOptions = (SettingsOption[])AccessTools.Field(typeof(OptionsController), "OptionButtons").GetValue(OptionsController);
              //  settingsOptions[j].MoveLeft();
            }

            internal void OnAction2()
            {
               // SettingsOption[] settingsOptions = (SettingsOption[])AccessTools.Field(typeof(OptionsController), "OptionButtons").GetValue(OptionsController);
               // settingsOptions[j].MoveRight();
                OptionsController.OptionButtons[j].MoveRight();
            }
        }
        public static bool __IsOptionMenuOpen()
        {
            return optionsController != null && optionsController.gameObject.activeSelf;
        }

        public static void __SaveConfigFileAll()
        {
            if (subModMenus == null)
            {
                subModMenus = new Dictionary<string, SubModMenuInterface>();
            }
            foreach (SubModMenuInterface subModMenuInterface in subModMenus.Values)
            {
                subModMenuInterface.SaveConfigFile();
            }
        }
        public static void __OnExitOptionsMenuAll()
        {
            Debug.Log("__OnExitOptionsMenuAll");
            if (subModMenus == null)
            {
                subModMenus = new Dictionary<string, SubModMenuInterface>();
            }
            foreach (SubModMenuInterface subModMenuInterface in subModMenus.Values)
            {
                subModMenuInterface.OnExitOptionsMenu();
            }
        }

        public static void __OnEnterModMenuAll()
        {
            if (subModMenus == null)
            {
                subModMenus = new Dictionary<string, SubModMenuInterface>();
            }
            Debug.Log("__OnEnterOptionsMenuAll");

            foreach (SubModMenuInterface subModMenuInterface in subModMenus.Values)
            {
                subModMenuInterface.OnEnterOptionsMenu();
            }
        }


        public static void AddSubMenu(string nameMod,SubModMenuInterface subModMenuInterface)
        {
            if (subModMenus == null)
            {
                subModMenus = new Dictionary<string, SubModMenuInterface>();
            }
            if (subModMenus.ContainsKey(nameMod))
            {
                subModMenus[nameMod] = subModMenuInterface;
                return;
            }
            subModMenus.Add(nameMod, subModMenuInterface);
        }

        public static bool __CheckIfNotExist(OptionsController optionsController)
        {
            GameObject childMod = null;
            GameObject Rows = null;
            GameObject frame = optionsController.gameObject.transform.GetChild(0).gameObject;
            foreach (Transform child in frame.transform)
            {
                if (child.gameObject.name.Equals(name))
                {
                    childMod = child.gameObject;
                }
                if (child.gameObject.name.Equals("Row Buttons"))
                {
                    Rows = child.gameObject;
                }
            }
            return childMod == null && Rows != null;
        }

        private static bool CheckIfNotExist(OptionsController optionsController, out GameObject settings, out GameObject childMod, out GameObject Rows)
        {
            childMod = null;
            Rows= null;
            settings = null;
            GameObject frame = optionsController.gameObject.transform.GetChild(0).gameObject;
            foreach (Transform child in frame.transform)
            {
                if (child.gameObject.name.Equals(name))
                {
                    childMod = child.gameObject;
                }
                if (child.gameObject.name.Equals("Settings"))
                {
                    settings = child.gameObject;
                }
                if (child.gameObject.name.Equals("Row Buttons"))
                {
                    Rows = child.gameObject;
                }
            }
           // Debug.Log(childMod + " settings" + settings + " Rows" + Rows);
            return childMod == null && Rows != null;
        }
        public static bool NotNeedToCreateModMenu()
        {
            return subModMenus == null;
        }


        public static void __CreateModMenu(OptionsController optionsController2)
        {
            optionsController = optionsController2;
          GameObject frame= optionsController.gameObject.transform.GetChild(0).gameObject;
            GameObject childMod = null;
            GameObject Rows = null;
            GameObject settings = null;


            if (CheckIfNotExist(optionsController, out settings, out childMod, out Rows)) {
                Settings = settings;
          //      UniEventBus.EventBusOptionsAdded = false;



                //  this.ModMenu =
                // this.ModMenu = childMod;

                ModMenu = UnityEngine.Object.Instantiate(Settings.gameObject);
                ModMenu.transform.SetParent(frame.transform);
                ModMenu.transform.localPosition= frame.transform.localPosition;
                ModMenu.transform.localScale = frame.transform.localScale;
                ModMenu.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
                ModMenu.SetActive(false);

                ModMenu.name = name;
                foreach (Transform child in ModMenu.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }


                
                Transform Gamemodebutton = Rows.transform.GetChild(Rows.transform.childCount-1);
                RectTransform Gamemodebuttonrect = Gamemodebutton.GetComponent<RectTransform>();

                GameObject clone = UnityEngine.Object.Instantiate(Gamemodebutton.gameObject);
                //GameObject clone= Gamemodebutton.gameObject.DeepClone();
                clone.transform.SetParent(Rows.transform);
                RectTransform recttrans= clone.GetComponent<RectTransform>();
                recttrans.anchoredPosition = Gamemodebuttonrect.anchoredPosition+new Vector2(47,0);
                recttrans.anchoredPosition3D = Gamemodebuttonrect.anchoredPosition3D + new Vector3(47, 0,0);
                recttrans.sizeDelta= Gamemodebuttonrect.sizeDelta;
                recttrans.pivot= Gamemodebuttonrect.pivot;
                recttrans.localScale= Gamemodebuttonrect.localScale;
                //  clone.GetComponent<TranslationKey>().;
                Button button = clone.GetComponent<Button>();
               // UnityEngine.Object.Destroy(button);
                button.onClick = new Button.ButtonClickedEvent();
                button.onClick.AddListener(delegate { optionsController.Load(3); });

                GameObject clonechild = clone.transform.GetChild(0).gameObject;

                TextUI text = clonechild.GetComponent<TextUI>();

                if (text != null)
                {
                    TranslationKey translationKey = text.GetComponent<TranslationKey>();
                    if (translationKey != null)
                    {
                        translationKey.enabled = false;
                        UnityEngine.Object.Destroy(translationKey);
                    }

                    optionsController.StartCoroutine(SetTextButton( text,"Mods"));

                }

            }

            else
            {
                if (childMod != null)
                {
                    ModMenu = childMod;
                    Settings=settings;
                }
            }
        }

        public static void __CreateSubModMenu(OptionsController optionsController2)
        {
            if (subModMenus == null)
            {
                subModMenus = new Dictionary<string, SubModMenuInterface>();
            }
            foreach (SubModMenuInterface subModMenuInterface in subModMenus.Values)
            {
                if (!subModMenuInterface.CheckIfExist(optionsController2))
                {
                    subModMenuInterface.CreateModMenu(optionsController2);
                }
                subModMenuInterface.SaveConfigFile();
            }
        }

        private static IEnumerator SetTextButton(TextUI text,string texttoput)
        {
            yield return new WaitForSeconds(0.1f);
            text.SetText(texttoput);
        }

        private static GameObject GetResolutionButton()
        {
            return Settings.transform.GetChild(0).gameObject;
        }
        public static GameObject AddLayout(string namelayout, GameObject parent = null)
        {
            GameObject newlayout = new GameObject(namelayout);
            if (parent == null)
            {
                newlayout.transform.SetParent(ModMenu.transform);
            }
            else
            {
                newlayout.transform.SetParent(parent.transform);

            }
            newlayout.transform.localScale = ModMenu.transform.localScale;

            VerticalLayoutGroup verticalLayoutGroup = ModMenu.GetComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childAlignment = TextAnchor.UpperCenter;
            verticalLayoutGroup.CopyComponent(newlayout);

            newlayout.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;

            newlayout.transform.localPosition = Vector3.zero;
            RectTransform rectTransform = newlayout.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.anchoredPosition3D = Vector2.zero;

            return newlayout;
        }
        public static void AddCheckLayout(Type type)
        {
          //  GameObject newlayout = new GameObject(name);
            ModMenu.AddComponent(type);
         //   newlayout.transform.parent=ModMenu.transform;
         //   newlayout.transform.SetParent(ModMenu.transform);
         //   newlayout.transform.localPosition = ModMenu.transform.localPosition;
         //  newlayout.transform.localScale = ModMenu.transform.localScale;

            //        VerticalLayoutGroup verticalLayoutGroup = ModMenu.GetComponent<VerticalLayoutGroup>();
            //   verticalLayoutGroup.CopyComponent(newlayout);

          //  return newlayout;
        }
        public static GameObject CreateText(string nameText, string textText, GameObject parent = null)
        {
            return CreateText(nameText, textText, overlaydefault, parent);
        }

            public static GameObject CreateText(string nameText, string idText, Color overlay, GameObject parent = null)
        {
            GameObject resolution = GetResolutionButton();
            GameObject newbutton = UnityEngine.Object.Instantiate(resolution);
            if (parent == null)
            {
                newbutton.transform.SetParent(ModMenu.transform);
            }
            else
            {
                newbutton.transform.SetParent(parent.transform);

            }
            newbutton.transform.localPosition = resolution.transform.localPosition;
            newbutton.transform.localScale = resolution.transform.localScale;
            newbutton.GetComponent<Image>().color = overlay;

            newbutton.name = nameText;
            GameObject key = null;

            foreach (Transform child in newbutton.transform)
            {
                if (child.gameObject.name.Equals("Key"))
                {
                    key = child.gameObject;
                }
                else
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }
            if (key != null)
            {
                TextUI text = key.GetComponent<TextUI>();
                RectTransform rectTransform= key.GetComponent<RectTransform>();
                rectTransform.pivot = new Vector2(0,1f);
                rectTransform.anchorMin = new Vector2(0, 1f);
                rectTransform.anchorMax = new Vector2(1f, 1f);
                text.alignment = TextAnchor.MiddleCenter;
                rectTransform.sizeDelta = new Vector2(0f, 11f);
                rectTransform.localPosition = new Vector3(0, rectTransform.localPosition.y, rectTransform.localPosition.z);
                rectTransform.anchoredPosition = Vector2.zero;
                rectTransform.anchoredPosition3D = Vector2.zero;

                if (text != null)
                {
                    TranslationKey translationKey = text.GetComponent<TranslationKey>();
                    if (translationKey != null)
                    {
                        //    translationKey.enabled = false;
                        //    UnityEngine.Object.Destroy(translationKey);
                        translationKey.Key = idText;
                    }
                    //TranslationKey translationKey = text.GetComponent<TranslationKey>();
                    //if (translationKey != null)
                    //{
                    //    translationKey.enabled = false;
                    //    UnityEngine.Object.Destroy(translationKey);
                    //}
                   // optionsController.StartCoroutine(SetTextButton(text, textText));

                }
            }
            UnityEngine.Object.Destroy(newbutton.GetComponent<OptionResolution>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<MouseSupport>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<OptionBinding>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<Button>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<EventTrigger>());

            return newbutton;
        }




        public static GameObject CreateButton(Type type,string nameButton, string idtext, GameObject parent=null)
        {
            GameObject resolution= GetResolutionButton();
            OptionResolution optionResolution= resolution.GetComponent<OptionResolution>();

            GameObject newbutton = UnityEngine.Object.Instantiate(resolution);
            if (parent == null)
            {
                newbutton.transform.SetParent(ModMenu.transform);
            }
            else
            {
                newbutton.transform.SetParent(parent.transform);
            }
            newbutton.transform.localPosition = resolution.transform.localPosition;
            newbutton.transform.localScale = resolution.transform.localScale;
            newbutton.name= nameButton;
            GameObject key = null;

            foreach (Transform child in newbutton.transform)
            {
                if (child.gameObject.name.Equals("Key"))
                {
                    key = child.gameObject;
                }
            }
            if (key != null)
            {
                TextUI text = key.GetComponent<TextUI>();

                if (text != null)
                {
                    TranslationKey translationKey = text.GetComponent<TranslationKey>();
                    if (translationKey != null)
                    {
                        //    translationKey.enabled = false;
                        //    UnityEngine.Object.Destroy(translationKey);
                        translationKey.Key = idtext;
                    }

               // optionsController.StartCoroutine(SetTextButton(text, textButton));

                }
            }
            UnityEngine.Object.Destroy(newbutton.GetComponent<OptionResolution>());
           newbutton.AddComponent(type);
            return newbutton;
        }


    }
}
