using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace valoa_elias_tapani_kansalle
{
    public enum MainMenuMode {
        MAIN_MENU_MODE_MAIN = 0,
        MAIN_MENU_MODE_SETTINGS = 0,
    }

    public class MainMenu
    {
        private SpriteFont font1;

        public MainMenu()
        {
        }

        public void LoadContent(
            ContentManager content)
        {
            font1 = content.Load<SpriteFont>("MenuFont");
        }

        public void Draw(
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch)
        {
            Viewport viewport = graphicsDevice.Viewport;
            Vector2 fontPos;

            // TODO: Load your game content here
            fontPos = new Vector2(viewport.Width / 2, viewport.Height / 2);
            
            graphicsDevice.Clear(Color.CornflowerBlue);

            // Draw Hello World
            string output = "Hello World";

            // Find the center of the string
            Vector2 FontOrigin = font1.MeasureString(output) / 2;

            // Draw the string
            spriteBatch.DrawString(font1,
                                   output,
                                   fontPos,
                                   Color.Black,
                                   0,
                                   FontOrigin,
                                   3.0f,
                                   SpriteEffects.None,
                                   0.5f);
        }
    }
}
