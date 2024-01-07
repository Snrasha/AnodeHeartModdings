using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Followers.Scripts
{
    public class FollowerDict
    {


        public TextureFollower textureOrbIdle;
        public TextureFollower textureOrbRun;
        public TextureFollower textureOrbWalk;

        public TextureFollower textureOrbShadowIdle;
        public TextureFollower textureOrbShadowRun;
        public TextureFollower textureOrbShadowWalk;

        public Floaty floaty;


        public FollowerDict()
        {
            floaty = new Floaty();
            floaty.duration = 0.15;
            floaty.isGround = false;

        }

        public void GetJson(string path)
        {

            using (Stream stream = File.OpenRead(path))
            {
                byte[] bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);

                string str = Encoding.ASCII.GetString(bytes);
                try
                {
                    floaty = JsonConvert.DeserializeObject<Floaty>(str);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error parsing Floaty.json: {ex.Message}");
                }
                if (floaty.duration < 0.05)
                {
                    floaty.duration = 0.15;
                }
                if (floaty.duration > 5)
                {
                    floaty.duration = 0.15;
                }
            }
        }

        public void SetFiles(string[] files)
        {
            foreach (string file in files)
            {


                string[] splut = file.Split('\\');
                string splut2 = splut[splut.Length - 1];
                string filename = splut2.Split('.')[0];
                if (splut2.Equals("Floaty.json"))
                {
                    GetJson(file);
                }

                if (filename.StartsWith("Orb"))
                {
                    bool hasDirection = filename.StartsWith("Orb4");
                    bool walk = filename.Contains("Walk");
                    bool run = filename.Contains("Run");
                    bool shadow = filename.Contains("Shadow");


                    string[] split = filename.Split('_');
                    int frames;
                    try
                    {
                        frames = int.Parse(split[split.Length - 1]);
                    }
                    catch
                    {
                        frames = 4;
                    }

                    Texture2D texture = FollowersPlugin.CreateTextureFromFile(file, filename);
                    TextureFollower textureOrb = new TextureFollower();
                    textureOrb.texture = texture;
                    textureOrb.hasDirection = hasDirection;
                    textureOrb.frames = frames;

                    if (shadow)
                    {
                        if (walk)
                        {
                            textureOrbShadowWalk = textureOrb;
                        }
                        else if (run)
                        {
                            textureOrbShadowRun = textureOrb;
                        }
                        else
                        {
                            textureOrbShadowIdle = textureOrb;
                        }
                    }
                    else
                    {
                        if (walk)
                        {
                            textureOrbWalk = textureOrb;
                        }
                        else if (run)
                        {
                            textureOrbRun = textureOrb;
                        }
                        else
                        {
                            textureOrbIdle = textureOrb;
                        }
                    }

                }
            }

            if (textureOrbRun != null && textureOrbWalk == null)
            {
                textureOrbWalk = textureOrbRun;
            }
            if (textureOrbRun == null && textureOrbWalk != null)
            {
                textureOrbRun = textureOrbWalk;
            }
            if (textureOrbShadowRun != null && textureOrbShadowWalk == null)
            {
                textureOrbShadowWalk = textureOrbShadowRun;
            }
            if (textureOrbShadowRun == null && textureOrbShadowWalk != null)
            {
                textureOrbShadowRun = textureOrbShadowWalk;
            }
        }


        public bool SetFollower(FollowerBehaviour orbBehaviour, GameObject follow, float keepdistance, float speed)
        {

            //Debug.Log(textureOrbIdle + " " + follow+" "+keepdistance);

            if (textureOrbIdle != null)
            {
                orbBehaviour.spritesIdle = new Sprite[4][];
                orbBehaviour.spritesWalk = new Sprite[4][];
                orbBehaviour.spritesRun = new Sprite[4][];

                orbBehaviour.spritesShadowIdle = new Sprite[4][];
                orbBehaviour.spritesShadowWalk = new Sprite[4][];
                orbBehaviour.spritesShadowRun = new Sprite[4][];
                orbBehaviour.FramesWalk = 0;


        orbBehaviour.SetCustomShadow(textureOrbShadowIdle != null || textureOrbShadowWalk != null);
                orbBehaviour.Duration = (float)floaty.duration;
                orbBehaviour.SetFollow(follow.transform);
                orbBehaviour.SetFloating(!floaty.isGround, keepdistance, speed);
                orbBehaviour.SetSprites(textureOrbIdle, 0);
                orbBehaviour.SetSprites(textureOrbWalk, 1);
                orbBehaviour.SetSprites(textureOrbRun, 2);
                orbBehaviour.SetSprites(textureOrbShadowIdle, 3);
                orbBehaviour.SetSprites(textureOrbShadowWalk, 4);
                orbBehaviour.SetSprites(textureOrbShadowRun, 5);

                orbBehaviour.spriteRenderer.sortingLayerID = follow.GetComponent<SpriteRenderer>().sortingLayerID;
                orbBehaviour.shadowSpriteRenderer.sortingLayerID = orbBehaviour.spriteRenderer.sortingLayerID;


                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
