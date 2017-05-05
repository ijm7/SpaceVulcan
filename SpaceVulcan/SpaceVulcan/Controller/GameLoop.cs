using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Controller;
using SpaceVulcan.Controller.States;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Players;
using SpaceVulcan.Util;
using SpaceVulcan.Util.States;
using System.Text;

namespace SpaceVulcan
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameLoop : Game
    {
        public GameState _state;
        public MenuSelection _menuSelection;
        public MenuShipSelect _menuShipSelect;
        public ButtonType _buttonType;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Player player;
        DrawTopMenu drawTopMenu;
        DrawShipSelect drawShipSelect;
        UpdateTopMenu updateTopMenu;
        UpdateShipSelect updateShipSelect;
        public Menus menuList;
        KeyboardState previousState;
        float elapsed;

        public GameLoop()
        {
            graphics = new GraphicsDeviceManager(this);
            menuList = new Menus();
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
            //graphics.IsFullScreen = true;
            /*graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;*/
            graphics.ApplyChanges();
            updateTopMenu = new UpdateTopMenu();
            updateShipSelect = new UpdateShipSelect();
            drawTopMenu = new DrawTopMenu();
            drawShipSelect = new DrawShipSelect();
            _state = GameState.TopMenu;
            _menuSelection = MenuSelection.Play;
            _buttonType = ButtonType.nil;
            _menuShipSelect = MenuShipSelect.Laser;
            menuList.mainMenu = 0;
            previousState = Keyboard.GetState();
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
            KeyboardState keyState = Keyboard.GetState();
            /*System.Text.StringBuilder sb = new StringBuilder();
            foreach (var key in keyState.GetPressedKeys())
                sb.Append("Key: ").Append(key).Append(" pressed ");

            if (sb.Length > 0 & previousState!=keyState)
            {
                checkPress = true;
            }*/
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (_state)
            {
                case GameState.TopMenu:
                    updateTopMenu.Update(keyState, previousState, ref _menuSelection, ref _state, ref _buttonType, gameTime);
                    break;
                case GameState.ShipSelect:
                    updateShipSelect.Update(keyState, previousState, ref _menuShipSelect, ref _state, ref _buttonType, gameTime, ref player);
                    break;
                case GameState.Exit:
                    Exit();
                    break;
            }
            // TODO: Add your update logic here
            previousState = keyState;
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {            spriteBatch.Begin();/*
            spriteBatch.DrawString(font, "MenuOptions" + score, new Vector2(100, 100), Color.Black);*/
                                // TODO: Add your drawing code here
            switch (_state)
            {
                case GameState.TopMenu:
                    drawTopMenu.Draw(_menuSelection, _buttonType, gameTime, elapsed);
                    break;
                case GameState.ShipSelect:
                    drawShipSelect.Draw(_menuShipSelect, _buttonType, gameTime, elapsed);
                    //DrawGameplay(deltaTime);
                    break;
            }
            spriteBatch.End();
            _buttonType = ButtonType.nil;
            base.Draw(gameTime);
        }
    }
}
