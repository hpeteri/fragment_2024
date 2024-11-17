using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Door : Interactable
    {
        private bool open;
        private Texture2D _door;
        public Texture2D _Door
        {
            get { return _door; }
            set { _door = value; }
        }


        public Texture2D DebugTexture { get; set; }
        public Door(Vector2 pos)
        {
            Position = pos;
            interactable = true;
        }
        public override void LoadContent(ContentManager content)
        {
            _door = content.Load<Texture2D>("sprites/door");
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, _door.Width, _door.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!open)
            {
                spriteBatch.Draw(_door,
                                 Position,
                                 null,
                                 Color.White,
                                 0,
                                 Vector2.Zero,
                                 Vector2.One,
                                 SpriteEffects.None,
                                 EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_INTERACTABLE));

                spriteBatch.Draw(DebugTexture, BoundingBox, Color.White);
            }
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime);
            if (Input.IsKeyPressed(Keys.F) && player.itemHeld == "key")
            {
                if (canInteract(player))
                {
                    player.itemHeld = null;
                    open = true;
                }
            }
        }
    }
}
