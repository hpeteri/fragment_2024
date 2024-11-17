using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Screwdriver : Interactable
    {
        private bool attachToPlayer;
        private Texture2D screwdriver;
        public Texture2D Screwdriver_
        {
            get { return screwdriver; }
            set { screwdriver = value; }
        }
        public Screwdriver(Vector2 pos)
        {
            Position = pos;
            attachToPlayer = false;
            interactable = true;
        }
        public override void LoadContent(ContentManager content)
        {
            screwdriver = content.Load<Texture2D>("sprites/screwdriver");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(screwdriver,
                             Position,
                             null,
                             Color.White,
                             0,
                             new Vector2(16, 16),
                             new Vector2(2, 2),
                             SpriteEffects.None,
                             EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_INTERACTABLE));
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime);
            float distance = (Vector2.Distance(player.Position, this.Position));

            // Player is facing the right direction
            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 playerFacingDirection = Vector2.Normalize(mousePosition - player.Position);

            // Calculate direction from player to this interactable
            Vector2 directionToItem = Vector2.Normalize(this.Position - player.Position);

            // Check if player is facing the interactable
            float dotProduct = Vector2.Dot(playerFacingDirection, directionToItem);

            if (Input.IsKeyPressed(Keys.F))
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Distance {0} Angle to {1}", distance, MathHelper.ToDegrees(dotProduct)));
                if (canInteract(player))
                {
                    attachToPlayer = true;
                }
            }

            if (attachToPlayer)
            {
                Position = player.Position;
                System.Diagnostics.Debug.WriteLine($"Item Position Updated: {Position}");
                player.itemHeld = "screwdriver";
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
