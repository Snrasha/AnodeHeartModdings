using Newtonsoft.Json;
using Randomizer.Scripts;
using Randomizer.Scripts.Encounter;
using System;
using System.Collections;
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
       // private string pathConfig;
        private string pathSave;

        private string pathFolderConfig = "/../BepInEx/plugins/RandomizerConfig";
      //  private string configname = "/Config.json";

        private string randname_seed = "/RandSeed_{0}.json";
        //private string randname_spawn = "/RandSpawn_{0}.json";
        //private string randname_stats = "/RandStats_{0}.json";
        //private string randname_techs = "/RandTechs_{0}.json";
        public SaveLoad()
        {
       //     pathConfig = Application.dataPath + pathFolderConfig;
            pathSave = FileUtil.savePath("Randomizer/");
        }
        //public void LoadConfig(int slot)
        //{
        //    if (!Directory.Exists(pathConfig))
        //    {
        //        Directory.CreateDirectory(pathConfig);
        //    }
        //}
        public void LoadFiles(int slot)
        {
            

            //FileUtil.savePath( string.Format("save_{0}.bin", saveslot)

            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }
            LoadRandSeedFile(slot);
            //LoadRandSpawnFile(slot);
            //LoadRandStatsFile(slot);
            //LoadRandTechsFile(slot);
        }
        private void LoadRandSeedFile(int slot)
        {

            if (File.Exists(pathSave + string.Format(randname_seed, slot)))
            {
                try
                {
                    using (Stream stream = File.OpenRead(pathSave + string.Format(randname_seed, slot)))
                    {
                        byte[] bytes = new byte[stream.Length];

                        stream.Read(bytes, 0, bytes.Length);

                        string str = Encoding.ASCII.GetString(bytes);
                        try
                        {
                            Plugin.randomizerSeed = JsonConvert.DeserializeObject<RandSeed>(str);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Error parsing {pathSave}: {ex.Message}");
                        }
                    }
                }
                catch
                {
                }
            }
        }

        //private void LoadRandSpawnFile(int slot)
        //{
            
        //    if (File.Exists(pathSave + string.Format(randname_spawn, slot)))
        //    {
        //        try
        //        {
        //            using (Stream stream = File.OpenRead(pathSave + string.Format(randname_spawn, slot)))
        //            {
        //                byte[] bytes = new byte[stream.Length];

        //                stream.Read(bytes, 0, bytes.Length);

        //                string str = Encoding.ASCII.GetString(bytes);
        //                try
        //                {
        //                    Plugin.randomizerSpawn = JsonConvert.DeserializeObject<RandomizerSpawn>(str);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Debug.LogError($"Error parsing {pathSave}: {ex.Message}");
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    else
        //    {
        //        Plugin.randomizerSpawn = new RandomizerSpawn();
        //    }
        //}
        //private void LoadRandStatsFile(int slot)
        //{
        //    if (File.Exists(pathSave + string.Format(randname_stats, slot)))
        //    {
        //        try
        //        {
        //            using (Stream stream = File.OpenRead(pathSave + string.Format(randname_stats, slot)))
        //            {
        //                byte[] bytes = new byte[stream.Length];

        //                stream.Read(bytes, 0, bytes.Length);

        //                string str = Encoding.ASCII.GetString(bytes);
        //                try
        //                {
        //                    Plugin.randomizerStats = JsonConvert.DeserializeObject<RandomizerStats>(str);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Debug.LogError($"Error parsing {pathSave}: {ex.Message}");
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    else
        //    {
        //        Plugin.randomizerStats = new RandomizerStats();
        //    }
        //}
        //private void LoadRandTechsFile(int slot)
        //{
        //    if (File.Exists(pathSave + string.Format(randname_techs, slot)))
        //    {
        //        try
        //        {
        //            using (Stream stream = File.OpenRead(pathSave + string.Format(randname_techs, slot)))
        //            {
        //                byte[] bytes = new byte[stream.Length];

        //                stream.Read(bytes, 0, bytes.Length);

        //                string str = Encoding.ASCII.GetString(bytes);
        //                try
        //                {
        //                    Plugin.randomizerTechs = JsonConvert.DeserializeObject<RandomizerTechs>(str);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Debug.LogError($"Error parsing {pathSave}: {ex.Message}");
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    else
        //    {
        //        Plugin.randomizerTechs = new RandomizerTechs();
        //    }
        //}

        public void SaveFiles(int slot)
        {
            SaveRandSeedFile(slot);
            //SaveRandSpawnFile(slot);
            //SaveRandStatsFile(slot);
            //SaveRandTechsFile(slot);
        }
        private void SaveRandSeedFile(int slot)
        {
            try
            {
                string textjson = JsonConvert.SerializeObject(Plugin.randomizerSeed);

                using (Stream stream = File.Open(pathSave + string.Format(randname_seed, slot), FileMode.Create))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(textjson);

                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error saving {pathSave}: {e.Message}");
            }
        }


        //// saveconfigfile is from universal interface
        //private void SaveRandSpawnFile(int slot)
        //{
        //    try
        //    {
        //        string textjson = JsonConvert.SerializeObject(Plugin.randomizerSpawn);

        //        using (Stream stream = File.Open(pathSave + string.Format(randname_spawn, slot), FileMode.Create))
        //        {
        //            byte[] bytes = Encoding.UTF8.GetBytes(textjson);

        //            stream.Write(bytes, 0, bytes.Length);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.LogError($"Error saving {pathSave}: {e.Message}");
        //    }
        //}
        //private void SaveRandStatsFile(int slot)
        //{
        //    try
        //    {
        //        string textjson = JsonConvert.SerializeObject(Plugin.randomizerStats);

        //        using (Stream stream = File.Open(pathSave + string.Format(randname_stats, slot), FileMode.Create))
        //        {
        //            byte[] bytes = Encoding.UTF8.GetBytes(textjson);

        //            stream.Write(bytes, 0, bytes.Length);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.LogError($"Error saving {pathSave}: {e.Message}");
        //    }
        //}
        //private void SaveRandTechsFile(int slot)
        //{
        //    try
        //    {
        //        string textjson = JsonConvert.SerializeObject(Plugin.randomizerTechs);

        //        using (Stream stream = File.Open(pathSave + string.Format(randname_techs, slot), FileMode.Create))
        //        {
        //            byte[] bytes = Encoding.UTF8.GetBytes(textjson);

        //            stream.Write(bytes, 0, bytes.Length);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.LogError($"Error saving {pathSave}: {e.Message}");
        //    }
        //}

    }
}
