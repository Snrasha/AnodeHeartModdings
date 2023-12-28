using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using System.Text;
using Newtonsoft.Json;
using Extrama.Json;
using System.Xml.Linq;

namespace Extrama
{

    class ExtramaFramework
    {
        public static Sprite ModIcon;

        public static Dictionary<int,string > TamasSpecies;
        public static Dictionary<string, Sprite> SpritesFronts;
        public static Dictionary<string, Sprite> SpritesAltFronts;

        public static Dictionary<string, Sprite> SpritesIcons;
        public static Dictionary<string, Sprite> SpritesAltIcons;

        public static Dictionary<string, string> StringDict;

        public static int ExtramaSpeciesPoint = 100000;
        //  public static 

        private MonsterLibrary WaitingMonsterForOthers;
        private Others WaitingOtherForMonster;
        private Species WaitingMonsterSpecies;



        private BepInEx.Logging.ManualLogSource _logger;
        public static Texture2D GetTexture(Dictionary<string, Texture2D> dict, string text)
        {
            if (dict.ContainsKey(text))
            {
                //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
                //  Sprite sprite = Sprite.Create(spriteb.texture, spriteb.rect, spriteb.pivot, 16);
                return dict[text];
            }
            return null;
        }

        public ExtramaFramework(BepInEx.Logging.ManualLogSource logger)
        {
            _logger = logger;

            
        }

  



        public void LoadAllTamas()
        {
            ModIcon = CreateSprite("Icon.png");

            SpritesFronts = new Dictionary<string, Sprite>();
            SpritesIcons = new Dictionary<string, Sprite>();
            SpritesAltFronts = new Dictionary<string, Sprite>();
            SpritesAltIcons = new Dictionary<string, Sprite>();
            TamasSpecies = new Dictionary<int, string>();
            StringDict = new Dictionary<string, string>();

            if (!Directory.Exists(Application.dataPath + "/../BepInEx/plugins/ExtramasConfig"))
            {
                Directory.CreateDirectory(Application.dataPath + "/../BepInEx/plugins/ExtramasConfig");
            }
            LoadSpeciesFile(Application.dataPath + "/../BepInEx/plugins/ExtramasConfig/Species");


            string appdataPath = Application.dataPath+ "/../";
            //string appdataPath = Application.persistentDataPath;

            appdataPath += "/ModsAssets";
            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
            }

            appdataPath += "/Extramas";
            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
            }
            int inc = ExtramaSpeciesPoint;

            string[] monsters = Directory.GetDirectories(appdataPath);


            // Create firstly every monsters
            foreach (string dir in monsters)
            {
                string[] files = Directory.GetFiles(dir);
                // Log("TextureReplacement monsters files " + files.Length);
                foreach (string file in files)
                {
                    string[] splut = file.Split('\\');
                    string splut2 = splut[splut.Length - 1];
                    string[] split2 = splut2.Split('.');
                    if (split2[split2.Length - 1].Equals("json") && !split2[0].Equals("Others"))
                    {
                        GetMonsterSpecies(split2[0]);
                    }
                }
            }


