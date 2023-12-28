using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;

namespace TextureReplacement
{

    class TextureReplacement
    {

        public static Dictionary<string, Sprite> SpritesFronts;
        public static Dictionary<string, Sprite> SpritesAltFronts;

        public static Dictionary<string, Sprite> SpritesIcons;
        public static Dictionary<string, Sprite> SpritesAltIcons;
        public static Dictionary<string, Sprite> SpritesCharacterIcons;

        public static Sprite ModIcon;


        public static Dictionary<string, Sprite> SpritesFrontsGlitch;

        public static Dictionary<string, Texture2D> SpritesOverworldsIdle;
        public static Dictionary<string, Texture2D> SpritesOverworldsWalk;
        public static Dictionary<string, Sprite> SpritesGrid;
        public static Dictionary<string, Texture2D> SpritesAnimationPlayer;
     //   public static Dictionary<GameCharacterAnimationType, Texture2D> SpritesScooterAnimationPlayer;
    //    public static Dictionary<GameCharacterAnimationType, Texture2D> SpritesFlatlandAnimationPlayer;
   //     public static Dictionary<GameCharacterAnimationType, Texture2D> SpritesLowResAnimationPlayer;

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
        public static Sprite GetSprite(Dictionary<GameCharacterAnimationType, Sprite> dict, GameCharacterAnimationType type)
        {
            if (dict.ContainsKey(type))
            {
                Sprite spriteb = dict[type];
                //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
                //  Sprite sprite = Sprite.Create(spriteb.texture, spriteb.rect, spriteb.pivot, 16);
                return spriteb;
            }
            return null;
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
        public TextureReplacement(BepInEx.Logging.ManualLogSource logger)
        {
            _logger = logger;
        }




        public void LoadAllTextures()
        {
            ModIcon = CreateSprite("Icon.png");

            SpritesFronts = new Dictionary<string, Sprite>();
            SpritesIcons = new Dictionary<string, Sprite>();
            SpritesAltFronts = new Dictionary<string, Sprite>();
            SpritesAltIcons = new Dictionary<string, Sprite>();
            SpritesCharacterIcons = new Dictionary<string, Sprite>();
            SpritesGrid = new Dictionary<string, Sprite>();

            SpritesFrontsGlitch = new Dictionary<string, Sprite>();
            SpritesOverworldsIdle = new Dictionary<string, Texture2D>();
            SpritesOverworldsWalk   = new Dictionary<string, Texture2D>();
        //    SpritesAnimationPlayer = new Dictionary<GameCharacterAnimationType, Texture2D>();
            SpritesAnimationPlayer = new Dictionary<string, Texture2D>();

            //SpritesScooterAnimationPlayer = new Dictionary<GameCharacterAnimationType, Texture2D>();
            //SpritesFlatlandAnimationPlayer = new Dictionary<GameCharacterAnimationType, Texture2D>();
            //SpritesLowResAnimationPlayer = new Dictionary<GameCharacterAnimationType, Texture2D>();



            string appdataPath = Application.dataPath+ "/../";
            //string appdataPath = Application.persistentDataPath;

            appdataPath += "/ModsAssets";
            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
            }
            appdataPath += "/Replacements";
            string appdataPathMonsters= appdataPath+ "/Monsters";
            string appdataPathPlayer = appdataPath + "/Player";

            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
                Directory.CreateDirectory(appdataPathMonsters);
                Directory.CreateDirectory(appdataPathPlayer);

                //Directory.CreateDirectory(appdataPathMonsters + "/Ignafir");
                //CreateFile("Ignafir.Ignafir_Front.png", appdataPathMonsters + "/Ignafir/Ignafir_Front.png");
                //CreateFile("Ignafir.Ignafir_Icon.png", appdataPathMonsters + "/Ignafir/Ignafir_Icon.png");
                //CreateFile("Ignafir.Ignafir_AltFront.png", appdataPathMonsters + "/Ignafir/Ignafir_AltFront.png");
                //CreateFile("Ignafir.Ignafir_AltIcon.png", appdataPathMonsters + "/Ignafir/Ignafir_AltIcon.png");
            }

            string[] monsters = Directory.GetDirectories(appdataPathMonsters);

            PlayerCase(appdataPathPlayer);

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
                        for(int i=0; i<split.Length-1; i++)
                        {
                            merge += split[i];
                            if (i < split.Length - 2)
                            {
                                merge += "_";
                            }
                        }

