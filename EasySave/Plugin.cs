using BepInEx;
using System.IO;
using System.Reflection;
using System;
using UnityEngine;
using HarmonyLib;
using System.Collections;
using Universal.IconLib;
using Universal;
using Universal.TexturesLib;
using EasySave.Config;
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

        private EasySaveConfigManager easySaveConfigManager;

      //  private EasySaveLang EasySaveLang;

        

        private void Awake()
        {
            harmony.PatchAll();
         //   EasySaveLang = new EasySaveLang();
            Sprite ModIcon = TexturesLib.CreateSprite("EasySave.Assets.", "Icon.png");
            IconGUI.AddIcon(new Icon("EasySave", "EasySave", ModIcon));

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            // easySaveSubMenuGUI = new EasySaveSubMenuGUI();

            //  ModMenuGUI.AddSubMenu("EasySave", easySaveSubMenuGUI);
            easySaveConfigManager = new EasySaveConfigManager();
            easySaveConfigManager.Init(Config);
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
                if (Input.GetKey(easySaveConfigManager.Save_Menu_Toggle.Value))
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
                if (Input.GetKey(easySaveConfigManager.Fast_Save_Save.Value)) {
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
                if (Input.GetKey(easySaveConfigManager.Fast_Save_Load.Value))
                {
                    // also for load
                    if (!CheckIfCanSave())
                    {
                        return;
                    }
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
                SaveGameScreen saveGameScreen = UnityEngine.Object.Instantiate(mainMenuHUD.SaveGameScreenPrefab, mainMenuHUD.transform.parent.parent.transform);
                yield return saveGameScreen.Load("QuickSave", saving: true);
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


    }
}
