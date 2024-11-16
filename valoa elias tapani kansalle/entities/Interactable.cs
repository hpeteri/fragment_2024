using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Interactable : GameObject
    {
        // Things here :)
        private Player player;
        private float interactRange = 64;
        private float interactAngle = (float) MathHelper.ToRadians(45f);

        public bool canInteract(Player player)
        {
            // Player is close enough
            float distance = Vector2.Distance(player.Position, this.Position);
            if (distance > interactRange)
                return false;

            // Player is facing the right direction
            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 playerFacingDirection = Vector2.Normalize(mousePosition - player.Position);

            // Calculate direction from player to this interactable
            Vector2 directionToItem = Vector2.Normalize(this.Position - player.Position);

            // Check if player is facing the interactable
            float dotProduct = Vector2.Dot(playerFacingDirection, directionToItem);

            // interactAngle should be the cosine of the allowed angle (e.g., cos(45°) ~ 0.707)
            if (dotProduct > interactAngle)
                return false;

            return true;
        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            // Virtual
        }
    }
}
