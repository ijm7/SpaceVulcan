using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Controller;
using SpaceVulcan.Controller.States;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using SpaceVulcan.Model.Players;
using SpaceVulcan.Model.Projectiles;
using SpaceVulcan.View.States;
using System;
using System.Collections.Generic;

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
        public EventTracker eventTracker;
        public ButtonType _buttonType;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Player player;
        public Level level;
        UpdateTopMenu updateTopMenu;
        UpdateShipSelect updateShipSelect;
        UpdateLevel updateLevel;
        UpdateIntermission updateIntermission;
        UpdatePause updatePause;
        UpdateGameOver updateGameOver;
        UpdateEnd updateEnd;
        UpdateInstructions updateInstructions;
        DrawTopMenu drawTopMenu;
        DrawShipSelect drawShipSelect;
        DrawLevel drawLevel;
        DrawIntermission drawIntermission;
        DrawPause drawPause;
        DrawGameOver drawGameOver;
        DrawEnd drawEnd;
        DrawInstructions drawInstructions;
        public Menus menuList;
        KeyboardState previousState;
        float elapsed;
        float shotCounter;
        int timer;
        int prevTimer;
        public List<Projectile> projectileList;
        Dictionary<int, List<Enemy>> currentLevel;
        List<Enemy> existingEnemies;

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
            base.Initialize();
            loader();
        }

        public void loader()
        {
            Song song = Content.Load<Song>("Music/TitleScreen");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            updateTopMenu = new UpdateTopMenu();
            updateShipSelect = new UpdateShipSelect();
            updateIntermission = new UpdateIntermission();
            updatePause = new UpdatePause();
            updateGameOver = new UpdateGameOver();
            updateEnd = new UpdateEnd();
            updateInstructions = new UpdateInstructions();
            drawIntermission = DrawIntermission.Instance;
            drawTopMenu = DrawTopMenu.Instance; //Implement rest of singletons later
            drawShipSelect = DrawShipSelect.Instance;
            drawPause = DrawPause.Instance;
            drawGameOver = DrawGameOver.Instance;
            drawEnd = DrawEnd.Instance;
            drawInstructions = DrawInstructions.Instance;
            eventTracker = new EventTracker();
            _state = GameState.TopMenu;
            _menuSelection = MenuSelection.Play;
            _buttonType = ButtonType.nil;
            _menuShipSelect = MenuShipSelect.Laser;
            menuList.mainMenu = 0;
            previousState = Keyboard.GetState();
            projectileList = new List<Projectile>();
            currentLevel = new Dictionary<int, List<Enemy>>();
            existingEnemies = new List<Enemy>();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
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
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer +=  (int)Math.Floor(elapsed);
            shotCounter += elapsed;
            switch (_state)
            {
                case GameState.TopMenu:
                    updateTopMenu.Update(keyState, previousState, ref _menuSelection, ref _state, ref _buttonType, gameTime);
                    break;
                case GameState.ShipSelect:
                    updateShipSelect.Update(keyState, previousState, ref _menuShipSelect, ref _state, ref _buttonType, gameTime, ref player, ref drawLevel, ref updateLevel, ref currentLevel);
                    break;
                case GameState.Level1:
                    updateLevel.Update(ref player, keyState,previousState, shotCounter, ref projectileList, gameTime, ref existingEnemies, ref _state, ref eventTracker, ref updateLevel);
                    break;
                case GameState.Intermission:
                    updateIntermission.Update(keyState,previousState, eventTracker,ref _state, ref updateLevel, ref drawLevel, ref player);
                    break;
                case GameState.Level2:
                    updateLevel.Update(ref player, keyState, previousState, shotCounter, ref projectileList, gameTime, ref existingEnemies, ref _state, ref eventTracker, ref updateLevel);
                    break;
                case GameState.Level3:
                    updateLevel.Update(ref player, keyState, previousState, shotCounter, ref projectileList, gameTime, ref existingEnemies, ref _state, ref eventTracker, ref updateLevel);
                    break;
                case GameState.Pause:
                    updatePause.Update(keyState, previousState, ref _state, eventTracker);
                    break;
                case GameState.GameOver:
                    drawTopMenu = null;
                    updateGameOver.Update(keyState, ref _state);
                    break;
                case GameState.End:
                    drawTopMenu = null;
                    updateEnd.Update(keyState, ref _state);
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                case GameState.Controls:
                    updateInstructions.Update(keyState, ref _state, ref _buttonType);
                    break;
            }
            previousState = keyState;
            prevTimer = timer;
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (_state)
            {
                case GameState.TopMenu:
                    drawTopMenu.Draw(_menuSelection, _buttonType, gameTime, elapsed);
                    break;
                case GameState.ShipSelect:
                    drawShipSelect.Draw(_menuShipSelect, _buttonType, gameTime, elapsed);
                    break;
                case GameState.Level1:
                    drawLevel.Draw(player, elapsed, projectileList, existingEnemies, eventTracker);
                    player.firing = false;
                    break;
                case GameState.Intermission:
                    drawIntermission.Draw(player);
                    player.firing = false;
                    break;
                case GameState.Level2:
                    drawLevel.Draw(player, elapsed, projectileList, existingEnemies, eventTracker);
                    player.firing = false;
                    break;
                case GameState.Level3:
                    drawLevel.Draw(player, elapsed, projectileList, existingEnemies, eventTracker);
                    player.firing = false;
                    break;
                case GameState.Pause:
                    drawPause.Draw();
                    player.firing = false;
                    break;
                case GameState.GameOver:
                    drawGameOver.Draw(player);
                    player.firing = false;
                    break;
                case GameState.End:
                    drawEnd.Draw(player);
                    player.firing = false;
                    break;
                case GameState.Controls:
                    drawInstructions.Draw(_buttonType);
                    break;
            }
            spriteBatch.End();
            _buttonType = ButtonType.nil;
            base.Draw(gameTime);
        }
    }
}
