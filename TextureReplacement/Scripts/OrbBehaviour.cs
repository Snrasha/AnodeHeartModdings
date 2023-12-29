

using System;
using UnityEngine;

namespace TextureReplacement.Scripts
{
    public class OrbBehaviour : MonoBehaviour
    {
        public float KeepDistance = 1.5f;

        public float MinSpeed = 1f;

        public float MaxSpeed = 5f;

        private Transform player;

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

        public GameCharacterDirection currentDirection { get; private set; }

        private int currentFrame;


        public void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            sqrKeepDistance = KeepDistance * KeepDistance;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 0;
        }


        void Update()
        {
            float num = player.position.SqrDistanceTo(base.transform.position);
            if (num > sqrKeepDistance)
            {
                Vector3 vector = base.transform.position.DirectionTo(player.position);
                float num2 = Mathf.Clamp(0.4f * num, MinSpeed, MaxSpeed);
                SetAnimatorDirection(vector,num2);
                base.transform.position += num2 * Time.deltaTime * vector;
            }
            else
            {
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
