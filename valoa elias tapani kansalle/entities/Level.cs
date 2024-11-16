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
        private 

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
            for (int i = 0; i < lines.Count; i++)
            {
                char[] line = lines[i].ToCharArray();
                for (int j = 0; j < line.Length; j++)
                {
                    tiles[i,j] = line[j];
                }
            }

        }
        
        // Draw tiles
        private void drawTiles(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    spriteBatch.Draw(sprite, position, Color.White);
                }
            }
        }
    }
}
