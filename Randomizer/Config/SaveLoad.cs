using Newtonsoft.Json;
using Randomizer.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Universal.EventBusLib;

namespace Randomizer.Config
{


    public class SaveLoad
    {
        public string pathConfig;

        private string pathFolderConfig = "/../BepInEx/plugins/RandomizerConfig";
        private string configname = "/Config.json";
        private string randname_spawn = "/RandSpawn.json";
        private string randname_stats = "/RandStats.json";
        private string randname_techs = "/RandTechs.json";
        public SaveLoad()
        {
            pathConfig = Application.dataPath + pathFolderConfig;
        }
        public void LoadFiles()
        {
            if (!Directory.Exists(pathConfig))
            {
                Directory.CreateDirectory(pathConfig);
            }
            LoadRand1File();
            LoadRand2File();
            LoadRand3File();
        }

        private void LoadRand1File()
        {
            if (File.Exists(pathConfig + randname_spawn))
            {
                try
                {
                    using (Stream stream = File.OpenRead(pathConfig + randname_spawn))
                    {
                        byte[] bytes = new byte[stream.Length];

                        stream.Read(bytes, 0, bytes.Length);

                        string str = Encoding.ASCII.GetString(bytes);
                        try
                        {
                            Plugin.randomizerSpawn = JsonConvert.DeserializeObject<RandomizerSpawn>(str);
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
                Plugin.randomizerSpawn = new RandomizerSpawn();
            }
        }
        private void LoadRand2File()
        {
            if (File.Exists(pathConfig + randname_stats))
            {
                try
                {
                    using (Stream stream = File.OpenRead(pathConfig + randname_stats))
                    {
                        byte[] bytes = new byte[stream.Length];

                        stream.Read(bytes, 0, bytes.Length);

                        string str = Encoding.ASCII.GetString(bytes);
                        try
                        {
                            Plugin.randomizerStats = JsonConvert.DeserializeObject<RandomizerStats>(str);
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
                Plugin.randomizerStats = new RandomizerStats();
            }
        }
        private void LoadRand3File()
        {
            if (File.Exists(pathConfig + randname_techs))
            {
                try
                {
                    using (Stream stream = File.OpenRead(pathConfig + randname_techs))
                    {
                        byte[] bytes = new byte[stream.Length];

                        stream.Read(bytes, 0, bytes.Length);

                        string str = Encoding.ASCII.GetString(bytes);
                        try
                        {
                            Plugin.randomizerTechs = JsonConvert.DeserializeObject<RandomizerTechs>(str);
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
                Plugin.randomizerTechs = new RandomizerTechs();
            }
        }

        public void SaveFiles()
        {
            SaveRand1File();
            SaveRand2File();
            SaveRand3File();
        }
        // saveconfigfile is from universal interface
        private void SaveRand1File()
        {
            try
            {
                string textjson = JsonConvert.SerializeObject(Plugin.randomizerSpawn);

                using (Stream stream = File.Open(pathConfig + randname_spawn, FileMode.Create))
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
        private void SaveRand2File()
        {
            try
            {
                string textjson = JsonConvert.SerializeObject(Plugin.randomizerStats);

                using (Stream stream = File.Open(pathConfig + randname_stats, FileMode.Create))
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
        private void SaveRand3File()
        {
            try
            {
                string textjson = JsonConvert.SerializeObject(Plugin.randomizerTechs);

                using (Stream stream = File.Open(pathConfig + randname_techs, FileMode.Create))
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
    }
}
