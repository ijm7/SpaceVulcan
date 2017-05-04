using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.View.States
{
    
    public class DrawTopMenu
    {
        private ContentManager content = Program.game.Content;
        private SpriteBatch spriteBatch;
        private SpriteFont menuTitle;
        private SpriteFont menuOptions;
        private Texture2D background;
        private GraphicsDevice graphicsDevice;

        public DrawTopMenu()
        {
            this.spriteBatch = Program.game.spriteBatch;
            this.menuTitle = content.Load<SpriteFont>("Fonts/MenuTitle");
            this.menuOptions = content.Load<SpriteFont>("Fonts/MenuOptions");
            this.background = content.Load<Texture2D>("Backgrounds/Stars2");
            this.graphicsDevice = Program.game.GraphicsDevice;
            //menuList.mainMenu = 0;
        }


        public void Draw(MenuSelection _menuSelection)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height), Color.White);
            spriteBatch.DrawString(menuTitle, "SPACE", new Vector2(600, 50), Color.Red);
            spriteBatch.DrawString(menuTitle, "VULCAN", new Vector2(545, 215), Color.Red);
            spriteBatch.DrawString(menuOptions, "Play", new Vector2(850, 575), Color.White);
            spriteBatch.DrawString(menuOptions, "Controls", new Vector2(775, 650), Color.White);
            spriteBatch.DrawString(menuOptions, "Exit", new Vector2(850, 725), Color.White);
            System.Diagnostics.Debug.WriteLine("Current menu = " + _menuSelection);
            //System.Diagnostics.Debug.WriteLine("Current menu variable = " + menuList.mainMenu);
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
        }
    }
}
