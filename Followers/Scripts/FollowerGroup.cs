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
        public OrbBehaviour[] orbBehaviours;


        public void Create(GameObject floaty, GameObject player)
        {
            SpriteRenderer spriteRendererplayer = player.GetComponent<SpriteRenderer>();
   
            if (spriteRendererplayer == null)
            {
                return;
            }


            orbBehaviours = new OrbBehaviour[3];
            for (int i = 0; i < orbBehaviours.Length; i++)
            {
                GameObject gameobject = new GameObject("Follower_" + i);
                gameobject.transform.parent = this.transform;
                SpriteRenderer spriteRenderer= spriteRendererplayer.CopyComponent(gameobject);


                orbBehaviours[i]=gameobject.AddComponent<OrbBehaviour>();
                orbBehaviours[i].enabled = false;

                orbBehaviours[i].SetOthersComponent(player.transform);

            }
        }
        public OrbDict GetOrbDictFromMonster(Monster monster)
        {
            OrbDict orbDict = null;

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

        public void SetupFollowers(GameObject floaty)
        {
            List<Monster> monsters = GameState.Instance().Data.Player.MainParty;
            int inc = 0;
            GameObject follow = floaty;
            float keepdistance = 0.5f;
            float speed = 20f;
            foreach (Monster monster in monsters)
            {

                OrbDict orbDict = GetOrbDictFromMonster(monster);
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
                        keepdistance = 0.5f;
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
