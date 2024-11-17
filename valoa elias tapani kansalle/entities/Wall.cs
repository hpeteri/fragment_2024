using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace valoa_elias_tapani_kansalle.entities
{
    public class Wall : GameObject
    {
        public Vector2 position;
        public float rotation;
        public Texture2D sprite;
        public ContentManager content;
        #region sprites
        private Texture2D wallSprite;
        private Texture2D wallEdgeSprite;
        private Texture2D wallCornerSprite;
        private Texture2D tileSpriteRed;
        public Texture2D WallSprite
        {
            get { return wallSprite; }
            set { wallSprite = value; }
        }
        public Texture2D WallEdgeSprite
        {
            get { return wallEdgeSprite; }
            set { wallEdgeSprite = value; }
        }
        public Texture2D WallCornerSprite
        {
            get { return wallCornerSprite; }
            set { wallCornerSprite = value; }
        }
        public Texture2D TileSpriteRed
        {
            get { return tileSpriteRed; }
            set { tileSpriteRed = value; }
        }
        #endregion
        public Texture2D Temp { get; set; }

        public Wall(Vector2 pos, int width, int height, char orientation)
        {
            position = pos;
            IsCollisionActive = true;
            this.rotation = 90;
            this.sprite = tileSpriteRed; // Default sprite

            // Store the orientation to use it later in LoadContent()
            this.Orientation = orientation;
        }

        public char Orientation { get; private set; }

        public override void LoadContent(ContentManager content)
        {
            // Load the sprites
            tileSpriteRed = content.Load<Texture2D>("sprites/TestSquareRed");
            wallSprite = content.Load<Texture2D>("sprites/wall");
            wallEdgeSprite = content.Load<Texture2D>("sprites/wall_edge");
            wallCornerSprite = content.Load<Texture2D>("sprites/wall_corner");

            // Set the BoundingBox based on the sprite size
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, tileSpriteRed.Width, tileSpriteRed.Height);
            Console.WriteLine($"{tileSpriteRed.Width} {tileSpriteRed.Height}");

            // Set the sprite based on the orientation now that the textures are loaded
            switch (Orientation)
            {
                // Corners
                case '3': // Up left corner
                    this.rotation = (float)(Math.PI / 180) * 180;
                    this.sprite = wallCornerSprite;
                    break;
                case '1': // Up right corner
                    this.rotation = (float)(Math.PI / 180) * 90;
                    this.sprite = wallCornerSprite;
                    break;
                case '7': // Down right corner
                    this.rotation = (float)(Math.PI / 180) * 0;
                    this.sprite = wallCornerSprite;
                    break;
                case '9': // Down left corner
                    this.rotation = (float)(Math.PI / 180) * 270;
                    this.sprite = wallCornerSprite;
                    break;

                // Edges
                case '6': // Right
                    this.rotation = (float)(Math.PI / 180) * 270;
                    this.sprite = wallEdgeSprite;
                    break;
                case '8': // Up
                    this.rotation = (float)(Math.PI / 180) * 180;
                    this.sprite = wallEdgeSprite;
                    break;
                case '4': // Left
                    this.rotation = (float)(Math.PI / 180) * 0;
                    this.sprite = wallEdgeSprite;
                    break;
                case '2': // Down
                    this.rotation = (float)(Math.PI / 180) * 90;
                    this.sprite = wallEdgeSprite;
                    break;

                // Straight
                case '|': // Vertical
                    this.rotation = (float)(Math.PI / 180) * 0;
                    this.sprite = wallSprite;
                    break;
                case '_': // Horizontal
                    this.rotation = (float)(Math.PI / 180) * 90;
                    this.sprite = wallSprite;
                    break;

                case '\0':
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite,
                                         position,
                                         null,
                                         Color.White,
                                         this.rotation,
                                         System.Numerics.Vector2.Zero,
                                         System.Numerics.Vector2.One,
                                         SpriteEffects.None,
                                         EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_LEVEL));
        }
    }
}