            foreach (string dir in monsters)
            {
                //     Log("TextureReplacement monsters " + dir);
                string[] files = Directory.GetFiles(dir);
                string[] last2 = dir.Split('\\');
                string last = last2[last2.Length - 1];
                // Log("TextureReplacement monsters files " + files.Length);
                foreach (string file in files)
                {
                    

                    string[] splut = file.Split('\\');
                    string splut2 = splut[splut.Length - 1];
                    string[] split2 = splut2.Split('.');
                    if (split2[split2.Length - 1].Equals("json"))
                    {
                        inc = GenerateMonster(file, split2[0],inc);

                        Debug.Log($"GenerateMonster  {split2[0]}: {inc}");

                        continue;
                    }

                    if (!split2[split2.Length - 1].Equals("png"))
                    {
                        continue;
                    }

                    //   Log("TextureReplacement monsters files " + file);
                    string[] split = split2[0].Split('_');
                    //   Log("TextureReplacement monsters files " + string.Join(",",split));
                    if (split.Length > 1)
                    {
                        string merge = "";
                        for (int i = 0; i < split.Length - 1; i++)
                        {
                            merge += split[i];
                            if (i < split.Length - 2)
                            {
                                merge += "_";
                            }
                        }
                        string end = split[split.Length - 1];
                        Debug.Log("Add " + end + " | " + merge+" "+ file);

                        if ("Front".Equals(end))
                        {
                            SpritesFronts.Add("Monsters/Fronts/" + merge, CreateSpriteFromFile(file));
                        }
                        if ("AltFront".Equals(end))
                        {
                            SpritesAltFronts.Add("Monsters/AltFronts/" + merge, CreateSpriteFromFile(file));
                        }
                        if ("Icon".Equals(end))
                        {
                            SpritesIcons.Add("Monsters/Icons/" + merge, CreateSpriteFromFile(file));
                        }
                        if ("AltIcon".Equals(end))
                        {
                            SpritesAltIcons.Add("Monsters/AltIcons/" + merge, CreateSpriteFromFile(file));
                        }
                    }
                }


                if (WaitingOtherForMonster != null && WaitingMonsterForOthers != null)
                {
                    MonsterLibrary monsterlib = MonsterLibrary.Data[WaitingOtherForMonster.EditSpecie.Species];

                    // Set the new evolutions of the specie.
                    foreach (Evolution evol in WaitingOtherForMonster.EditSpecie.Evolutions)
                    {
                        bool hasevol = false;
                        foreach (Evolution evol2 in monsterlib.Evolutions)
                        {
                            if (evol.Species == evol2.Species)
                            {
                                hasevol = true;
                                evol2.Wisdom = evol.Wisdom;
                                evol2.Level = evol.Level;
                                evol2.Weight = evol.Weight;
                                evol2.Magic = evol.Magic;
                                evol2.Mind = evol.Mind;
                                evol2.Speed = evol.Speed;
                                evol2.Strength = evol.Strength;

                                break;

                            }
                        }
                        if (!hasevol)
                        {
                            monsterlib.Evolutions.Add(evol);
                        }
                    }
                    // Set the new base (for baby tama)
                    if (WaitingOtherForMonster.EditSpecie.SwitchAncestorToNew) {
                        foreach (KeyValuePair<Species, MonsterLibrary> keyValuePair in MonsterLibrary.Data)
                        {
                            if (keyValuePair.Value.BaseAncestor.Equals(WaitingOtherForMonster.EditSpecie.Species)) {
                                keyValuePair.Value.BaseAncestor = WaitingMonsterSpecies;
                            }
                        }
                    }

                    if (WaitingOtherForMonster.Name != null && WaitingOtherForMonster.Dex!=null)
                    {
                        Language language = Singleton<Settings>.Instance.Load().Language.ToEnum<Language>();

                        switch (language)
                        {
                            case Language.English:
                                StringDict.Add("Species_"+ WaitingMonsterSpecies, WaitingOtherForMonster.Name.en);
                                StringDict.Add(WaitingMonsterSpecies + "_Dex", WaitingOtherForMonster.Dex.en);
                                break;
                            case Language.French:
                                StringDict.Add("Species_" + WaitingMonsterSpecies, WaitingOtherForMonster.Name.fr);
                                StringDict.Add(WaitingMonsterSpecies+"_Dex", WaitingOtherForMonster.Dex.en);


                                break;
                        }

                       
                       // int_Need_Food_Starving_GetPawn = type.GetField("pawn", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
                        // typeof(StringLocalizer).ge
                        //  .
                    }
                }

                WaitingMonsterForOthers = null;
                WaitingOtherForMonster = null;
            }

