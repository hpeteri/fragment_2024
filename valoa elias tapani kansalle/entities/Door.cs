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
        public Door(Vector2 pos)
        {
            Position = pos;
            attachToPlayer = false;
            interactable = true;
        }
        public override void LoadContent(ContentManager content)
        {
            _door = content.Load<Texture2D>("sprites/door");
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
                                 new Vector2(16, 16),
                                 new Vector2(2, 2),
                                 SpriteEffects.None,
                                 EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_INTERACTABLE));
            }
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime);
            if (Input.IsKeyPressed(Keys.F) && player.itemHeld == "key")
            {
                if (canInteract(player))
                {
                    open = true;
                }
            }
        }
    }
}
