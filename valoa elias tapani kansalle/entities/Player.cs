using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using valoa_elias_tapani_kansalle.collision;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Player : BaseEntitity
    { 
        private Texture2D playerSprite;
        private CollisionShapeRectangle collisionShape;

        public Texture2D PlayerSprite
        {
            get { return playerSprite; }
            set { playerSprite = value; }
        }

        public Player()
        {
            Speed = 300f;
            Position = new Vector2(0f, 0f);
            collisionShape = new CollisionShapeRectangle(new Rectangle(64, 64, 64, 64));
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
            PlayerSprite = content.Load<Texture2D>("ball");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += GetMovementDirection() * updatedSpeed;


            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(playerSprite,
                             Position,
                             null,
                             Color.White,
                             0,
                             Vector2.Zero,
                             Vector2.One,
                             SpriteEffects.None,
                             EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_PLAYER));

        }


    }
}
