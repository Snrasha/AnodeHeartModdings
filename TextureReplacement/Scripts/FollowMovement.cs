
using System.Collections.Generic;
using UnityEngine;

namespace TextureReplacement.Scripts
{
    internal class FollowMovement
    {
        public float KeepDistance = 1.5f;

        public float MinSpeed = 1f;
        public float MaxSpeed = 5f;
        public float MiddleSpeed {  get; private set; }
        private float sqrKeepDistance;
        private float smallSqrKeepDistance;
        private float ColdownForLowRes = .5f;

        private Transform floaty;
        private Transform follow;
        private Rigidbody2D rigidbody2D;

        private Vector3 lastPos;
        private Vector3 prevPos;
        private List<Vector3> followPos = new List<Vector3>();

        public OrbBehaviour orbBehaviour;

        private bool flatland;

        //private Vector3 UnlockTest;
        private bool isUnlocked = false;

        //private float countBlocked = 0;


        public FollowMovement(OrbBehaviour orbBehaviour, Rigidbody2D rigidbody2D, bool flatland)
        {
            this.orbBehaviour = orbBehaviour;
            this.flatland = flatland;
            this.rigidbody2D = rigidbody2D;

            sqrKeepDistance = KeepDistance * KeepDistance;
            smallSqrKeepDistance = KeepDistance * KeepDistance/4f;

            MiddleSpeed = (MaxSpeed + MinSpeed) / 2;
        }

        public void SetTransform(Transform floaty, Transform follow)
        {
            this.floaty = floaty;
            this.follow = follow;
            isUnlocked = false;

        }

        void UpdateStandard()
        {

            // If empty, add the player position. And reset the last and previous of last.
            if (followPos.Count == 0)
            {
                followPos.Add(follow.position);
                lastPos=follow.position;
                prevPos = follow.position;
            }
            float num5 = floaty.position.DistanceTo(follow.position);
            if (num5 > 25)
            {
                rigidbody2D.velocity = Vector2.zero;
                floaty.position = follow.position;
                followPos.Clear();
            }


            // Check if the last position is near of the player, if no, add a vector to it.
            num5 = lastPos.SqrDistanceTo(follow.position);
            if (num5 > smallSqrKeepDistance)
            {

                float num6 = prevPos.SqrDistanceTo(follow.position);
                ////// check if the previous the last is more near of the follow, if yes ignore it.
                if (num5 < (num6 + smallSqrKeepDistance / 2f))
                {
                    followPos.Add(follow.position);
                    prevPos = lastPos;
                    lastPos = follow.position;
                }

            }

            // Check if the floaty is far of the player, if yes, begin to move and follow the path.
            float num = follow.position.SqrDistanceTo(this.floaty.position);
            if (num > sqrKeepDistance)
            {
                Vector3 togo = GetNextPos();
                if (followPos.Count == 0)
                {
                    return;
                }

                    //countBlocked += Time.deltaTime;
                    //if (countBlocked > 1f)
                    //{
                    //    rigidbody2D.isKinematic = true;
                    //}
                

                // Move to the point of the path.
                Vector3 vector = this.floaty.position.DirectionTo(togo);
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
        Vector3 GetNextPos()
        {
            Vector3 togo = followPos[0];

            // Look at the path and check if a point in the path need to be clean up.
            float dist;
                while (togo.SqrDistanceTo(this.floaty.position) < smallSqrKeepDistance / 2f)
            {


                followPos.RemoveAt(0);
                //countBlocked = 0;
                //rigidbody2D.isKinematic = false;

                if (followPos.Count == 0)
                {
                    return Vector3.zero;
                }
                togo = followPos[0];
            }
            int inc = 0;
            int index = 0;
            float distmin = togo.SqrDistanceTo(this.floaty.position);
            // Get the more near vector3. Remove every previous vector.
            foreach (Vector3 vector3 in followPos)
            {
                dist = vector3.SqrDistanceTo(this.floaty.position);
                if (dist < distmin)
                {
                    distmin = dist;
                    index = inc;
                }
                inc++;
            }
            togo = followPos[index];
            if (index > 0)
            {
                followPos.RemoveRange(0, index);
            }

            return togo;
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
        public void UnlockMovement()
        {
            if (rigidbody2D != null)
            {
                isUnlocked = true;
                rigidbody2D.velocity = Vector2.zero;
                this.floaty.position = follow.position;
                followPos.Clear();
            }
        }

        public void Update()
        {
            if (rigidbody2D == null)
            {
                UpdateFloating();
                return;
            }

            if (ColdownForLowRes > 0f)
            {
                ColdownForLowRes -= Time.deltaTime;
                return;
            }
            if (!isUnlocked)
            {
                return;
            }

            if (flatland)
            {
                UpdateFlatLand();
                return;
            }
            UpdateStandard();
        }
    }
}
