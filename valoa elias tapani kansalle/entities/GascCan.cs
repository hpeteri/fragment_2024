using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class GasCan : Interactable
    {
        private bool attachToPlayer;
        private bool yeeted;
        private Texture2D gascan;
        public Texture2D _Gascan
        {
            get { return gascan; }
            set { gascan = value; }
        }
        public GasCan(Vector2 pos)
        {
            Position = pos;
            attachToPlayer = false;
            interactable = true;
            yeeted = false;
        }
        public override void LoadContent(ContentManager content)
        {
            gascan = content.Load<Texture2D>("sprites/gascan");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!attachToPlayer && !yeeted)
            {
                spriteBatch.Draw(gascan,
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
                    attachToPlayer = true;
                    player.itemHeld = "gascan";
                }
            }

            if (attachToPlayer)
            {
                if (player.itemHeld == null)
                {
                    Position = new Vector2(-9999, -9999);
                    attachToPlayer = false;
                    yeeted = true;
                }
                Position = player.Position;
                System.Diagnostics.Debug.WriteLine($"Item Position Updated: {Position}");
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
