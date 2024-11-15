using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using valoa_elias_tapani_kansalle.entities;

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
        private MainMenu _mainMenu;
        private ProgramMode _programMode;
        private Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _programMode = ProgramMode.PROGRAM_MODE_MENU;
            
            player = new Player();
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Input.Initialize();
            IsMouseVisible = false;
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);

        // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            switch (_programMode)
            {
                case ProgramMode.PROGRAM_MODE_GAME:

                    player.Update(gameTime);
                    if (Input.IsKeyPressed(Keys.Escape))
                    {
                        Exit();
                    }
                    break;

                case ProgramMode.PROGRAM_MODE_MENU:

                    if (Input.IsKeyPressed(Keys.Escape))
                    {
                        _programMode = ProgramMode.PROGRAM_MODE_GAME;

                        // Some player testing boilerplate 
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

                    _spriteBatch.Begin(); 
                    player.Draw(_spriteBatch);
                    _spriteBatch.End();

                    break;

                case ProgramMode.PROGRAM_MODE_MENU:
                    GraphicsDevice.Clear(Color.Brown);
                    break;

                default:
                    break;
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
