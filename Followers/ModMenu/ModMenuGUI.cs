using HarmonyLib;
using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UIElements.UIRAtlasAllocator;

namespace Followers.ModMenu
{
    public class ModMenuGUI
    {


        private OptionsController optionsController;
        public GameObject ModMenu;
        private GameObject Settings;

        public string name = "ModMenu";

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
        


        // LevelBase -> Canvas -> Below Overlay -> MainMenu -> Options -> Frame
        public ModMenuGUI() { 
        
        }

        public bool CheckIfExist(OptionsController optionsController)
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

        public bool CheckIfNotExist(OptionsController optionsController, out GameObject settings, out GameObject childMod, out GameObject Rows)
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


        public void CreateModMenu(OptionsController optionsController)
        {
            this.optionsController = optionsController;
          GameObject frame= optionsController.gameObject.transform.GetChild(0).gameObject;
            GameObject childMod = null;
            GameObject Rows = null;
            GameObject settings = null;


            if (CheckIfNotExist(optionsController, out settings, out childMod, out Rows)) {
                this.Settings = settings;



                //  this.ModMenu =
                // this.ModMenu = childMod;

                this.ModMenu = UnityEngine.Object.Instantiate(Settings.gameObject);
                this.ModMenu.transform.SetParent(frame.transform);
                this.ModMenu.transform.localPosition= frame.transform.localPosition;
                this.ModMenu.transform.localScale = frame.transform.localScale;

                this.ModMenu.SetActive(false);

                this.ModMenu.name = name;
                foreach (Transform child in this.ModMenu.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }



                Transform Gamemodebutton = Rows.transform.GetChild(1);
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
                button.onClick.AddListener(delegate { optionsController.Load(2); });

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
        private IEnumerator SetTextButton(TextUI text,string texttoput)
        {
         //   Button button = clone.AddComponent<Button>();
            yield return new WaitForSeconds(0.1f);
            text.SetText(texttoput);
        }

        public GameObject GetResolutionButton()
        {
            return Settings.transform.GetChild(0).gameObject;
        }
        public void AddCheckLayout(Type type)
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
        public GameObject CreateText(string nameText, string textText)
        {
            GameObject resolution = GetResolutionButton();
            GameObject newbutton = UnityEngine.Object.Instantiate(resolution);
            newbutton.transform.SetParent(ModMenu.transform);
            newbutton.transform.localPosition = resolution.transform.localPosition;
            newbutton.transform.localScale = resolution.transform.localScale;

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
                rectTransform.pivot = new Vector2(0, 0.5f);
                rectTransform.sizeDelta = new Vector2(203f, 11f);



                if (text != null)
                {
                    TranslationKey translationKey = text.GetComponent<TranslationKey>();
                    if (translationKey != null)
                    {
                        translationKey.enabled = false;
                        UnityEngine.Object.Destroy(translationKey);
                    }
                    optionsController.StartCoroutine(SetTextButton(text, textText));

                }
            }
            UnityEngine.Object.Destroy(newbutton.GetComponent<OptionResolution>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<MouseSupport>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<OptionBinding>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<Button>());
            UnityEngine.Object.Destroy(newbutton.GetComponent<EventTrigger>());

            return newbutton;
        }
        public GameObject CreateButton(Type type,string nameButton, string textButton)
        {
            GameObject resolution= GetResolutionButton();
            OptionResolution optionResolution= resolution.GetComponent<OptionResolution>();

            GameObject newbutton = UnityEngine.Object.Instantiate(resolution);
            newbutton.transform.SetParent(ModMenu.transform);
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
                        translationKey.enabled = false;
                        UnityEngine.Object.Destroy(translationKey);
                    }
                    optionsController.StartCoroutine(SetTextButton(text, textButton));

                }
            }
            UnityEngine.Object.Destroy(newbutton.GetComponent<OptionResolution>());
           newbutton.AddComponent(type);
            return newbutton;
        }


    }
}
