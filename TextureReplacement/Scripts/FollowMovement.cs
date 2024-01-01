using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TextureReplacement.Scripts
{
    internal class FollowMovement
    {
        public float KeepDistance = 1.5f;

        public float MinSpeed = 1f;
        public float MaxSpeed = 10f;
        public float MiddleSpeed {  get; private set; }
        private float sqrKeepDistance;
        private float smallSqrKeepDistance;
     //   private float ColdownForLowRes = 1f;

        private Transform floaty;
        private Transform follow;
        private Rigidbody2D rigidbody2D;

        private Vector3 lastPos;
        private Vector3 prevPos;
        private Queue<Vector3> followPos = new Queue<Vector3>();

        public OrbBehaviour orbBehaviour;

        private bool flatland;
        private bool lowres;

        private Vector3 UnlockTest;
        private bool isUnlocked = false;


        public FollowMovement(OrbBehaviour orbBehaviour, Rigidbody2D rigidbody2D, bool flatland, bool lowres)
        {
            this.orbBehaviour = orbBehaviour;
            this.flatland = flatland;
            this.lowres = lowres;
            this.rigidbody2D = rigidbody2D;

            sqrKeepDistance = KeepDistance * KeepDistance;
            smallSqrKeepDistance = KeepDistance / 4f * KeepDistance;

            MiddleSpeed = (MaxSpeed + MinSpeed) / 2;
        }

        public void SetTransform(Transform floaty, Transform follow)
        {
            this.floaty = floaty;
            this.follow = follow;
            UnlockTest = this.follow.position;
            isUnlocked = false;

        }
        void UpdateLowRes()
        {


            //float num = follow.position.SqrDistanceTo( this.floaty.position);

            if (followPos.Count == 0)
            {
                followPos.Enqueue(follow.position);
                lastPos=follow.position;
                prevPos = follow.position;

            }

          //  Debug.Log(followPos.Count + " " + followPos.Peek() + " " + follow.position + " " + num);

            if (follow.position.SqrDistanceTo(this.floaty.position) > sqrKeepDistance * 16)
            {
                rigidbody2D.velocity = Vector2.zero;
                this.floaty.position = follow.position;
                followPos.Clear();
                return;
                // 
            }
            float num5 = lastPos.SqrDistanceTo(follow.position);
            if (num5 > smallSqrKeepDistance)
            {
                float num6 =prevPos.SqrDistanceTo(follow.position);
                // check if the previous the last is more near of the follow, if yes ignore it.
                if (num5 > (num6 + smallSqrKeepDistance / 4f))
                {
                    followPos.Enqueue(follow.position);
                    prevPos = lastPos;
                    lastPos = follow.position;
                }
            }

            float num = follow.position.SqrDistanceTo(this.floaty.position);
            if (num > smallSqrKeepDistance)
            {
                Vector3 togo = followPos.Peek();
                
                float dist = togo.SqrDistanceTo(this.floaty.position);
                while (dist < smallSqrKeepDistance)
                {
                    followPos.Dequeue();
                    togo = followPos.Peek();
                    togo.SqrDistanceTo(this.floaty.position);
                    if (followPos.Count == 0)
                    {
                        return;
                    }
                }
                Vector3 vector = this.floaty.position.DirectionTo(togo);



                float num2 = Mathf.Clamp(0.4f * num, MinSpeed, MaxSpeed);
                orbBehaviour.SetAnimatorDirection(vector, num2);
                rigidbody2D.velocity = num2 * vector;
            }
            else
            {
                if (followPos.Count > 1)
                {
                    followPos.Dequeue();
                }
                rigidbody2D.velocity /= 2;

                orbBehaviour.ResetToIdle();
            }
        }
        void UpdateFlatLand()
        {
            float num = follow.position.SqrDistanceTo(this.floaty.position);


            if (num > sqrKeepDistance)
            {
                if (num > sqrKeepDistance * 16)
                {
                    rigidbody2D.velocity = Vector2.zero;
                    this.floaty.position = Vector3.Lerp(this.floaty.position, follow.position, 0.8f);
                }
                Vector3 vector = this.floaty.position.DirectionTo(follow.position);
                float num2 = Mathf.Clamp(0.4f * num, MinSpeed, MaxSpeed);
                orbBehaviour.SetAnimatorDirection(vector, num2);

                rigidbody2D.velocity = num2 * new Vector2(vector.x, 0);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x / 2, rigidbody2D.velocity.y);


                orbBehaviour.ResetToIdle();
            }
        }
        void UpdateStandard()
        {
            float num = follow.position.SqrDistanceTo(this.floaty.position);

            if (num > sqrKeepDistance)
            {
                if ( num > sqrKeepDistance * 16)
                {
                    rigidbody2D.velocity = Vector2.zero;
                    this.floaty.position = Vector3.Lerp(this.floaty.position, follow.position, 0.8f);
                }



                Vector3 vector = this.floaty.position.DirectionTo(follow.position);
                float num2 = Mathf.Clamp(0.4f * num, MinSpeed, MaxSpeed);

                orbBehaviour.SetAnimatorDirection(vector, num2);
                rigidbody2D.velocity = num2 * vector;

            }
            else
            {
                rigidbody2D.velocity /= 2;
                orbBehaviour.ResetToIdle();
            }
        }

        void UpdateFloating()
        {
            float num = follow.position.SqrDistanceTo(this.floaty.position);

            if (num > sqrKeepDistance)
            {

                Vector3 vector = this.floaty.position.DirectionTo(follow.position);
                float num2 = Mathf.Clamp(0.4f * num, MinSpeed, MaxSpeed);

                orbBehaviour.SetAnimatorDirection(vector, num2);

                this.floaty.position += num2 * Time.deltaTime * vector;

            }
            else
            {
                orbBehaviour.ResetToIdle();
            }
        }

        public void Update()
        {
            if (rigidbody2D == null)
            {
                UpdateFloating();
                return;
            }

            if (!isUnlocked)
            {
                if (UnlockTest.SqrDistanceTo(follow.position)>0.5f)
                {
                    isUnlocked = true;
                    rigidbody2D.velocity = Vector2.zero;
                    this.floaty.position = follow.position;
                    followPos.Clear();
                }
                return;
            }


            //if (ColdownForLowRes > 0f)
            //{
            //    ColdownForLowRes -= Time.deltaTime;

            //    if (ColdownForLowRes <= 0f)
            //    {
            //        rigidbody2D.velocity = Vector2.zero;
            //        this.floaty.position = follow.position;
            //        followPos.Clear();
            //    }
            //    return;
            //}

            if (flatland)
            {
                UpdateFlatLand();
                return;
            }
            UpdateLowRes();
            //if (lowres)
            //{
            //    UpdateLowRes();
            //    return;
            //}
            //UpdateStandard();
        }
    }
}
