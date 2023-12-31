

using System;
using UnityEngine;

namespace TextureReplacement.Scripts
{
    public class OrbBehaviour : MonoBehaviour
    {
        public float KeepDistance = 1.5f;

        public float MinSpeed = 1f;

        public float MaxSpeed = 5f;

        private Transform follow;

        private float sqrKeepDistance;


        public Sprite[][] spritesIdle = new Sprite[4][];
        public Sprite[][] spritesWalk = new Sprite[4][];
        public Sprite[][] spritesRun = new Sprite[4][];

        public int FramesIdle = 4;
        public int FramesWalk = 0;
        public int FramesRun = 4;
        public int Frames = 4;

        private float timeTillNextFrame;
        private int currentAnimationRow;
        public bool IsSimpleSheet;
        public SpriteRenderer spriteRenderer;
        public float Duration = 0.15f;//0.8f

        public int animationType;
        Rigidbody2D rigidbody2D;
        private bool flatland;

        public GameCharacterDirection currentDirection { get; private set; }

        private int currentFrame;


        public void Awake()
        {
            sqrKeepDistance = KeepDistance * KeepDistance;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 0;
            

        }
       public void SetFollow(Transform transformFollow)
        {
            follow = transformFollow;
            //follow = GameObject.FindGameObjectWithTag("Player").transform;

        }
        public void SetFloatingFlatland(bool isFloating)
        {
             
            if (isFloating)
            {
                if (GetComponent<CapsuleCollider2D>() != null)
                {
                    Destroy(GetComponent<CapsuleCollider2D>());
                }
               
                if (GetComponent<Rigidbody2D>()!= null)
                {
                    Destroy(GetComponent<Rigidbody2D>());
                }
            }
            else
            {
                if (follow != null)
                {
                    CapsuleCollider2D capsuleCollider2D = this.gameObject.AddComponent<CapsuleCollider2D>();
                    rigidbody2D = this.gameObject.AddComponent<Rigidbody2D>();
                    CapsuleCollider2D capsuleCollider2D2Dfollow = this.follow.GetComponent<CapsuleCollider2D>();
                    Rigidbody2D rigidbody2Dfollow = this.follow.GetComponent<Rigidbody2D>();


                    this.gameObject.layer = follow.gameObject.layer;

                    Physics2D.IgnoreLayerCollision(follow.gameObject.layer, follow.gameObject.layer, true);



                    rigidbody2D.collisionDetectionMode = rigidbody2Dfollow.collisionDetectionMode;
                    rigidbody2D.freezeRotation = rigidbody2Dfollow.freezeRotation;
                    rigidbody2D.simulated = rigidbody2Dfollow.simulated;
                    rigidbody2D.mass = rigidbody2Dfollow.mass / 16f;
                    rigidbody2D.isKinematic = rigidbody2Dfollow.isKinematic;
                    rigidbody2D.angularDrag = rigidbody2Dfollow.angularDrag;
                    // .angularVelocity = rigidbody2Dfollow.angularVelocity;
                    rigidbody2D.gravityScale = rigidbody2Dfollow.gravityScale;
                    rigidbody2D.sleepMode = rigidbody2Dfollow.sleepMode;

                    //  rigidbody2D
                    capsuleCollider2D.direction = capsuleCollider2D2Dfollow.direction;
                    capsuleCollider2D.isTrigger = capsuleCollider2D2Dfollow.isTrigger;
                    capsuleCollider2D.size = capsuleCollider2D2Dfollow.size+new Vector2(0,0.3f);
                  //  capsuleCollider2D.density = capsuleCollider2D2Dfollow.density;
                    capsuleCollider2D.offset = capsuleCollider2D2Dfollow.offset;


                    //circleCollider2D.friction = 0.4f;

                }
            }
        }



