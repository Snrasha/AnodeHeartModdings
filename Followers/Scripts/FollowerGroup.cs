using Followers.ModMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Followers.Scripts
{
    public class FollowerGroup: MonoBehaviour
    {
        public FollowerBehaviour[] orbBehaviours;


        public void Create(GameObject floaty, GameObject player)
        {
            SpriteRenderer spriteRendererplayer = player.GetComponent<SpriteRenderer>();

            SpriteRenderer spriteRendererfloaty = floaty.GetComponent<SpriteRenderer>();

            if (spriteRendererplayer == null)
            {
                return;
            }


            orbBehaviours = new FollowerBehaviour[3];
            for (int i = 0; i < orbBehaviours.Length; i++)
            {
                GameObject gameobject = new GameObject("Follower_" + i);
                gameobject.transform.parent = this.transform;
                SpriteRenderer spriteRenderer= spriteRendererplayer.CopyComponent(gameobject);
                if (spriteRendererfloaty != null)
                {
                    spriteRenderer.sharedMaterial = spriteRendererfloaty.sharedMaterial;
                }



                orbBehaviours[i]=gameobject.AddComponent<FollowerBehaviour>();
                orbBehaviours[i].enabled = false;

                orbBehaviours[i].SetOthersComponent(player.transform, floaty.transform);

            }
        }
        public FollowerDict GetOrbDictFromMonster(Monster monster)
        {
            FollowerDict orbDict = null;

            if (FollowersPlugin.OrbDicts.ContainsKey(monster.GetSpecies()))
            {
                orbDict = FollowersPlugin.OrbDicts[monster.GetSpecies()];
            }
            else if(FollowersPlugin.OrbDicts.ContainsKey(monster.BaseAncestor))
            {
                orbDict = FollowersPlugin.OrbDicts[monster.BaseAncestor];
            }
          //  Debug.Log(monster.Species+" "+orbDict);

            return orbDict;
        }
        public FollowerDict GetOrbDictFromSpecies(Species specie)
        {
            FollowerDict orbDict = null;


            if (FollowersPlugin.OrbDicts.ContainsKey(specie))
            {
                orbDict = FollowersPlugin.OrbDicts[specie];
            }
            else
            {
                if (MonsterLibrary.Data.ContainsKey(specie)){
                    if (FollowersPlugin.OrbDicts.ContainsKey(MonsterLibrary.Data[specie].BaseAncestor))
                    {
                        orbDict = FollowersPlugin.OrbDicts[MonsterLibrary.Data[specie].BaseAncestor];
                    }
                }
            }
            //  Debug.Log(monster.Species+" "+orbDict);

            return orbDict;
        }
        public void TeleportFollowers(GameObject togo)
        {
            if (togo != null)
            {
                for (int inc =0; inc < orbBehaviours.Length; inc++) {
                    orbBehaviours[inc].UnlockMovement();
                }
            }
        }


        public void SetupFollowers(GameObject firstfollow)
        {
            GameObject follow = firstfollow;
            float keepdistance = 2.5f;

            float speed =6f;
            int inc = 0;
            Debug.Log("SetupFollowers");
            Config config = FollowersPlugin.followersSubMenuGUI.config;
            if (config.option_followers == 0)
            {

                List<Monster> monsters = GameState.Instance().Data.Player.MainParty;
               
                foreach (Monster monster in monsters)
                {

                    FollowerDict orbDict = GetOrbDictFromMonster(monster);
                    if (orbDict != null)
                    {
                        if (orbDict.SetFollower(orbBehaviours[inc], follow, keepdistance, speed, monster.AltColor))
                        {
                            orbBehaviours[inc].transform.position = follow.transform.position;
                            orbBehaviours[inc].enabled = true;
                            orbBehaviours[inc].gameObject.SetActive(true);
                            orbBehaviours[inc].ResetToIdle();
                            orbBehaviours[inc].UnlockMovement();
                            follow = orbBehaviours[inc].gameObject;
                            keepdistance = 1f;
                            speed = 10f;
                            inc++;
                        }
                    }

                    if (inc > 2)
                    {
                        break;
                    }
                }

            }
            else
            {
                string speciestr = config.option_species;
                bool altcolor = config.altcolor;
                Species specie;
                if (Enum.IsDefined(typeof(Species), speciestr))
                {
                    specie = (Species)Enum.Parse(typeof(Species), speciestr);
                }
                else
                {
                    specie = Species.Beebee;
                }
            

                    FollowerDict orbDict = GetOrbDictFromSpecies(specie);
                if (orbDict != null)
                {
                    if (orbDict.SetFollower(orbBehaviours[inc], follow, keepdistance, speed, altcolor))
                    {
                        orbBehaviours[inc].transform.position = follow.transform.position;
                        orbBehaviours[inc].enabled = true;
                        orbBehaviours[inc].gameObject.SetActive(true);
                        orbBehaviours[inc].ResetToIdle();
                        orbBehaviours[inc].UnlockMovement();
                    }
                }
                inc = 1;

            }
            for (; inc < orbBehaviours.Length; inc++)
            {
                orbBehaviours[inc].enabled = false;
                orbBehaviours[inc].gameObject.SetActive(false);
            }
        }


        public void UnlockMovement()
        {
            if (orbBehaviours == null)
            {
                return;
            }

            for (int i = 0; i < orbBehaviours.Length; i++)
            {
                orbBehaviours[i].UnlockMovement();
            }

        }

    }
}
