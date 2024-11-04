
using Followers.ModMenu;
using Impossible.Langs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using Universal.EventBusLib;
using Universal.ModMenu;
using Universal.ModMenuLib;


namespace Impossible.ModMenu
{
    public class ImpossibleSubMenuGUI : SubModMenuInterface
    {
        public Config config;
        public string pathConfig;
        private string pathFolderConfig = "/../BepInEx/plugins/ImpossibleConfig";
        private string configname = "/Config.json";
        public OptionEnable optionEnable;

        public ImpossibleLayoutFlag impossibleLayoutFlag;



        public ImpossibleSubMenuGUI()
        {
            pathConfig = Application.dataPath + pathFolderConfig;

            LoadOptions();
            UniEventBus.AddTypesToEventBus("Impossible", new Type[] { typeof(OptionEnable) });

        }




        public bool CheckIfExist(OptionsController optionsController)
        {
            GameObject childMod = null;

            GameObject frame = optionsController.gameObject.transform.GetChild(0).gameObject;
            foreach (Transform child in frame.transform)
            {
                if (child.gameObject.name.Equals(ModMenuGUI.name))
                {
                    childMod = child.gameObject;
                }
            }
            if (childMod != null)
            {

                impossibleLayoutFlag = childMod.GetComponent<ImpossibleLayoutFlag>();

            }


            return impossibleLayoutFlag != null;
        }

        public void CreateModMenu(OptionsController optionsController)
        {
            GameObject childMod = null;

            GameObject frame = optionsController.gameObject.transform.GetChild(0).gameObject;
            foreach (Transform child in frame.transform)
            {
                if (child.gameObject.name.Equals(ModMenuGUI.name))
                {
                    childMod = child.gameObject;
                }
            }
            if (childMod != null)
            {
                impossibleLayoutFlag = childMod.GetComponent<ImpossibleLayoutFlag>();
            }
            if (impossibleLayoutFlag == null)
            {

                ModMenuGUI.AddCheckLayout(typeof(ImpossibleLayoutFlag));

                GameObject layout=ModMenuGUI.AddLayout("ImpossibleLayout");

                ModMenuGUI.CreateText("TextImpossibleTitle", ImpossibleLang.Impossible_title_plugin, ModMenuGUI.overlaytitle, layout);
                optionEnable = ModMenuGUI.CreateButton(typeof(OptionEnable), "ImpossibleBtn", "", layout).GetComponent<OptionEnable>();


                //optionTamas1.enabled = true;
                //optionTamas2.enabled = true;

            }


        }


        public void LoadOptions()
        {
            if (!Directory.Exists(pathConfig))
            {
                Directory.CreateDirectory(pathConfig);
            }

            LoadConfigFile();


        }

        public void LoadConfigFile()
        {
            if (File.Exists(pathConfig + configname))
            {
                try
                {
                    using (Stream stream = File.OpenRead(pathConfig + configname))
                    {
                        byte[] bytes = new byte[stream.Length];

                        stream.Read(bytes, 0, bytes.Length);

                        string str = Encoding.ASCII.GetString(bytes);
                        try
                        {
                            config = JsonConvert.DeserializeObject<Config>(str);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Error parsing {pathConfig}: {ex.Message}");
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                config = new Config();
                config.enabled = 0;
            }
        }
        public void SaveConfigFile()
        {
            try
            {
                string textjson = JsonConvert.SerializeObject(config);

                using (Stream stream = File.Open(pathConfig + configname, FileMode.Create))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(textjson);

                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error saving {pathConfig}: {e.Message}");
            }
        }

        public void OnExitOptionsMenu()
        {
        }

        public void OnEnterOptionsMenu()
        {
            if (optionEnable != null)
            {
                optionEnable.getStartingOption();
            }
        }
    }


}
