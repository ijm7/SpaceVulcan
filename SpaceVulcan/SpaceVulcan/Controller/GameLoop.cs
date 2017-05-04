using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Controller;
using SpaceVulcan.View.States;

namespace SpaceVulcan
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameLoop : Game
    {
        GameState _state;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        DrawTopMenu drawTopMenu;


        public GameLoop()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            /*graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;*/
            graphics.ApplyChanges();
            _state = GameState.TopMenu;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
            switch (_state)
            {
                case GameState.TopMenu:
                    //UpdateMainMenu(deltaTime);
                    break;
                case GameState.ShipSelect:
                    //UpdateGameplay(deltaTime);
                    break;

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();/*
            spriteBatch.DrawString(font, "MenuOptions" + score, new Vector2(100, 100), Color.Black);*/
                                // TODO: Add your drawing code here
            switch (_state)
            {
                case GameState.TopMenu:
                    drawTopMenu = new DrawTopMenu(gameTime);
                    drawTopMenu.Draw();
                    break;
                case GameState.ShipSelect:
                    //DrawGameplay(deltaTime);
                    break;
            }
            spriteBatch.End();
            
        }
    }
}
