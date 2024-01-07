using Followers.Langs;

using Newtonsoft.Json;
using System;

using System.IO;

using System.Text;

using UnityEngine;
using Universal.EventBusLib;
using Universal.ModMenu;
using Universal.ModMenuLib;


namespace Followers.ModMenu
{
    public class FollowersSubMenuGUI : SubModMenuInterface
    {

        public Config config;
        public string pathConfig;
        private string pathFolderConfig = "/../BepInEx/plugins/FollowersConfig";
        private string configname = "/Config.json";

        public OptionTamas1 optionTamas1;
        public OptionTamas2 optionTamas2;
        public FollowerLayoutFlag followerLayoutFlag;


        public FollowersSubMenuGUI()
        {
            pathConfig = Application.dataPath + pathFolderConfig;

            LoadOptions();
            UniEventBus.AddTypesToEventBus("Followers", new Type[] { typeof(OptionTamas1), typeof(OptionTamas2) });

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

                followerLayoutFlag = childMod.GetComponent<FollowerLayoutFlag>();

            }


            return followerLayoutFlag != null;
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
                followerLayoutFlag= childMod.GetComponent<FollowerLayoutFlag>();
            }
            if (followerLayoutFlag == null)
            {

                ModMenuGUI.AddCheckLayout(typeof(FollowerLayoutFlag));

                GameObject layout=ModMenuGUI.AddLayout("FollowerLayout");

                ModMenuGUI.CreateText("TextFollowersTitle", FollowersLang.Followers_title_plugin, ModMenuGUI.overlaytitle, layout);
                optionTamas1 = ModMenuGUI.CreateButton( typeof(OptionTamas1),"FollowersBtn", FollowersLang.Followers_option1_button_title, layout).GetComponent<OptionTamas1>();
                optionTamas2 = ModMenuGUI.CreateButton( typeof(OptionTamas2),"ChoiceBtn", FollowersLang.Followers_option2_button_title, layout).GetComponent<OptionTamas2>();
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
            if (File.Exists(pathConfig+configname))
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
                config.option_followers = 0;
                config.option_species = Species.Beebee.ToString();
            }
        }
        public void SaveConfigFile()
        {
            try
            {
                string textjson=JsonConvert.SerializeObject(config);
                
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
            FollowersPlugin.UpdateFollowersGroup();
        }

        public void OnEnterOptionsMenu()
        {

            if (optionTamas1 != null)
            {
                optionTamas1.getStartingOption();
            }
        }
    }


}
