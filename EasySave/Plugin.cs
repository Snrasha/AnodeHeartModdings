using BepInEx;
using System.IO;
using System.Reflection;
using System;
using UnityEngine;
using HarmonyLib;
using System.Collections;
using Universal.IconLib;
using Universal;
using Universal.ModMenu;
using EasySave.ModMenu;
using EasySave.Langs;

namespace EasySave
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("_Universal")]

    public class Plugin : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        private bool currentlyLoading = false;
        private bool currentlySaving = false;
        public static int slotSave = -100;

        private EasySaveSubMenuGUI easySaveSubMenuGUI = new EasySaveSubMenuGUI();

        private EasySaveLang EasySaveLang;

        

        private void Awake()
        {
            harmony.PatchAll();
            EasySaveLang = new EasySaveLang();
            Sprite ModIcon = CreateSprite("Icon.png");
            IconGUI.AddIcon(new Icon("EasySave", "EasySave", ModIcon));

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            easySaveSubMenuGUI = new EasySaveSubMenuGUI();

            ModMenuGUI.AddSubMenu("EasySave", easySaveSubMenuGUI);

        }

        public bool CheckIfCanSave()
        {

            //if battle
            BattleDirector director = UnityEngine.Object.FindObjectOfType<BattleDirector>();
            if (director == null || director.IsBattling)
            {
                return false;
            }
            CardBattleScreen cardBattleScreen = UnityEngine.Object.FindObjectOfType<CardBattleScreen>();
            if (cardBattleScreen != null && cardBattleScreen.isActiveAndEnabled)
            {
                return false;
            }
            TitleController TitleController = UnityEngine.Object.FindObjectOfType<TitleController>();

            if (TitleController != null && TitleController.isActiveAndEnabled)
            {
                return false;
            }
            MainMenuHUD mainMenuHUD = UnityEngine.Object.FindObjectOfType<MainMenuHUD>();

            if (mainMenuHUD != null && mainMenuHUD.isShown)
            {
                return false;
            }

            return true;


        }

        public void Update()
        {

            if (!currentlyLoading && !currentlySaving) {
                if (Input.GetKey(KeyCode.F4))
                {
                    if (!CheckIfCanSave())
                    {
                        return;
                    }

                    Debug.Log("F4 input");
                    PopupInfo.Call("Save Menu");
                    //QuickSave
                    StartCoroutine(ShowSavingMenu());
                }
                if (Input.GetKey(KeyCode.F5)) {
                    //   GameState.Instance().Data.
                    if (!CheckIfCanSave())
                    {
                        return;
                    }
                    Debug.Log("F5 input");
                    currentlySaving = true;
                    //QuickSave
                    PopupInfo.Call("Quick Save");

                  //  guiText.Call("Quick Load");

                    StartCoroutine(QuickSaveGame(slotSave));
                }
                if (Input.GetKey(KeyCode.F9))
                {
                    Debug.Log("F9 input");
                    currentlyLoading = true;
                    PopupInfo.Call("Quick Load");

                    StartCoroutine(QuickLoadGame(slotSave));
                }
            }

        }

        private IEnumerator ShowSavingMenu()
        {
            MainMenuHUD mainMenuHUD = UnityEngine.Object.FindObjectOfType<MainMenuHUD>();
            if (mainMenuHUD != null)
            {
                FreezeEvent.Set(freeze: true);
              //  mainMenuHUD.Group.alpha = 1f;
              //  mainMenuHUD.Group.blocksRaycasts = true;
                SaveGameScreen saveGameScreen = UnityEngine.Object.Instantiate(mainMenuHUD.SaveGameScreenPrefab, UnityEngine.Object.FindObjectOfType<Canvas>().transform);
                yield return saveGameScreen.Load("QuickSave", saving: true);
                FreezeEvent.Set(freeze: false);
            }
        }


        // Save the standard data, but need to save also the position for replace.
        private IEnumerator QuickSaveGame(int slot)
        {
            QuickSave quickSave = new QuickSave();
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            quickSave.xPosPlayer = gameObject.transform.position.x;
            quickSave.yPosPlayer = gameObject.transform.position.y;
            quickSave.zPosPlayer = gameObject.transform.position.z;

            FileUtil.Save(quickSave, $"quicksave_{slot}.bin");
            GameState.Save("QuickSave", slot);
            yield return new WaitForSeconds(0.5f);
            currentlySaving = false;
        }


        private IEnumerator QuickLoadGame(int slot)
        {

            if (GameState.SaveExists(slot))
            {


                SaveGameScreen saveGameScreen = UnityEngine.Object.FindObjectOfType<SaveGameScreen>();
                // not quickload on savescreen.
                if (saveGameScreen != null)
                {
                    currentlyLoading = false;
                    yield break;
                }

                //yield return Group.FadeOut(2f);
                SceneSingleton<AudioPlayer>.Instance.StopMusic();
                OverlayHUD overlayHUD = UnityEngine.Object.FindObjectOfType<OverlayHUD>();
                if (overlayHUD != null)
                {
                    yield return overlayHUD.FadeIn();
                }
                

                TitleController TitleController = UnityEngine.Object.FindObjectOfType<TitleController>();

                if (TitleController != null)
                {
                    yield return TitleController.Overlay.FadeIn(4f);
                }
                GameStateData gameStateData = GameState.LoadAndApply(slot);
               // Singleton<GameStore>.Instance.CheckAllAchievements();
                Singleton<LevelLoader>.Instance.Load(gameStateData.Level.ToEnum<LevelId>(), gameStateData.Anchor, 0.25f, PortalDirection.DontChange, forceLevelReload: true);

            }
            currentlyLoading = false;
        }


            public static Sprite CreateSprite(string path)
        {

            Texture2D tex = new Texture2D(1, 1);
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("EasySave.Assets." + path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    tex.filterMode = FilterMode.Point;  // Thought maybe this would help 
                    tex.LoadImage(bytes);
                }
            }
            catch (Exception e)
            {
            }

            tex.filterMode = FilterMode.Point;
            tex.anisoLevel = 0;
            tex.wrapMode = TextureWrapMode.Clamp;

            tex.Apply();
            Vector2 standardPivot = new Vector2(tex.width / 2f, tex.height / 2f);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            return sprite;
        }
    }
}
