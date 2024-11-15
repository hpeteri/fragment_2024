using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace valoa_elias_tapani_kansalle
{
    public enum ProgramMode
    {
        PROGRAM_MODE_MENU = 0,
        PROGRAM_MODE_GAME = 1,
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ProgramMode _programMode;
        private MainMenu _mainMenu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _programMode = ProgramMode.PROGRAM_MODE_MENU;
            _mainMenu = new MainMenu(Content);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Input.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            switch (_programMode)
            {
                case ProgramMode.PROGRAM_MODE_GAME:

                    if (Input.IsKeyPressed(Keys.Escape))
                    {
                        Exit();
                    }
                    break;

                case ProgramMode.PROGRAM_MODE_MENU:

                    if (Input.IsKeyPressed(Keys.Escape))
                    {
                        _programMode = ProgramMode.PROGRAM_MODE_GAME;
                    }
                    break;

                default:
                    break;
            }



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (_programMode)
            {
                case ProgramMode.PROGRAM_MODE_GAME:
                    GraphicsDevice.Clear(Color.Cyan);

                    break;

                case ProgramMode.PROGRAM_MODE_MENU:
                    GraphicsDevice.Clear(Color.Brown);
                    _mainMenu.Draw(GraphicsDevice, _spriteBatch);
                    break;

                default:
                    break;
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
