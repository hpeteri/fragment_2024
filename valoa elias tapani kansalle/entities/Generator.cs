using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Generator : Interactable
    {
        private bool fueled;
        private bool _fixed;
        private Texture2D _generator;
        public Texture2D _Generator
        {
            get { return _generator; }
            set { _generator = value; }
        }


        public Texture2D DebugTexture { get; set; }
        public Generator(Vector2 pos)
        {
            Position = pos;
            interactable = true;
            IsCollisionActive = true;
        }
        public override void LoadContent(ContentManager content)
        {
            _generator = content.Load<Texture2D>("sprites/generator");
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, _generator.Width, _generator.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_generator,
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

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime);
            if (Input.IsKeyPressed(Keys.F) && canInteract(player))
            {
                if (player.itemHeld == "gasCan")
                {
                    player.itemHeld = null;
                    fueled = true;
                } else if (player.itemHeld == "screwdriver")
                {
                    player.itemHeld = null;
                    _fixed = true;
                }
            }
            if (_fixed && fueled)
            {
                // Win condition
                System.Diagnostics.Debug.WriteLine("Voitit pelin!");
            }
        }
    }
}
