using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Followers.Scripts;
using Followers.ModMenu;
using Universal.IconLib;
using Universal.ModMenu;

namespace Followers
{

    class FollowersPlugin
    {


        public static Dictionary<Species,FollowerDict> OrbDicts;

        public static FollowersSubMenuGUI followersSubMenuGUI;

        



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

        public static void UpdateFollowersGroup()
        {
            GameObject orb = GameObject.FindGameObjectWithTag("Floaty");
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (orb == null || player==null)
            {
                return;
            }
            if (player.GetComponent<CircleCollider2D>() == null)
            {
                return;
            }
            if (player.GetComponent<Rigidbody2D>() == null)
            {
                return;
            }


            Transform followersGroup = orb.transform.parent.Find("Followers");
            FollowerGroup followerBehaviour;
            if (followersGroup == null)
            {
                followersGroup = new GameObject("Followers").transform;
                followersGroup.parent = orb.transform.parent;
                followerBehaviour = followersGroup.gameObject.AddComponent<FollowerGroup>();
                followerBehaviour.Create(orb, player);
            }
            else
            {
                followerBehaviour = followersGroup.GetComponent<FollowerGroup>();
            }
            followerBehaviour.SetupFollowers(player);

        }



        public FollowersPlugin(BepInEx.Logging.ManualLogSource logger)
        {
            _logger = logger;
        }




        public void LoadAllTextures()
        {

            OrbDicts = new Dictionary<Species, FollowerDict>();
            Sprite ModIcon = CreateSprite("Icon.png");
            IconGUI.AddIcon(new Icon("Followers", "Followers", ModIcon));

            ModMenuGUI.AddSubMenu("Followers", followersSubMenuGUI);
            

            string appdataPath = Application.dataPath+ "/../";
            //string appdataPath = Application.persistentDataPath;

            appdataPath += "/ModsAssets";
            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
            }
            string appdataPathFollowers = appdataPath + "/Followers";

            if (!Directory.Exists(appdataPathFollowers))
            {
                Directory.CreateDirectory(appdataPathFollowers);
            }

            FollowersCase(appdataPathFollowers);
        }
        void FollowersCase(string path)
        {
            string[] followers = Directory.GetDirectories(path);

            foreach(string pathdir in followers)
            {
                string[] last2 = pathdir.Split('\\');
                string dirname = last2[last2.Length - 1];
                if (Enum.IsDefined(typeof(Species), dirname))
                {
                    //  Species.Anchory.
                    Species specie=dirname.ToEnum<Species>();
                    string[] files = Directory.GetFiles(pathdir);
                    OrbDicts.Add(specie, new FollowerDict());
                    OrbDicts[specie].SetFiles(files);
                }
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

        public static Sprite CreateSprite(string path)
        {

            Texture2D tex = new Texture2D(1, 1);
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("Followers.Assets." + path))
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
            tex.name = path;
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            Vector2 standardPivot = new Vector2(0.5f, 0.5f);
            //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            sprite.name = tex.name;
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            return sprite;
        }
    }
}