        public void SetFloating(bool isFloating, bool isflatland)
        {
            flatland = isflatland;

            if (isflatland)
            {
                SetFloatingFlatland(isFloating);
                return;
            }
            if (isFloating)
            {
                if (GetComponent<CircleCollider2D>() != null)
                {
                    Destroy(GetComponent<CircleCollider2D>());
                }

                if (GetComponent<Rigidbody2D>() != null)
                {
                    Destroy(GetComponent<Rigidbody2D>());
                }

            }
            else
            {
                if (follow != null)
                {
                    CircleCollider2D circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
                    rigidbody2D = this.gameObject.AddComponent<Rigidbody2D>();
                    CircleCollider2D circleCollider2Dfollow = this.follow.GetComponent<CircleCollider2D>();
                    Rigidbody2D rigidbody2Dfollow = this.follow.GetComponent<Rigidbody2D>();


                    this.gameObject.layer = follow.gameObject.layer;

                    Physics2D.IgnoreLayerCollision(follow.gameObject.layer, follow.gameObject.layer, true);



                    rigidbody2D.collisionDetectionMode = rigidbody2Dfollow.collisionDetectionMode;
                    rigidbody2D.freezeRotation = rigidbody2Dfollow.freezeRotation;
                    rigidbody2D.simulated = rigidbody2Dfollow.simulated;
                    rigidbody2D.mass = rigidbody2Dfollow.mass / 16f;
                    rigidbody2D.isKinematic = rigidbody2Dfollow.isKinematic;
                    rigidbody2D.angularDrag = rigidbody2Dfollow.angularDrag;
                    // .angularVelocity = rigidbody2Dfollow.angularVelocity;
                    rigidbody2D.gravityScale = rigidbody2Dfollow.gravityScale;
                    rigidbody2D.sleepMode = rigidbody2Dfollow.sleepMode;

                    //  rigidbody2D

                    circleCollider2D.radius = circleCollider2Dfollow.radius * 0.5f;
                 //   circleCollider2D.density = circleCollider2Dfollow.density;
                    circleCollider2D.offset = new Vector2(0, -0.15f);
                    circleCollider2D.isTrigger  = circleCollider2Dfollow.isTrigger;

                    //circleCollider2D.friction = 0.4f;

                }
            }
        }


        void Update()
        {
            float num = follow.position.SqrDistanceTo(base.transform.position);


            if (num > sqrKeepDistance)
            {

                // Let it ignore collision for follow the user.
                if (rigidbody2D != null && num > sqrKeepDistance * 12)
                {

                    rigidbody2D.velocity = Vector2.zero;
                    base.transform.position = Vector3.Lerp(base.transform.position, follow.position,0.9f);


                    // 
                }
            
                

                    Vector3 vector = base.transform.position.DirectionTo(follow.position);
                float num2 = Mathf.Clamp(0.4f * num, MinSpeed, MaxSpeed);
                SetAnimatorDirection(vector,num2);
                if (rigidbody2D != null)
                {
                    if (flatland)
                    {
                        rigidbody2D.velocity = num2 * new Vector2(vector.x, 0);
                    }
                    else
                    {
                        rigidbody2D.velocity = num2 * vector;
                    }
                }
                else
                {
                    base.transform.position += num2 * Time.deltaTime * vector;
                }
            }
            else
            {
                if (rigidbody2D != null)
                {
                    if (flatland)
                    {
                        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x / 2, rigidbody2D.velocity.y);
                    }
                    else
                    {
                        rigidbody2D.velocity /= 2;
                    }
                }

                if (this.animationType > 0)
                {
                    this.animationType = 0;
                    Frames = FramesIdle;
                    currentFrame = currentFrame % Frames;
                    UpdateSprite();
                }
            }
        }
        private void SetAnimatorDirection(Vector2 direction,float speed)
        {
            //float move = direction.magnitude;
               // Debug.Log(move+" "+speed+ FramesWalk+" "+)
            if (direction.x < -0.05f)
            {
                SetDirection(GameCharacterDirection.Left);
            }
            else if (direction.x > 0.05f)
            {
                SetDirection(GameCharacterDirection.Right);
            }

            if (direction.y < -0.05f)
            {
                SetDirection(GameCharacterDirection.Down);
            }
            else if (direction.y > 0.05f)
            {
                SetDirection(GameCharacterDirection.Up);
            }
            if (FramesWalk == 0)
            {
                return;
            }

            if((this.animationType==0 || this.animationType==1) && speed > (MaxSpeed + MinSpeed) / 2)
            {
               this.animationType = 2;
                Frames = FramesRun;
                currentFrame = currentFrame % Frames;
                UpdateSprite();
            }
            else if ((this.animationType == 0 || this.animationType == 2) && speed > 0f && speed < (MaxSpeed + MinSpeed) / 2)
            {
               this.animationType = 1;
                Frames = FramesWalk;
                currentFrame = currentFrame % Frames;

                UpdateSprite();

            }
        }
    
