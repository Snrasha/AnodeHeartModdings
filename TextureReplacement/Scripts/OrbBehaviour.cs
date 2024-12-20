﻿

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextureReplacement.Scripts
{
    public class OrbBehaviour : MonoBehaviour
    {

        private Transform follow;
        private FollowMovement followMovement;


        public Sprite[][] spritesIdle = new Sprite[4][];
        public Sprite[][] spritesWalk = new Sprite[4][];
        public Sprite[][] spritesRun = new Sprite[4][];

        public Sprite[][] spritesShadowIdle = new Sprite[4][];
        public Sprite[][] spritesShadowWalk = new Sprite[4][];
        public Sprite[][] spritesShadowRun = new Sprite[4][];

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
        private bool hasCustomShadow = false;


        private SpriteRenderer shadowSpriteRenderer;



        public GameCharacterDirection currentDirection { get; private set; }

        private int currentFrame;


        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
        }
       public void SetFollow(Transform transformFollow)
        {
            follow = transformFollow;
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
                    capsuleCollider2D.enabled = false;

                    //this.gameObject.layer = follow.gameObject.layer;

                    // Physics2D.IgnoreLayerCollision(follow.gameObject.layer, follow.gameObject.layer, true);







                    rigidbody2D.collisionDetectionMode = rigidbody2Dfollow.collisionDetectionMode;
                    rigidbody2D.freezeRotation = rigidbody2Dfollow.freezeRotation;
                    rigidbody2D.simulated = rigidbody2Dfollow.simulated;
               //     rigidbody2D.mass = rigidbody2Dfollow.mass / 16f;
                    rigidbody2D.mass = 0f;

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
                    capsuleCollider2D.enabled = false;

                    //circleCollider2D.friction = 0.4f;

                }
            }
        }
        public void SetFloatingNotFlatland(bool isFloating)
        {
            if (hasCustomShadow)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.gameObject.name.Equals("Shadow"))
                    {
                        shadowSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    }
                }

                if (shadowSpriteRenderer == null)
                {
                    GameObject shadow = new GameObject("Shadow");
                    shadowSpriteRenderer = shadow.AddComponent<SpriteRenderer>();
                    shadow.transform.parent = this.transform;
                    //   shadowSpriteRenderer.material=Material.CreateWithString()

                }
              //  spriteRenderer.sortingOrder--;
                shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
                shadowSpriteRenderer.drawMode = spriteRenderer.drawMode;
                shadowSpriteRenderer.size = spriteRenderer.size;
                shadowSpriteRenderer.material = spriteRenderer.material;
                shadowSpriteRenderer.transform.localPosition = Vector3.zero;
                shadowSpriteRenderer.transform.localScale = Vector3.one;
                shadowSpriteRenderer.transform.localRotation = Quaternion.identity;
                shadowSpriteRenderer.color = new Color(1f, 1f, 1f, .8f);
                shadowSpriteRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
                shadowSpriteRenderer.sortingGroupID = spriteRenderer.sortingGroupID;
                shadowSpriteRenderer.renderingLayerMask = spriteRenderer.renderingLayerMask;

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

                    circleCollider2D.sharedMaterial = new PhysicsMaterial2D();
                    //circleCollider2D.sharedMaterial.friction = 1f;
                    circleCollider2D.sharedMaterial.bounciness = 0.3f;
                    circleCollider2D.sharedMaterial.friction = 0f;
                    //circleCollider2D.sharedMaterial.bounciness = 0.1f;

                    rigidbody2D.collisionDetectionMode = rigidbody2Dfollow.collisionDetectionMode;
                    rigidbody2D.freezeRotation = rigidbody2Dfollow.freezeRotation;
                    rigidbody2D.simulated = rigidbody2Dfollow.simulated;
                    rigidbody2D.mass = rigidbody2Dfollow.mass / 16f;
                    rigidbody2D.isKinematic = rigidbody2Dfollow.isKinematic;
                    rigidbody2D.angularDrag = rigidbody2Dfollow.angularDrag;
                    // .angularVelocity = rigidbody2Dfollow.angularVelocity;
                    rigidbody2D.gravityScale = rigidbody2Dfollow.gravityScale;
                    rigidbody2D.sleepMode = rigidbody2Dfollow.sleepMode;
                    circleCollider2D.enabled = false;

                    //  rigidbody2D

                    circleCollider2D.radius = circleCollider2Dfollow.radius * 0.5f;
                    //   circleCollider2D.density = circleCollider2Dfollow.density;
                    circleCollider2D.offset = new Vector2(0, -0.15f);
                    circleCollider2D.isTrigger = circleCollider2Dfollow.isTrigger;

                    //circleCollider2D.friction = 0.4f;

                }
            }
        }



        public void SetFloating(bool isFloating, bool isflatland,bool isLowRes)
        {

            if (isLowRes)
            {
                transform.position = follow.position;
            }

            if (isflatland)
            {
                SetFloatingFlatland(isFloating);
            }
            else
            {
                SetFloatingNotFlatland(isFloating);
            }
            followMovement = new FollowMovement(this, rigidbody2D, isflatland);
            followMovement.SetTransform(transform, follow);


        }

        void Update()
        {
            followMovement.Update();
        }
        public void SetCustomShadow(bool customshadow)
        {
            hasCustomShadow = customshadow;
        }
        public void ResetToIdle()
        {
            if (this.animationType > 0)
            {
                this.animationType = 0;
                Frames = FramesIdle;
                currentFrame = currentFrame % Frames;
                UpdateSprite();
            }
        }
        public void UnlockMovement()
        {
            if (followMovement != null)
            {
                followMovement.UnlockMovement();
            }
        }

        public void SetAnimatorDirection(Vector2 direction,float speed)
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

            if((this.animationType==0 || this.animationType==1) && speed > followMovement.MiddleSpeed)
            {
               this.animationType = 2;
                Frames = FramesRun;
                currentFrame = currentFrame % Frames;
                UpdateSprite();
            }
            else if ((this.animationType == 0 || this.animationType == 2) && speed > 0f && speed < followMovement.MiddleSpeed)
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
                if (shadowSpriteRenderer != null)
                {
                    shadowSpriteRenderer.flipX = direction == GameCharacterDirection.Left;
                }
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
                if(shadowSpriteRenderer != null)
                {
                    shadowSpriteRenderer.sprite = spritesShadowIdle[currentAnimationRow][currentFrame % spritesShadowIdle[0].Length];
                }

            }
            else if (animationType == 1)
            {
                spriteRenderer.sprite = spritesWalk[currentAnimationRow][currentFrame];
                if (shadowSpriteRenderer != null)
                {
                    shadowSpriteRenderer.sprite = spritesShadowWalk[currentAnimationRow][currentFrame% spritesShadowWalk[0].Length];
                }
            }
            else if(animationType == 2)
            {
                spriteRenderer.sprite = spritesRun[currentAnimationRow][currentFrame];
                if (shadowSpriteRenderer != null)
                {
                    shadowSpriteRenderer.sprite = spritesShadowRun[currentAnimationRow][currentFrame % spritesShadowRun[0].Length];
                }
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
                    sprites[0][j].name = texture.name;
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
                        sprites[k][l].name = texture.name;

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
            else if (type == 3)
            {
                SetSprites(texture, hasDirection, spritesShadowIdle, nbframes);
            }
            else if (type == 4)
            {
                SetSprites(texture, hasDirection, spritesShadowWalk, nbframes);
            }
            else if (type == 5)
            {
                SetSprites(texture, hasDirection, spritesShadowRun, nbframes);
            }
        }
    }
}
