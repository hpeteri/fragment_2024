using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Interactable : GameObject
    {
        // Things here :)
        private Player player;
        private float interactRange = 64;
        private float interactAngle = 0.7f;

        private bool canInteract()
        {
            // Player is close enough
            float distance = Vector2.Distance(player.Position, this.Position);
            if (distance > interactRange)
                return false;

            // Player is facing the right direction
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 playerFacingDirection = Vector2.Normalize(mousePosition - player.Position);
            Vector2 directionToItem = Vector2.Normalize(this.Position - player.Position);

            // Check if the player is facing this using a dot product
            if (Vector2.Dot(playerFacingDirection, directionToItem) < interactAngle)
                return false;
            
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (canInteract() && Input.IsKeyDown(Keys.F))
            {
                // Interact
                System.Console.WriteLine("jelqKING");
            }
        }
    }
}
