using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
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
        private ProgramMode _programMode;

        private MainMenu _mainMenu;
        private Player player;
        private Level level;
        private Stream fileStream;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _programMode = ProgramMode.PROGRAM_MODE_MENU;

            _mainMenu = new MainMenu();
            player = new Player();

            // Load level
            string levelPath = string.Format("Content/levels/level1.txt");
            fileStream = TitleContainer.OpenStream(levelPath);
            level = new Level(fileStream, Services);

            // Set fullscreen
            _graphics.IsFullScreen = false;
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
            _mainMenu.LoadContent(Content);
            level.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            _mainMenu.Update(gameTime);

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

            
            _spriteBatch.Begin();
            switch (_programMode)
            {
                case ProgramMode.PROGRAM_MODE_GAME:
                    GraphicsDevice.Clear(Color.Cyan);
                    
                    player.Draw(_spriteBatch);
                    level.Draw(_spriteBatch);

                    break;

                case ProgramMode.PROGRAM_MODE_MENU:
                    GraphicsDevice.Clear(Color.Brown);

                    _mainMenu.Draw(GraphicsDevice, _spriteBatch);

                    break;

                default:
                    break;
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
