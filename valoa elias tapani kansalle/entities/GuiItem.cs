using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    internal class GuiItem
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Virtual
        }
    }
}
