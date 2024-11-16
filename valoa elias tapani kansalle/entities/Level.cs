using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

// !!! WORK IN PROGRESS !!!
// This is a base class for a level
// The level is made of a uniform grid detailed by txt file

namespace valoa_elias_tapani_kansalle.entities
{
    internal class Level
    {
        private char[,] tiles;
        private int gridSize = 64;
        private Wall[] walls;
        public ContentManager content;
        private Texture2D tileSpriteBlue;
        private Texture2D tileSpriteRed;
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

        // Constructor
        public Level(Stream fileStream, IServiceProvider serviceProvider)
        {
            // Load textures
            content = new ContentManager(serviceProvider, "Content");
            
            loadTiles(fileStream);
        }
        
        // Load content
        public void LoadContent(ContentManager content)
        {
            // Textures
            tileSpriteBlue = content.Load<Texture2D>("sprites/TestSquareBlue");
            tileSpriteRed = content.Load<Texture2D>("sprites/TestSquareRed");
        }
        
        // Load tiles
        private void loadTiles(Stream fileStream)
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
                    tiles[i,j] = line[j];
                }
            }
        }
        
        // Draw tiles
        public void Draw(SpriteBatch spriteBatch)
        {
            walls = new Wall[tiles.GetLength(0) * tiles.GetLength(1)];
            int index = 0;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Vector2 position = new Vector2(i * gridSize, j * gridSize);
                    if (tiles[i,j] != '0')
                    {
                        spriteBatch.Draw(tileSpriteBlue,
                                         position,
                                         null,
                                         Color.White,
                                         0,
                                         Vector2.Zero,
                                         Vector2.One,
                                         SpriteEffects.None,
                                         EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_LEVEL));
                    } else
                    {
                        // Add walls
                        walls[index] = new Wall(position, gridSize, gridSize);
                        index += 1;
                    }
                }

            }
            // Draw walls
            foreach (Wall wall in walls)
            {
                if (wall != null)
                {
                    wall.LoadContent(content);
                    wall.Draw(spriteBatch);
                } else
                {
                    break;
                }
            }
        }
    }
}
