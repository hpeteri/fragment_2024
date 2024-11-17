using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.IO.Pipes;

// !!! WORK IN PROGRESS !!!
// This is a base class for a level
// The level is made of a uniform grid detailed by txt file

namespace valoa_elias_tapani_kansalle.entities
{
    internal class Level
    {
        private char[,] tiles;
        private int gridSize = 128;
        private Wall[] walls;
        private Interactable[] interactables;
        public ContentManager content;

        #region sprites
        private Texture2D tileSpriteBlue;
        private Texture2D tileSpriteRed;
        private Texture2D wallSprite;
        private Texture2D wallEdgeSprite;
        private Texture2D wallCornerSprite;
        private Texture2D floorSprite;
        public Texture2D TileSpriteBlue
        {
            get { return tileSpriteBlue; }
            set { tileSpriteBlue = value; }
        }
        public Texture2D TileSpriteRed
        {
            get { return tileSpriteRed; }
            set { tileSpriteRed = value; }
        }
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
        public Texture2D FloorSprite
        {
            get { return floorSprite; }
            set { floorSprite = value; }
        }
        #endregion

        public Wall[] Walls 
        {
            get { return walls; }
            set { walls = value; }
        }

        // Constructor
        public Level(Stream fileStream, IServiceProvider serviceProvider)
        {
            // Load textures
            content = new ContentManager(serviceProvider, "Content");
            LoadTiles(fileStream);
        }

        // Load content
        public void LoadContent(ContentManager content)
        { 
            // Textures
            tileSpriteBlue = content.Load<Texture2D>("sprites/TestSquareBlue");
            tileSpriteRed = content.Load<Texture2D>("sprites/TestSquareRed");
            floorSprite = content.Load<Texture2D>("sprites/floor");
        }

        // Load tiles
        private void LoadTiles(Stream fileStream)
        {

            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
            }

            int rows = lines.Count;
            int columns = lines.Max(line => line.Length);
            tiles = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                char[] line = lines[i].ToCharArray();
                for (int j = 0; j < line.Length; j++)
                {
                    tiles[i, j] = line[j];
                }
            }


            interactables = new Interactable[tiles.GetLength(0) * tiles.GetLength(1)];
            walls = new Wall[tiles.GetLength(0) * tiles.GetLength(1)];
            
            int interactablesIndex = 0;
            int wallsIndex = 0;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    
                    Vector2 position = new Vector2(i * gridSize, j * gridSize); 
                    if (tiles[i, j] == 'i')
                    {
                        // Add interactables
                        Screwdriver screwDriver = new Screwdriver(position);
                        screwDriver.LoadContent(content);
                        interactables[interactablesIndex] = screwDriver;
                        interactablesIndex += 1;
                    }
                    else if (tiles[i, j] != '0')
                    {
                        // Add walls
                        walls[wallsIndex] = new Wall(position, gridSize, gridSize, tiles[i,j]);
                        
                        walls[wallsIndex].LoadContent(content);
                        wallsIndex += 1;
                    }
                }
            }
        }

        // Draw tiles
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Vector2 position = new Vector2(i * gridSize, j * gridSize);
                    Rectangle targetRect = new Rectangle((int)position.X, (int)position.Y, gridSize, gridSize);
                    // Draw floor (yes, even under interactables)
                    if (tiles[i, j] == '0' || tiles[i, j] == 'i')
                    {
                        
                        spriteBatch.Draw(floorSprite,
                                         position,
                                         targetRect,
                                         Color.White,
                                         0,
                                         Vector2.Zero,
                                         Vector2.One,
                                         SpriteEffects.None,
                                         EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_LEVEL)); 
                    }
                }
            }


            // Draw walls
            foreach (Wall wall in walls)
            {
                if (wall != null)
                {
                    wall.Draw(spriteBatch);
                }
                else
                {
                    break;
                }
            }

            // Draw items
            foreach (Interactable item in interactables)
            {
                if (item != null)
                {
                    item.Draw(spriteBatch);
                }
                else
                {
                    break;
                }
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
            foreach (Interactable item in interactables)
            {
                if (item != null)
                {
                    item.Update(gameTime, player);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
