using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TextureReplacement
{

    class TextureReplacement : MonoBehaviour
    {

        public static Dictionary<string, Sprite> SpritesFronts;
        public static Dictionary<string, Sprite> SpritesAltFronts;

        public static Dictionary<string, Sprite> SpritesIcons;
        public static Dictionary<string, Sprite> SpritesCharacterIcons;

        public static Dictionary<string, Sprite> SpritesAltIcons;

        public static Dictionary<string, Sprite> SpritesFrontsGlitch;

        public static Dictionary<string, Sprite> SpritesOverworldsIdle;
        public static Dictionary<string, Sprite> SpritesOverworldsWalk;
        public static Dictionary<string, Sprite> SpritesGrid;
        public static Dictionary<GameCharacterAnimationType, Sprite> SpritesAnimationPlayer;


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




        public void LoadAllTextures()
        {
            SpritesFronts = new Dictionary<string, Sprite>();
            SpritesIcons = new Dictionary<string, Sprite>();
            SpritesAltFronts = new Dictionary<string, Sprite>();
            SpritesAltIcons = new Dictionary<string, Sprite>();
            SpritesCharacterIcons = new Dictionary<string, Sprite>();  
            SpritesFrontsGlitch = new Dictionary<string, Sprite>();
            SpritesOverworldsIdle = new Dictionary<string, Sprite>();
            SpritesOverworldsWalk   = new Dictionary<string, Sprite>();
            SpritesAnimationPlayer = new Dictionary<GameCharacterAnimationType, Sprite>();




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

                Directory.CreateDirectory(appdataPathMonsters + "/Ignafir");
                CreateFile("Ignafir.Ignafir_Front.png", appdataPathMonsters + "/Ignafir/Ignafir_Front.png");
                CreateFile("Ignafir.Ignafir_Icon.png", appdataPathMonsters + "/Ignafir/Ignafir_Icon.png");
                CreateFile("Ignafir.Ignafir_AltFront.png", appdataPathMonsters + "/Ignafir/Ignafir_AltFront.png");
                CreateFile("Ignafir.Ignafir_AltIcon.png", appdataPathMonsters + "/Ignafir/Ignafir_AltIcon.png");
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

                    //   Log("TextureReplacement monsters files " + file);
                    string[] split = splut2.Split('.')[0].Split('_');
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
                            SpritesFronts.Add("Monsters/Fronts/" + merge, CreateSpriteFromAppData(file));
                        }
                        if ("AltFront".Equals(end))
                        {
                            SpritesAltFronts.Add("Monsters/AltFronts/" + merge, CreateSpriteFromAppData(file));
                        }
                        
                        if ("Icon".Equals(end))
                        {
                            SpritesIcons.Add("Monsters/Icons/" + merge, CreateSpriteFromAppData(file));
                        }
                        if ("AltIcon".Equals(end))
                        {
                            SpritesAltIcons.Add("Monsters/AltIcons/" + merge, CreateSpriteFromAppData(file));
                        }
                        if ("OverworldsIdle".Equals(end))
                        {
                            SpritesOverworldsIdle.Add("Monsters/Overworlds/" + merge + "_Idle", CreateSpriteFromAppData(file));
                        }
                        if ("OverworldsWalk".Equals(end))
                        {
                            SpritesOverworldsWalk.Add("Monsters/Overworlds/" + merge + "_Walk", CreateSpriteFromAppData(file));
                        }
                        if ("FrontsGlitch".Equals(end))
                        {
                            SpritesFrontsGlitch.Add("Monsters/FrontsGlitch/" + merge, CreateSpriteFromAppData(file));
                        }
                        if ("Grid".Equals(end))
                        {
                            SpritesGrid.Add("Monsters/Grid/" + merge, CreateSpriteFromAppData(file));
                        }
                        if ("CharacterIcon".Equals(end))
                        {
                            SpritesCharacterIcons.Add("Characters/Icons/" + merge, CreateSpriteFromAppData(file));
                        }
                    }
                }
            }
        }
        void PlayerCase(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string[] splut = file.Split('\\');
                string splut2 = splut[splut.Length - 1];

                //   Log("TextureReplacement monsters files " + file);
                string[] split = splut2.Split('.')[0].Split('_');
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
                    switch (end)
                    {
                        case "Walk":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Walk, CreateSpriteFromAppData(file));
                            break;
                        case "Idle":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Idle, CreateSpriteFromAppData(file));
                            break;
                        case "Fall":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Fall, CreateSpriteFromAppData(file));
                            break;
                        case "IdleClimb":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.IdleClimb, CreateSpriteFromAppData(file));
                            break;
                        case "Fish":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Fish, CreateSpriteFromAppData(file));
                            break;
                        case "Climb":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Climb, CreateSpriteFromAppData(file));
                            break;
                        case "Run":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Run, CreateSpriteFromAppData(file));
                            break;
                        case "Shovel":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Shovel, CreateSpriteFromAppData(file));
                            break;
                        case "Sleep":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Sleep, CreateSpriteFromAppData(file));
                            break;
                        case "Wake":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Custom, CreateSpriteFromAppData(file));
                            break;
                        case "Fainted":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Fainted, CreateSpriteFromAppData(file));
                            break;
                        case "Drop":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Drop, CreateSpriteFromAppData(file));
                            break;
                        case "Rise":
                            SpritesAnimationPlayer.Add(GameCharacterAnimationType.Rise, CreateSpriteFromAppData(file));
                            break;
                    }
                   

                    if ("Icon".Equals(end))
                    {
                        SpritesCharacterIcons.Add("Characters/Icons/" + merge, CreateSpriteFromAppData(file));
                    }
                }
            }
        }



        void Awake()
        {

            LoadAllTextures();


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
        public static Sprite CreateSpriteFromAppData(string path)
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

            tex.Apply();
            Vector2 standardPivot = new Vector2(tex.width / 2f, tex.height / 2f);
            //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            return sprite;
        }


        //public static Texture2D CreateTexture(string path)
        //{

        //    Texture2D tex = new Texture2D(1, 1);
        //    try
        //    {
        //        var assembly = Assembly.GetExecutingAssembly();
        //        using (Stream stream = assembly.GetManifestResourceStream("TextureReplacement.Assets." + path))
        //        {
        //            byte[] bytes = new byte[stream.Length];

        //            stream.Read(bytes, 0, bytes.Length);
        //            tex.filterMode = FilterMode.Point;  // Thought maybe this would help 
        //            tex.LoadImage(bytes);

        //            //var bytes = File.ReadAllBytes(path);
        //            //tex.LoadImage(bytes);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //    }

        //    Log("TextureReplacement v tex " + tex.isReadable+" "+tex.width+" "+tex.height);

        //    tex =tex.ToReadable();

        //    tex.filterMode = FilterMode.Point;
        //    tex.anisoLevel = 0;
        //    tex.wrapMode = TextureWrapMode.Clamp;

        //    tex.Apply();
        //    //Log("TextureReplacement v tex " + tex.isReadable, true);
        //    return tex;
        //}
    }
}