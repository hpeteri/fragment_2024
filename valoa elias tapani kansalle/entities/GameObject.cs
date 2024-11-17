// Goal of this class is to implement things that appear on game world instead of 
// base.cs which can be used for invisible things such as camera 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace valoa_elias_tapani_kansalle.entities
{
    public class GameObject : BaseEntitity
    {
        public Rectangle BoundingBox { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsCollisionActive { get; set; }

        public void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle( (int)Position.X-Width/4, (int)Position.Y-Height/3, Width/2, Height-Height/4 );
        }

        public virtual void OnCollision(GameObject collideObject)
        {

        }
    }
}