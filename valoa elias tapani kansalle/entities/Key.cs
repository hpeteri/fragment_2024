using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Key : Interactable
    {
        private bool attachToPlayer;
        private Texture2D _key;
        public Texture2D _Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public Key(Vector2 pos)
        {
            Position = pos;
            attachToPlayer = false;
            interactable = true;
        }
        public override void LoadContent(ContentManager content)
        {
            _key = content.Load<Texture2D>("sprites/key");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!attachToPlayer)
            {
                spriteBatch.Draw(_key,
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
            if (Input.IsKeyPressed(Keys.F))
            {
                if (canInteract(player) && player.itemHeld == null)
                {
                    player.itemHeld = "key";
                    attachToPlayer = true;
                }
            }

            if (attachToPlayer)
            {
                if (player.itemHeld == null)
                {
                    Position = new Vector2(-9999, -9999);
                    attachToPlayer = false;
                }
                Position = player.Position;
                if (Input.IsKeyPressed(Keys.F))
                {
                    System.Diagnostics.Debug.WriteLine("Attached");
                }
                if (Input.IsKeyPressed(Keys.G))
                {
                    System.Diagnostics.Debug.WriteLine("Dropping");
                    attachToPlayer = false;
                    player.itemHeld = null;

                }
            }
        }
    }
}
