using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TextureReplacement.Scripts
{
    class OrbDict
    {


        public TextureOrb textureOrbIdle;
        public TextureOrb textureOrbRun;
        public TextureOrb textureOrbWalk;
        public TextureOrb textureOrbFlatLandIdle;
        public TextureOrb textureOrbFlatLandRun;
        public TextureOrb textureOrbFlatLandWalk;

        public TextureOrb textureOrbAztlandIdle;
        public TextureOrb textureOrbAztlandRun;
        public TextureOrb textureOrbAztlandWalk;

        public TextureOrb textureOrbShadowIdle;
        public TextureOrb textureOrbShadowRun;
        public TextureOrb textureOrbShadowWalk;

        public Floaty floaty;


        public OrbDict()
        {
            floaty = new Floaty();
            floaty.duration = 0.15;
            floaty.isGround = false;
            floaty.durationFlatland = 0.15;
            floaty.isGroundFlatland = false;

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
                if (floaty.durationFlatland < 0.05)
                {
                    floaty.durationFlatland = 0.15;
                }
                if (floaty.durationFlatland > 5)
                {
                    floaty.durationFlatland = 0.15;
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
                    bool flatland = filename.Contains("Flatland");
                    bool aztland = filename.Contains("Aztland");
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

                    Texture2D texture = TextureReplacement.CreateTextureFromFile(file, filename);
                    TextureOrb textureOrb = new TextureOrb();
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
                    else if (flatland)
                    {
                        if (walk)
                        {
                            textureOrbFlatLandWalk = textureOrb;
                        }
                        else if (run)
                        {
                            textureOrbFlatLandRun = textureOrb;
                        }
                        else
                        {
                            textureOrbFlatLandIdle = textureOrb;
                        }
                    }
                    else if (aztland)
                    {
                        if (walk)
                        {
                            textureOrbAztlandWalk = textureOrb;
                        }
                        else if (run)
                        {
                            textureOrbAztlandRun = textureOrb;
                        }
                        else
                        {
                            textureOrbAztlandIdle = textureOrb;
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

            if (textureOrbFlatLandRun != null && textureOrbFlatLandWalk == null)
            {
                textureOrbFlatLandWalk = textureOrbFlatLandRun;
            }
            if (textureOrbFlatLandRun == null && textureOrbFlatLandWalk != null)
            {
                textureOrbFlatLandRun = textureOrbFlatLandWalk;
            }
            if (textureOrbAztlandRun != null && textureOrbAztlandWalk == null)
            {
                textureOrbAztlandWalk = textureOrbAztlandRun;
            }
            if (textureOrbAztlandRun == null && textureOrbAztlandWalk != null)
            {
                textureOrbAztlandRun = textureOrbAztlandWalk;
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


        public void ReplaceFloaty(bool isFlatLand, bool isLowRes,bool isAztland)
        {

            GameObject orb = GameObject.FindGameObjectWithTag("Floaty");
            SpriteRenderer spriteRendererorb = orb.GetComponent<SpriteRenderer>();


            //Debug.Log("Orb " + spriteRendererorb);
            if (spriteRendererorb != null)
            {
                if (isFlatLand)
                {
                    SpriteAnimator Animatororb = orb.GetComponent<SpriteAnimator>();
                    FollowPlayer followPlayer = orb.GetComponent<FollowPlayer>();

                    if (Animatororb != null && followPlayer != null && followPlayer.enabled)
                    {
                        bool hasOrb = ((isFlatLand ? textureOrbFlatLandIdle : textureOrbIdle) != null);
                        if (hasOrb)
                        {
                            Animatororb.enabled = false;
                            followPlayer.enabled = false;

                            //  Physics2D.lay


                            OrbBehaviour orbBehaviour = orb.AddComponent<OrbBehaviour>();
                            orbBehaviour.Duration = (float)floaty.durationFlatland;
                            orbBehaviour.SetFollow(GameObject.FindGameObjectWithTag("Player").transform);
                            orbBehaviour.SetFloating(!floaty.isGroundFlatland, isFlatLand, isLowRes);
                            orbBehaviour.SetSprites(textureOrbFlatLandIdle, 0);
                            orbBehaviour.SetSprites(textureOrbFlatLandRun, 2);
                            orbBehaviour.SetSprites(textureOrbFlatLandWalk, 1);

                            //orbBehaviour.MaxSpeed = followPlayer.MaxSpeed;
                            //orbBehaviour.MinSpeed = followPlayer.MinSpeed;
                            //orbBehaviour.KeepDistance = followPlayer.KeepDistance;
                        }

                    }



                }
                else if (isAztland)
                {
                    SpriteAnimator Animatororb = orb.GetComponent<SpriteAnimator>();
                    FollowPlayer followPlayer = orb.GetComponent<FollowPlayer>();

                    if (Animatororb != null && followPlayer != null && followPlayer.enabled)
                    {
                        bool hasOrb = ((isAztland ? textureOrbAztlandIdle : textureOrbIdle) != null);
                        if (hasOrb)
                        {
                            Animatororb.enabled = false;
                            followPlayer.enabled = false;

                            //  Physics2D.lay


                            OrbBehaviour orbBehaviour = orb.AddComponent<OrbBehaviour>();
                            orbBehaviour.Duration = (float)floaty.durationFlatland;
                            orbBehaviour.SetFollow(GameObject.FindGameObjectWithTag("Player").transform);
                            orbBehaviour.SetFloating(!floaty.isGroundFlatland, isAztland, isLowRes);
                            orbBehaviour.SetSprites(textureOrbAztlandIdle, 0);
                            orbBehaviour.SetSprites(textureOrbAztlandRun, 2);
                            orbBehaviour.SetSprites(textureOrbAztlandWalk, 1);
                        }

                    }
                }
                else
                {
                    Animator Animatororb = orb.GetComponent<Animator>();
                    FollowPlayer followPlayer = orb.GetComponent<FollowPlayer>();

                    if (Animatororb != null && followPlayer != null && followPlayer.enabled)
                    {
                        bool hasOrb = ((isFlatLand ? textureOrbFlatLandIdle : textureOrbIdle) != null);
                        if (hasOrb)
                        {
                            Animatororb.enabled = false;
                            followPlayer.enabled = false;

                            //  Physics2D.lay

                            OrbBehaviour orbBehaviour = orb.AddComponent<OrbBehaviour>();
                            orbBehaviour.SetCustomShadow(textureOrbShadowIdle != null || textureOrbShadowWalk != null);
                            orbBehaviour.Duration = (float)floaty.duration;
                            orbBehaviour.SetFollow(GameObject.FindGameObjectWithTag("Player").transform);
                            orbBehaviour.SetFloating(!floaty.isGround, isFlatLand, isLowRes);
                            orbBehaviour.SetSprites(textureOrbIdle, 0);
                            orbBehaviour.SetSprites(textureOrbWalk, 1);
                            orbBehaviour.SetSprites(textureOrbRun, 2);
                            orbBehaviour.SetSprites(textureOrbShadowIdle, 3);
                            orbBehaviour.SetSprites(textureOrbShadowWalk, 4);
                            orbBehaviour.SetSprites(textureOrbShadowRun, 5);
                            //orbBehaviour.MaxSpeed = followPlayer.MaxSpeed;
                            //orbBehaviour.MinSpeed = followPlayer.MinSpeed;
                            //orbBehaviour.KeepDistance = followPlayer.KeepDistance;
                        }

                    }
                }



            }


        }
    }
}
