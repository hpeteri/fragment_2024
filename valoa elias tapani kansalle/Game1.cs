using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using valoa_elias_tapani_kansalle.collision;
using valoa_elias_tapani_kansalle.entities;

namespace valoa_elias_tapani_kansalle
{
    public enum ProgramMode
    {
        PROGRAM_MODE_MENU = 0,
        PROGRAM_MODE_GAME = 1,
        PROGRAM_MODE_SHOULD_QUIT = 2,
    }

    public class GameState
    {
        public ProgramMode programMode;

        public GameState()
        {
            this.programMode = ProgramMode.PROGRAM_MODE_MENU;
        }
    }
                                              
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState _gameState;
        private MainMenu _mainMenu;
        private Player player;
        private Level level;
        private Stream fileStream;
        private LightLayer _lightLayer;
        private GameObject[] gameObjects;

        private Texture2D debugTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _gameState = new GameState();

            _mainMenu = new MainMenu();
            player = new Player();

            
            // Set fullscreen
            _graphics.IsFullScreen = false;
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
            player.LoadContent(Content);
            _mainMenu.LoadContent(Content);

            _lightLayer = new LightLayer(GraphicsDevice);


            // create debugTexture for object
            debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            debugTexture.SetData([Color.White]);
            player.DebugTexture = debugTexture;
            
            // Load level
            string levelPath = string.Format("Content/levels/level1.txt");
            fileStream = TitleContainer.OpenStream(levelPath);
            level = new Level(fileStream, Services);
            level.LoadContent(Content);

            gameObjects = level.Walls;
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            GraphicsDevice.SetRenderTarget(null);

            switch (_gameState.programMode)
            {
                case ProgramMode.PROGRAM_MODE_GAME:

                    player.Update(gameTime);
                    if (Input.IsKeyPressed(Keys.Escape))
                    {
                        _gameState.programMode = ProgramMode.PROGRAM_MODE_MENU;

                        _mainMenu.SetMenu(MainMenuMode.MAIN_MENU_MODE_PAUSED);
                    }

                    level.Update(gameTime, player);

                    // testing collision system
                    var collidedObject = CollisionSystem.IsColliding(player, gameObjects);
                    if( collidedObject != null )
                    {
                        //Console.WriteLine($"Player collided with {collidedObject}");
                        player.OnCollision(collidedObject);
                        Console.WriteLine("Collision is happening!");
                    }
                    break;

                case ProgramMode.PROGRAM_MODE_MENU:

                    _mainMenu.Update(gameTime, _gameState);
                                
                    if (Input.IsKeyPressed(Keys.Escape))
                    {
                        _gameState.programMode = ProgramMode.PROGRAM_MODE_GAME;

                        // Some player testing boilerplate 
                    }
                    break;

                default:
                    break;
            }



            if (_gameState.programMode == ProgramMode.PROGRAM_MODE_SHOULD_QUIT)
            {
                Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            switch (_gameState.programMode)
            {
                case ProgramMode.PROGRAM_MODE_GAME:

                    /**
                     * Render targets get cleared when bound, so
                     * UpdateTorchBeam renders to an offscreen
                     * rendertarget first and then binds the default
                     * render target.
                     *
                     *
                     */
                    GraphicsDevice.Clear(Color.Cyan);

                    _lightLayer.UpdateTorchBeam(GraphicsDevice,
                        player.Position,
                        player.GetFacingRotation());


                    level.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    _lightLayer.Draw(_spriteBatch);


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
