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

                orbBehaviours[i].SetOthersComponent(player.transform);

            }
        }
        public FollowerDict GetOrbDictFromMonster(Monster monster)
        {
            FollowerDict orbDict = null;

            if (FollowersBehaviour.OrbDicts.ContainsKey(monster.GetSpecies()))
            {
                orbDict = FollowersBehaviour.OrbDicts[monster.GetSpecies()];
            }
            else if(FollowersBehaviour.OrbDicts.ContainsKey(monster.BaseAncestor))
            {
                orbDict = FollowersBehaviour.OrbDicts[monster.BaseAncestor];
            }
          //  Debug.Log(monster.Species+" "+orbDict);

            return orbDict;
        }

        public void SetupFollowers(GameObject firstfollow)
        {
            List<Monster> monsters = GameState.Instance().Data.Player.MainParty;
            int inc = 0;
            GameObject follow = firstfollow;
            float keepdistance = 2.5f;
            float speed = 5f;
            foreach (Monster monster in monsters)
            {

                FollowerDict orbDict = GetOrbDictFromMonster(monster);
                if (orbDict!=null)
                {
                    if (orbDict.SetFollower(orbBehaviours[inc], follow, keepdistance,speed))
                    {
                        orbBehaviours[inc].transform.position=follow.transform.position;
                        orbBehaviours[inc].enabled = true;
                        orbBehaviours[inc].gameObject.SetActive(true);
                        orbBehaviours[inc].ResetToIdle();
                        orbBehaviours[inc].UnlockMovement();
                        follow = orbBehaviours[inc].gameObject;
                        keepdistance = 1f;
                        inc++;
                    }
                }

                if (inc > 2)
                {
                    break;
                }
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
