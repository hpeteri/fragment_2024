using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Wall : GameObject
    {
        public Vector2 position;
        public ContentManager content;
        private Texture2D tileSpriteRed;
        public Texture2D Temp { get; set; } 


        public Texture2D TileSpriteRed
        {
            get { return tileSpriteRed; }
            set { tileSpriteRed = value; }
        }

        public Wall(Vector2 pos, int width, int height)
        {
            position = pos;
            IsCollisionActive = true;
        }
        public override void LoadContent(ContentManager content)
        {
            tileSpriteRed = content.Load<Texture2D>("sprites/TestSquareRed");
            BoundingBox = new Rectangle( (int)position.X, (int) position.Y, tileSpriteRed.Width, tileSpriteRed.Height);
            Console.WriteLine($"{tileSpriteRed.Width} {tileSpriteRed.Height}" );
        }

         public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileSpriteRed,
                                         position,
                                         null,
                                         Color.White,
                                         0,
                                         System.Numerics.Vector2.Zero,
                                         System.Numerics.Vector2.One,
                                         SpriteEffects.None,
                                         EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_LEVEL));

        }
    }
}
