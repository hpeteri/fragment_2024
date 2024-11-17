using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace valoa_elias_tapani_kansalle.entities
{
    public class TorchBox : GameObject
    {
        private Vector2 origin;
        public TorchBox(int height, int width)
        {
            this.Width = width;
            this.Height = height;
            origin = new Vector2(this.Position.X + 0, this.Position.Y + Height / 2);
            IsCollisionActive = true;
        }

        public override void OnCollision(GameObject collideObject)
        {
            /*Tässä kohtaa loppu moti tehdä tätä
             * en koskaan kerenny ees kokeilla toimiiko toi pyöritys
            if (collideObject.interactable)
            {
                collideObject.
            }*/
        }
        
        public float ToMouseRotation()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 direction = Vector2.Normalize(mousePosition - Position);

            return (float)System.Math.Atan2(direction.Y, direction.X) + (float)System.Math.PI / 2.0f;
        }

        public void RotateBoundingBox(Vector2 pos)
        {
            // Rotate the bounding box so that it's origin stays at pos
            // and the box is 'pointed' at ToMouseRotation
            float rotation = ToMouseRotation();

            // Center
            Vector2 center = new Vector2(Position.X - Width/2, Position.Y - Height/2);

            // Offset
            Vector2 offset = new Vector2(center.X - pos.X, center.Y - pos.Y);

            // Rotate the offset vector around the origin (pos)
            Vector2 rotatedOffset = new Vector2(
                (float)(Math.Cos(rotation) * offset.X - Math.Sin(rotation) * offset.Y),
                (float)(Math.Sin(rotation) * offset.X + Math.Cos(rotation) * offset.Y)
            );

            // Update the Position to keep the origin at pos
            Position = pos + rotatedOffset;

            // Update the origin of the box relative to the new position
            origin = pos;
        }
    }
}