                        string end = split[split.Length - 1];
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
                        if ("Idle".Equals(end))
                        {
                            SpritesOverworldsIdle.Add("Monsters/Overworlds/" + merge + "_Idle", CreateTextureFromFile(file, merge));
                        }
                        if ("Walk".Equals(end))
                        {
                            SpritesOverworldsWalk.Add("Monsters/Overworlds/" + merge + "_Walk", CreateTextureFromFile(file, merge));
                        }
                        if ("FrontsGlitch".Equals(end))
                        {
                            SpritesFrontsGlitch.Add("Monsters/FrontsGlitch/" + merge, CreateSpriteFromFile(file));
                        }
                        if ("Grid".Equals(end))
                        {
                            SpritesGrid.Add("Monsters/Grid/" + merge, CreateSpriteFromFile(file));
                        }
                        //if ("CharacterIcon".Equals(end))
                        //{
                        //    SpritesCharacterIcons.Add("Characters/Icons/" + merge, CreateSpriteFromFile(file));
                        //}
                    }
                }
            }
        }
        void PlayerCase(string path)
        {
            string[] files = Directory.GetFiles(path);
            //foreach (string file in files)
            //{
            //    string[] splut = file.Split('\\');
            //    string splut2 = splut[splut.Length - 1];
            //    string filename = splut2.Split('.')[0];
            //    Debug.Log($"File {filename} !");
            //}

            foreach (string file in files)
            {


                string[] splut = file.Split('\\');
                string splut2 = splut[splut.Length - 1];
                string filename = splut2.Split('.')[0];


                //_logger.LogInfo($"Filename {filename} !");
                //Debug.Log($"Filename {filename} !");
                switch (filename)
                {
                    case "Player":
                        SpritesCharacterIcons.Add("Characters/Icons/" + filename, CreateSpriteFromFile(file));
                        break;
                    case "Player_Blink":
                        SpritesCharacterIcons.Add("Characters/Icons/" + filename, CreateSpriteFromFile(file));
                        break;
                    case "Player_Cry":
                        SpritesCharacterIcons.Add("Characters/Icons/" + filename, CreateSpriteFromFile(file));
                        break;
                    case "Player_XD":
                        SpritesCharacterIcons.Add("Characters/Icons/" + filename, CreateSpriteFromFile(file));
                        break;
                    case "PlayerCursor":
                        SpritesCharacterIcons.Add(filename, CreateSpriteFromFile(file));
                        break;
                    case "PlayerOnAirboat":
                        SpritesGrid.Add("PlayerOnAirboat", CreateSpriteFromFile(file));
                        break;
                    case "Player_Walk":
                    case "Player_Idle":
                    case "Player_Fall":
                    case "Player_IdleClimb":
                    case "Player_Fish":
                    case "Player_Climb":
                    case "Player_Run":
                    case "Player_Shovel":
                    case "Player_Sleep":
                    case "Player_Fainted":
                    case "Player_Drop":
                    case "Player_Flatland_Idle":
                    case "Player_Flatland_Run":
                    //  case "PlayerShadow":
                    case "Player_Lowres_Idle":
                    case "Player_Lowres_Run":
                    case "Player_Scooter":
                    case "Player_Scooter_Idle":
                        SpritesAnimationPlayer.Add(filename, CreateTextureFromFile(file, filename));
                        break;
                    case "Player_Wake":
                    case "Player_Rise":
                        if (!SpritesAnimationPlayer.ContainsKey("Player_Rise")){
                            SpritesAnimationPlayer.Add("Player_Rise", CreateTextureFromFile(file, "Player_Rise"));
                            SpritesAnimationPlayer.Add("Player_Wake", CreateTextureFromFile(file, "Player_Wake"));
                        }

                        break;


                }
            }
        }




        public void CreateFile(string path, string to)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("TextureReplacement.Assets." + path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    File.WriteAllBytes(to, bytes);
                }
            }
            catch (Exception e)
            {
            }
        }
        public static Texture2D CreateTextureFromFile(string path,string filename)
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

            Texture2D tex = CreateTextureFromFile(path,path);
            Vector2 standardPivot = new Vector2(tex.width / 2f, tex.height / 2f);
            //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            return sprite;
        }


        public static Sprite CreateSprite(string path)
        {

            Texture2D tex = new Texture2D(1, 1);
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("TextureReplacement.Assets." + path))
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