            SaveSpeciesFile(Application.dataPath + "/../BepInEx/plugins/ExtramasConfig/Species");

        }


        /**
         * 
         * Return the species number for the monster, if it do not exist, add one new.
         * */
        public int GetMonsterSpecies(string name)
        {
            int numberOfSpecies = ExtramaSpeciesPoint;
            if (!TamasSpecies.ContainsValue(name))
            {
                while (TamasSpecies.ContainsKey(numberOfSpecies))
                {
                    numberOfSpecies++;
                }
                TamasSpecies.Add(numberOfSpecies, name);
                return numberOfSpecies;
            }
            else
            {
                foreach (KeyValuePair<int, string> entry in TamasSpecies)
                {
                    if (entry.Value.Equals(name))
                    {
                        return entry.Key;
                    }
                }
            }
            return -1;
        }

        public int GenerateMonster(string path, string name,int inc)
        {
            if (name.Equals("Others"))
            {
                using (Stream stream = File.OpenRead(path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);

                    string str = Encoding.ASCII.GetString(bytes);
                    try
                    {
                        WaitingOtherForMonster = JsonConvert.DeserializeObject<Others>(str);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error parsing {name}: {ex.Message}");
                    }

                    
                    
                  
                }

                return inc;
            }

            int numberOfSpecies = GetMonsterSpecies(name);
            //    ExtramaSpeciesPoint;
            //if (!TamasSpecies.ContainsValue(name))
            //{
            //    while (TamasSpecies.ContainsKey(inc))
            //    {
            //        inc++;
            //    }
            //    TamasSpecies.Add(inc, name);
            //    numberOfSpecies = inc;
            //    inc++;
            //}
            //else
            //{
            //    foreach (KeyValuePair<int, string> entry in TamasSpecies)
            //    {
            //        if (entry.Value.Equals(name))
            //        {
            //            numberOfSpecies = entry.Key;
            //            break;
            //        }
            //    }
            //}
            WaitingMonsterSpecies = (Species)numberOfSpecies;
            if (MonsterLibrary.Data.ContainsKey((Species)numberOfSpecies))
            {
                return numberOfSpecies;
            }
            using (Stream stream = File.OpenRead(path))
            {
                byte[] bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);

                string str = Encoding.ASCII.GetString(bytes);
                try
                {
                    WaitingMonsterForOthers = JsonConvert.DeserializeObject<MonsterLibrary>(str);
                    MonsterLibrary.Data.Add((Species)numberOfSpecies, WaitingMonsterForOthers);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error parsing {name}: {ex.Message}");
                }
            }
            return inc;

        }



        public static Texture2D CreateTextureFromFile(string path, string filename)
        {

            Texture2D tex = new Texture2D(1, 1);
            try
            {
                using (Stream stream = File.OpenRead(path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    tex.filterMode = FilterMode.Point;  // Thought maybe this would help 
                    tex.LoadImage(bytes);

                    //var bytes = File.ReadAllBytes(path);
                    //tex.LoadImage(bytes);
                }
            }
            catch (Exception e)
            {
            }

            // Log("TextureReplacement v tex " + tex.isReadable + " " + tex.width + " " + tex.height);

            //  tex = tex.ToReadable();

            tex.filterMode = FilterMode.Point;
            tex.anisoLevel = 0;
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.name = filename;
            tex.Apply();
            return tex;
        }
        public static Sprite CreateSpriteFromFile(string path)
        {

            Texture2D tex = CreateTextureFromFile(path, path);
            Vector2 standardPivot = new Vector2(tex.width / 2f, tex.height / 2f);
            //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            return sprite;
        }
        public static Sprite GetSprite(Dictionary<string, Sprite> dict, string text)
        {
            if (dict.ContainsKey(text))
            {
                Sprite spriteb = dict[text];
                //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
                //  Sprite sprite = Sprite.Create(spriteb.texture, spriteb.rect, spriteb.pivot, 16);
                return spriteb;
            }
            return null;
        }

        public void SaveSpeciesFile(string to)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                bool first = false;
                foreach(KeyValuePair<int, string> entry in TamasSpecies)
                {
                    if (!first)
                    {
                        first = true;
                    }
                    else
                    {
                        stringBuilder.Append("\n");
                    }
                    stringBuilder.Append(entry.Key + ";" + entry.Value.Trim());
                }

                File.WriteAllBytes(to, Encoding.ASCII.GetBytes(stringBuilder.ToString()));
                
            }
            catch (Exception e)
            {
            }
        }
        public void LoadSpeciesFile(string from)
        {
            try
            {
                using (Stream stream = File.OpenRead(from))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);

                    string str = Encoding.ASCII.GetString(bytes); 
                    string[] lines=str.Split('\n');
                    foreach(string line in lines)
                    {
                        string[] lin = line.Split(';');
                        int inc = ExtramaSpeciesPoint;

                        int.TryParse(lin[0], out inc);

                        if (!TamasSpecies.ContainsKey(inc))
                        {
                            TamasSpecies.Add(  inc, lin[1].Trim());
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
        public static Sprite CreateSprite(string path)
        {

            Texture2D tex = new Texture2D(1, 1);
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("Extrama.Assets." + path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    tex.filterMode = FilterMode.Point;  // Thought maybe this would help 
                    tex.LoadImage(bytes);

                    //var bytes = File.ReadAllBytes(path);
                    //tex.LoadImage(bytes);
                }
            }
            catch (Exception e)
            {
            }

            tex.filterMode = FilterMode.Point;
            tex.anisoLevel = 0;
            tex.wrapMode = TextureWrapMode.Clamp;

            tex.Apply();
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            Vector2 standardPivot = new Vector2(tex.width / 2f, tex.height / 2f);
            //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            return sprite;
        }
    }


}