using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Controller;
using SpaceVulcan.Model;
using SpaceVulcan.Util;
using System.Collections.Generic;

namespace SpaceVulcan.View.States
{

    public sealed class DrawTopMenu
    {
        private static readonly DrawTopMenu instance = new DrawTopMenu();
        private ContentManager content = Program.game.Content;
        private SpriteBatch spriteBatch;
        private SpriteFont menuTitle;
        private SpriteFont menuOptions;
        private Texture2D background;
        List<SoundEffect> soundEffects;
        private GraphicsDevice graphicsDevice;
        ScrollingBackground menuBackground;

        static DrawTopMenu()
        {

        }
        private DrawTopMenu()
        {
            soundEffects = new List<SoundEffect>();
            this.spriteBatch = Program.game.spriteBatch;
            this.menuTitle = content.Load<SpriteFont>("Fonts/MenuTitle");
            this.menuOptions = content.Load<SpriteFont>("Fonts/MenuOptions");
            this.background = content.Load<Texture2D>("Backgrounds/Stars2");
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_select1"));
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_move1"));
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_select2"));
            this.graphicsDevice = Program.game.GraphicsDevice;
            menuBackground = new ScrollingBackground();
            menuBackground.Load(graphicsDevice, background);
        }

        public static DrawTopMenu Instance
        {
            get
            {
                return instance;
            }
        }

        public void Draw(MenuSelection _menuSelection, ButtonType _buttonType, GameTime gameTime, float elapsed)
        {
            menuBackground.Update(elapsed * 25);
            menuBackground.Draw(spriteBatch);
            spriteBatch.DrawString(menuTitle, "SPACE", new Vector2(600, 50), Color.Red);
            spriteBatch.DrawString(menuTitle, "VULCAN", new Vector2(545, 215), Color.Red);
            spriteBatch.DrawString(menuOptions, "Play", new Vector2(850, 575), Color.White);
            spriteBatch.DrawString(menuOptions, "Controls", new Vector2(775, 650), Color.White);
            spriteBatch.DrawString(menuOptions, "Exit", new Vector2(850, 725), Color.White);
            switch (_menuSelection)
            {
                case MenuSelection.Play:
                    spriteBatch.DrawString(menuOptions, ">", new Vector2(800, 575), Color.Red);
                    break;
                case MenuSelection.Controls:
                    spriteBatch.DrawString(menuOptions, ">", new Vector2(725, 650), Color.Red);
                    break;
                case MenuSelection.Exit:
                    spriteBatch.DrawString(menuOptions, ">", new Vector2(800, 725), Color.Red);
                    break;
            }
            switch (_buttonType)
            {
                case ButtonType.enter:
                    soundEffects[0].Play();
                    break;
                case ButtonType.move:
                    soundEffects[1].Play();
                    break;
                case ButtonType.back:
                    soundEffects[2].Play();
                    break;
            }
        }
    }
}