        public void FixedUpdate()
        {
            if (timeTillNextFrame > 0f)
            {
                timeTillNextFrame -= Time.fixedDeltaTime;
            }
            else
            {
                currentFrame = (currentFrame + 1) % Frames;
                timeTillNextFrame = Duration;

             //   timeTillNextFrame = Duration / Frames;
                UpdateSprite();
            }
        }


        public void SetDirection(GameCharacterDirection direction)
        {
            currentAnimationRow = GetRow(direction);
            currentDirection = direction;
            UpdateSprite();
            if (IsSimpleSheet && (direction == GameCharacterDirection.Left|| direction == GameCharacterDirection.Right))
            {
                spriteRenderer.flipX = direction == GameCharacterDirection.Left;
            }

        }
        private int GetRow(GameCharacterDirection direction)
        {
            if (IsSimpleSheet)
            {
                return 0;
            }

            switch (direction)
            {
                case GameCharacterDirection.Down:
                    return 0;
                case GameCharacterDirection.Right:
                    return 1;
                case GameCharacterDirection.Left:
                    return 2;
                case GameCharacterDirection.Up:
                    return 3;
                default:
                    return 0;
            }
        }
        private void UpdateSprite()
        {
            //         Debug.Log(currentAnimationRow + " " + currentFrame);
            if (animationType == 0)
            {
                spriteRenderer.sprite = spritesIdle[currentAnimationRow][currentFrame];
            }
            else if (animationType == 1)
            {
                spriteRenderer.sprite = spritesWalk[currentAnimationRow][currentFrame];
            }
            else if(animationType == 2)
            {
                spriteRenderer.sprite = spritesRun[currentAnimationRow][currentFrame];
            }
        }
        private void SetSprites(Texture2D texture, bool hasDirection, Sprite[][] sprites, int nbframes)
        {

            int num = texture.width / nbframes;
            int num2;
            IsSimpleSheet = !hasDirection;

            if (IsSimpleSheet)
            {
                num2 = texture.height;
                sprites[0] = new Sprite[nbframes];
                for (int j = 0; j < nbframes; j++)
                {
                    Rect rect = new Rect(j * num, 0f, num, num2);
                    sprites[0][j] = Sprite.Create(texture, rect, 0.5f * Vector2.one, 16f);
                }
            }
            else
            {
                num2 = texture.height / 4;
                for (int k = 0; k < 4; k++)
                {
                    sprites[k] = new Sprite[nbframes];
                    for (int l = 0; l < nbframes; l++)
                    {
                        Rect rect2 = new Rect(l * num, (3 - k) * num2, num, num2);
                        sprites[k][l] = Sprite.Create(texture, rect2, 0.5f * Vector2.one, 16f);
                    }
                }
            }
        }
        public void SetSprites(TextureOrb textureorb, int type)
        {
            if (textureorb == null)
            {
                return;
            }
            SetSprites(textureorb.texture, textureorb.hasDirection, type, textureorb.frames);
        }


            public void SetSprites(Texture2D texture, bool hasDirection, int type, int nbframes)
        {
            if (GetComponent<Animator>() != null)
            {
                Destroy(GetComponent<Animator>());
            }

            //for flatland
            if (GetComponent<SpriteAnimator>() != null)
            {
                Destroy(GetComponent<SpriteAnimator>());
            }
            if (GetComponent<FollowPlayer>() != null)
            {
                Destroy(GetComponent<FollowPlayer>());
            }

            if (texture == null)
            {
                return;
            }
            if (type == 0)
            {

                FramesIdle = nbframes;
                this.animationType = 0;
                Frames = FramesIdle;
                SetSprites(texture, hasDirection, spritesIdle, nbframes);
            }
            else if (type == 1)
            {
                FramesWalk = nbframes;
                SetSprites(texture, hasDirection, spritesWalk, nbframes);
            }
            else if (type == 2)
            {
                FramesRun = nbframes;
                SetSprites(texture, hasDirection, spritesRun, nbframes);
            }

        }
    }
}
