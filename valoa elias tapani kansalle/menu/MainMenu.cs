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
        private SpriteFont _menuFont;
        private int        _menuIndex;
        private MenuItem []  _menuItems;

        public MainMenu()
        {
            _menuIndex = 0;

            _menuItems = new MenuItem[] {
                new MenuItem("Play", 1.0f),
                new MenuItem("Settings", 1.0f),
                new MenuItem("Quit", 1.0f)
            };
        }

        public void LoadContent(
            ContentManager content)
        {
            _menuFont = content.Load<SpriteFont>("MenuFont");
        }

        public void Update(
            GameTime gameTime)
        {

            if (Input.IsKeyPressed(Keys.W))
            {
                _menuIndex += _menuItems.Length - 1;
            }

            if (Input.IsKeyPressed(Keys.S))
            {
                _menuIndex ++;
            }

            if (Input.IsKeyPressed(Keys.Space))
            {
            }

            _menuIndex = _menuIndex % _menuItems.Length;

            for (int i = 0; i < _menuItems.Length; i++)
            {
                var item = _menuItems[i];

                item.UpdateHovered(gameTime,
                                   i == _menuIndex);
            }

        }
        
        public void Draw(
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch)
        {
            Viewport viewport = graphicsDevice.Viewport;
            Vector2 fontPos, fontOrigin, fontOffset;
            float fontScale = 1.0f;
            float fontLineScale = 1.5f;

            int menuItemCount = _menuItems.Length;

            // TODO: Load your game content here
            fontPos = new Vector2(viewport.Width / 2, viewport.Height / 2);

            fontOffset.X = 0;
            fontOffset.Y = 0;
            
            for (int i = 0; i < _menuItems.Length; i++)
            {
                var item = _menuItems[i];

                var baseSize = _menuFont.MeasureString(item.text);
                fontOffset +=  baseSize * item.scale * fontScale * fontLineScale;
            }

            fontPos.Y -= fontOffset.Y * 0.5f;
            
            for (int i = 0; i < _menuItems.Length; i++)
            {
                var item = _menuItems[i];

                fontOrigin = _menuFont.MeasureString(item.text) / 2.0f;

                Color fontColor =
                    i == _menuIndex ?
                        Color.Yellow :
                        Color.Black;

                // Draw the string
                spriteBatch.DrawString(_menuFont,
                                       item.text,
                                       fontPos,
                                       fontColor,
                                       0,
                                       fontOrigin,
                                       fontScale * item.scale,
                                       SpriteEffects.None,
                                       0.5f);

                fontPos.Y += fontOrigin.Y * 2.0f * item.scale * fontScale * fontLineScale;
            }
        }
    }
}
