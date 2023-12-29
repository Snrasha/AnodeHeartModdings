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

        public Floaty floaty;


        public OrbDict()
        {
            floaty = new Floaty();
            floaty.duration = 0.15;
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
            }
        }

            public void SetFiles(string[] files)
        {
            foreach (string file in files)
            {


                string[] splut = file.Split('\\');
                string splut2 = splut[splut.Length - 1];
                string filename = splut2.Split('.')[0];
                if (splut2.Equals("Floaty.json")){
                    GetJson(file); 
                }

                if (filename.StartsWith("Orb"))
                {
                    bool flatland = filename.Contains("Flatland");
                    bool hasDirection = filename.StartsWith("Orb4");
                    bool walk = filename.Contains("Walk");
                    bool run = filename.Contains("Run");


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
                    Texture2D texture = TextureReplacement.CreateTextureFromFile(file,file);
                    TextureOrb textureOrb = new TextureOrb();
                    textureOrb.texture = texture;
                    textureOrb.hasDirection = hasDirection;
                    textureOrb.frames = frames;


                    if (flatland)
                    {
                        if (walk)
                        {
                            textureOrbFlatLandWalk= textureOrb;
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

            if (textureOrbFlatLandRun != null && textureOrbFlatLandWalk==null)
            {
                textureOrbFlatLandWalk = textureOrbFlatLandRun;
            }
            if (textureOrbFlatLandRun == null && textureOrbFlatLandWalk != null)
            {
                textureOrbFlatLandRun = textureOrbFlatLandWalk;
            }
            if (textureOrbRun != null && textureOrbWalk == null)
            {
                textureOrbWalk = textureOrbRun;
            }
            if (textureOrbRun == null && textureOrbWalk != null)
            {
                textureOrbRun = textureOrbWalk;
            }
        }


        public void ReplaceFloaty(bool isFlatLand)
        {

            GameObject orb = GameObject.FindGameObjectWithTag("Floaty");
            SpriteRenderer spriteRendererorb = orb.GetComponent<SpriteRenderer>();


            //Debug.Log("Orb " + spriteRendererorb);
            if (spriteRendererorb != null)
            {
                Animator Animatororb = orb.GetComponent<Animator>();
                FollowPlayer followPlayer = orb.GetComponent<FollowPlayer>();

                if (Animatororb != null && followPlayer != null && followPlayer.enabled)
                {
                    bool hasOrb = ((isFlatLand ? textureOrbFlatLandIdle : textureOrbIdle) != null);
                    if (hasOrb )
                    {
                        Animatororb.enabled = false;
                        followPlayer.enabled = false;


                        OrbBehaviour orbBehaviour = orb.AddComponent<OrbBehaviour>();
                        orbBehaviour.Duration = (float)floaty.duration;
                        orbBehaviour.SetSprites((isFlatLand ? textureOrbFlatLandIdle : textureOrbIdle), 0);
                        orbBehaviour.SetSprites((isFlatLand ? textureOrbFlatLandRun : textureOrbRun), 2);
                        orbBehaviour.SetSprites((isFlatLand ? textureOrbFlatLandWalk : textureOrbWalk), 1);

                        orbBehaviour.MaxSpeed = followPlayer.MaxSpeed;
                        orbBehaviour.MinSpeed = followPlayer.MinSpeed;
                        orbBehaviour.KeepDistance = followPlayer.KeepDistance;
                    }

                }
            }


        }
    }
}
