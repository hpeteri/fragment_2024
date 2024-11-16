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

        public void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle( (int)Position.X, (int)Position.Y, Width, Height );
        }
    }
}