using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using valoa_elias_tapani_kansalle.collision;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Player : GameObject
    { 
        private Texture2D playerSprite;
        private Texture2D temp;

        public Vector2 Velocity { get; private set; }

        // Animation
        private bool isMoving;
        public string itemHeld;
        private int frameWidth;
        private int frameHeight;
        private int totalFrames;
        private int currentFrame;
        private double animationTimer;
        private double frameTime = 0.1; // time in seconds between frames
        private Rectangle sourceRectangle;
        private Vector2 previousPosition;

        // debug texture used for drawing bounding box around player
        public Texture2D DebugTexture { get; set; }


        public Texture2D PlayerSprite
        {
            get { return playerSprite; }
            set { playerSprite = value; }
        }

        public Player()
        {
            Speed = 300f;
            Position = new Vector2(600f, 300f);
            frameWidth = 128;
            frameHeight = 128;
            totalFrames = 6;
            currentFrame = 0;
            animationTimer = 0;
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, frameWidth, frameHeight);
            IsCollisionActive = true;

            //FIXME: scuffed way of ensuring boudingbox update works correctly
            Width = frameWidth;
            Height = frameHeight;
        }


        private static Vector2 GetMovementDirection()
        {
            Vector2 direction = Vector2.Zero;

            if( Input.IsKeyDown(Keys.A) ) 
            {
                direction.X = -1;
            }

            if ( Input.IsKeyDown(Keys.D) )
            {
                direction.X = 1;
            }

            if ( Input.IsKeyDown(Keys.W) )
            {
                direction.Y = -1;
            }

            if ( Input.IsKeyDown(Keys.S) )
            {
                direction.Y = 1;
            }

            return direction;
        }

        public override void LoadContent(ContentManager content)
        {
            PlayerSprite = content.Load<Texture2D>("sprites/lamp_walk");
        }

        public override void OnCollision(GameObject collideObject)
        {
            base.OnCollision(collideObject);
            if( collideObject is Wall )
            {
               Position = previousPosition; 
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 inputDirection = GetMovementDirection();
            previousPosition = Position;
            Position += inputDirection * updatedSpeed;



            UpdateBoundingBox();

            isMoving = inputDirection != Vector2.Zero;
            if (isMoving)
            {
                inputDirection.Normalize();
                Velocity = inputDirection * 100f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position += Velocity;

                // Update animation frames
                animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if (animationTimer >= frameTime)
                {
                    animationTimer -= frameTime;
                    currentFrame = (currentFrame + 1) % (totalFrames - 1); // Exclude the last frame for idle
                }
            }
            else
            {
                Velocity = Vector2.Zero;
                currentFrame = totalFrames - 1; // Set to the last frame for idle
            }

            // Update source rectangle for the current frame
            sourceRectangle.X = (currentFrame % (playerSprite.Width / frameWidth)) * frameWidth;
            sourceRectangle.Y = (currentFrame / (playerSprite.Width / frameWidth)) * frameHeight;
            sourceRectangle.Width = frameWidth;
            sourceRectangle.Height = frameHeight;

        }

        public float GetFacingRotation()
        {

            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 playerFacingDirection = Vector2.Normalize(mousePosition - Position);

            return (float) System.Math.Atan2(playerFacingDirection.Y, playerFacingDirection.X) + (float)System.Math.PI / 2.0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(playerSprite,
                             Position,
                             sourceRectangle,
                             Color.White,
                             GetFacingRotation(),
                             new Vector2(64, 64),
                             Vector2.One,
                             SpriteEffects.None,
                             EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_PLAYER));

            //uncomment if collision is necessary to debug
            spriteBatch.Draw(DebugTexture,
                             BoundingBox,
                             Color.White);         
        }
    }
}
